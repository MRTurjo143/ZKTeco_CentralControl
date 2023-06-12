using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StandaloneSDKDemo;
using System.Data;
using ZKTeco.Migrations;
using ZKTeco.Models;

namespace ZKTeco.Controllers
{
    public class BlockController : Controller
    {
        private readonly IConfiguration config;
        private readonly ZKTDbContext db;
        private IWebHostEnvironment HostEnvironment;
        private string ComId;
        private string DeviceName;
        private string dowloadOption;
        private string ViewOption;
        private readonly ILogger<EventManagement> _logger;

        public BlockController(IConfiguration config, ZKTDbContext db, IWebHostEnvironment hostEnvironment, ILogger<EventManagement> logger)
        {
            this.config = config;
            this.db = db;
            HostEnvironment = hostEnvironment;
            ComId = config.GetValue<string>("ComId");
            DeviceName = config.GetValue<string>("DeviceName");
            dowloadOption = config.GetValue<string>("dowloadOption");
            ViewOption = config.GetValue<string>("ViewOption");
            _logger = logger;
        }

        SDKHelper SDK = new SDKHelper();

        public IActionResult Index(string msg)
        {
            ViewBag.msg = msg;
           
            return View();
        }


        public IActionResult AbsentList(string processDate)
        {
         
            List<viewModel> viewModels = new List<viewModel>();

            var dbconfig = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json").Build();
            try
            {
                var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
                string sql = $"exec HR_RptAttendunnilever '{ComId}','{processDate}','{processDate}' ";
                using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 0;
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            viewModel vm = new viewModel();
                            vm.EmpCode = dataReader["EmpCode"].ToString();
                            vm.EmpName = dataReader["EmpName"].ToString();
                            vm.DeptName = dataReader["DeptName"].ToString();
                            vm.SectName = dataReader["SectName"].ToString();
                            vm.desigName = dataReader["DesigName"].ToString();
                            vm.userGroup = Convert.ToInt32(dataReader["AbTn"]);
                            viewModels.Add(vm);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(viewModels);

            
        }

            
        [HttpPost]
        public IActionResult BlockUser(List<viewModel> SelectedData)
        {



            try
            {
                List<viewModel> blockedList = new List<viewModel>();
                List<string> Ermsg = new List<string>();
                var deviceList = (from b in db.HR_MachineNoZkt where b.ComId == ComId select b).ToList();

                int DeviceCount = 0;
                string unableConnection = "";
                foreach (var dev in deviceList)
                {

                    int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);

                    if (ret == 1)
                    {
                        blockedList = new();
                         blockedList=SDK.BlockUser(SelectedData);
                            
                       SDK.axCZKEM1.RefreshData(1);  //the data in the device should be refreshed         
                          
                    }
                                          
                    else
                    {
                        SDK.axCZKEM1.EnableDevice(1, true);
                        unableConnection += "[" + dev.IpAddress + "];";
                        continue;
                    }
                    
                }
                foreach (var item in blockedList)
                {
                    Blocklist bl = new Blocklist();
                    bl.comId = ComId;
                    bl.empCode = item.EmpCode;
                    bl.empName = item.EmpName;
                    bl.blockdate = DateTime.Now;
                    bl.isBlock = true;

                    db.Blocklist.Add(bl);
                }
                db.SaveChanges();
                string errormsg = "";
                if (unableConnection != "")
                {
                    errormsg = unableConnection + "unable to connect";
                }
                return Json(new { data = blockedList.Count() + " - Employees blocked successfully!.", error = errormsg });

            }  //try
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        [HttpPost]
        public IActionResult UnBlockUser(List<viewModel> SelectedData)
        {



            try
            {
                List<viewModel> blockedList = new List<viewModel>();
                var deviceList = (from b in db.HR_MachineNoZkt where b.ComId == ComId select b).ToList();
                int DeviceCount = 0;
                string unableConnection = "";
                foreach (var dev in deviceList)
                {

                    int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);

                if (ret == 1)
                {


                    blockedList= SDK.UnBlockUser(SelectedData);
                    SDK.axCZKEM1.RefreshData(1);//the data in the device should be refreshed

                    foreach (var item in blockedList)
                    {
                        var exist= db.Blocklist.Where(w=>w.empCode==item.EmpCode && w.comId==ComId).OrderByDescending(o=>o.id).FirstOrDefault();
                        if (exist != null)
                        {
                            exist.unblockdate = DateTime.Now;
                            exist.isBlock = false;
                            db.Blocklist.Update(exist);
                        }
                    }
                    db.SaveChanges(true);   

                }

                    else
                    {
                        SDK.axCZKEM1.EnableDevice(1, true);
                        unableConnection += "[" + dev.IpAddress + "];";
                        continue;
                    }

                }
                string errormsg = "";
                if (unableConnection != "")
                {
                    errormsg = unableConnection + "unable to connect";
                }
                return Json(new { data = blockedList.Count() + " - Employees Unblocked successfully!.", error = errormsg });

            }  
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        [HttpGet]
        public IActionResult BlockedUserList()
        {
            
            var blockedList = db.Blocklist.Where(w=>w.comId==ComId && w.isBlock==true).ToList();

            return Json(blockedList);

        }

        [HttpPost]
        public async Task<IActionResult> UploadFromExcel(IFormFile file,int flag)
        {
         string msg = "";
            try
            {
                
                if (file != null)
                {
                    string fileLocation = Path.GetFullPath("wwwroot/Content/" + ComId );
                    if (Directory.Exists(fileLocation))
                    {
                        Directory.Delete(fileLocation, true);
                    }
                    string uploadlocation = Path.GetFullPath("wwwroot/Content/" + ComId +"/");

                    if (!Directory.Exists(uploadlocation))
                    {
                        Directory.CreateDirectory(uploadlocation);
                    }

                    string filePath = uploadlocation + Path.GetFileName(file.FileName);

                    string extension = Path.GetExtension(file.FileName);
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Close();

                    var data = this.GetDataFromExcel(file.FileName);
                    if (data.Count() > 0 && flag > 0)
                    {
                        if (flag == 1)
                        {
                            try
                            {
                                List<viewModel> blockedList = new List<viewModel>();
                                List<string> Ermsg = new List<string>();
                                var deviceList = (from b in db.HR_MachineNoZkt where b.ComId == ComId select b).ToList();

                                int DeviceCount = 0;
                                string unableConnection = "";
                                foreach (var dev in deviceList)
                                {

                                    int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);

                                    if (ret == 1)
                                    {
                                        blockedList = new();
                                        blockedList = SDK.BlockUser(data);

                                        SDK.axCZKEM1.RefreshData(1);  //the data in the device should be refreshed         

                                    }

                                    else
                                    {
                                        SDK.axCZKEM1.EnableDevice(1, true);
                                        unableConnection += "[" + dev.IpAddress + "];";
                                        continue;
                                    }

                                }
                                foreach (var item in blockedList)
                                {
                                    Blocklist bl = new Blocklist();
                                    bl.comId = ComId;
                                    bl.empCode = item.EmpCode;
                                    bl.empName = item.EmpName;
                                    bl.blockdate = DateTime.Now;
                                    bl.isBlock = true;

                                    db.Blocklist.Add(bl);
                                }
                                db.SaveChanges();
                                string errormsg = "";
                                if (unableConnection != "")
                                {
                                    errormsg = unableConnection + "unable to connect";
                                }
                                msg = blockedList.Count() + " - Employees blocked successfully!.and Device" + errormsg ;

                            }  
                            catch (Exception ex)
                            {
                                throw (ex);
                            }
                        }
                        if (flag == 2)
                        {
                            try
                            {
                                List<viewModel> blockedList = new List<viewModel>();
                                var deviceList = (from b in db.HR_MachineNoZkt where b.ComId == ComId select b).ToList();
                                int DeviceCount = 0;
                                string unableConnection = "";
                                foreach (var dev in deviceList)
                                {

                                    int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);

                                    if (ret == 1)
                                    {


                                        blockedList = SDK.UnBlockUser(data);
                                        SDK.axCZKEM1.RefreshData(1);//the data in the device should be refreshed

                                        foreach (var item in blockedList)
                                        {
                                            var exist = db.Blocklist.Where(w => w.empCode == item.EmpCode && w.comId == ComId).OrderByDescending(o => o.id).FirstOrDefault();
                                            if (exist != null)
                                            {
                                                exist.unblockdate = DateTime.Now;
                                                exist.isBlock = false;
                                                db.Blocklist.Update(exist);
                                            }
                                        }
                                        db.SaveChanges(true);

                                    }

                                    else
                                    {
                                        SDK.axCZKEM1.EnableDevice(1, true);
                                        unableConnection += "[" + dev.IpAddress + "];";
                                        continue;
                                    }

                                }
                                string errormsg = "";
                                if (unableConnection != "")
                                {
                                    errormsg = unableConnection + "unable to connect";
                                }
                                msg = blockedList.Count() + " - Employees Unblocked successfully!.and Device" + errormsg;

                            }
                            catch (Exception ex)
                            {
                                throw (ex);
                            }
                            

                        }

                    }
                    else if (data.Count() > 0 && flag == 0) {
                        msg = "plesase select what u want to do";
                    }
                    else
                    {
                        msg = ("Data couldn't be read from excel.Use 1st column as employee_Code and 2nd column as Name");
                    }
                }
                else
                {
                    msg = ( "Please, Select a excel file!");
                }


            }
            catch (Exception e)
            {
                throw e;
                msg = "Error Occured";
            }
            //Process();
            return RedirectToAction("Index", new { msg=msg});
        }



        private List<viewModel> GetDataFromExcel(string fName)
        {
            
            var filename = Path.GetFullPath("wwwroot/Content/" + ComId + "/" + fName);

            List<viewModel> empdata = new List<viewModel>();

            try
            {

                using (var stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var startSl = 0;
                        while (reader.Read())
                        {
                            startSl++;
                            if (startSl == 1)
                            {
                                continue;
                            }

                            else
                            {

                                empdata.Add(new viewModel()
                                {

                                    EmpCode = reader.GetValue(1).ToString(),
                                    EmpName = reader.GetValue(2).ToString(),

                                    
                                });
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
                _logger.LogError(e.InnerException.Message);
            }

            return empdata;
        }
    }
}
