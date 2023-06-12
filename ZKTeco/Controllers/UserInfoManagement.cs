using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StandaloneSDKDemo;
using System.Text;
using ZKTeco.Models;

namespace ZKTeco.Controllers
{
    public class UserInfoManagement : Controller
    {
        private readonly IConfiguration config;
        private readonly ZKTDbContext db;
        private IWebHostEnvironment HostEnvironment;
        private string ComId;
        private string DeviceName;
        private string ViewOption;
        private string ClearOption;

        public UserInfoManagement(IConfiguration config, ZKTDbContext db, IWebHostEnvironment hostEnvironment)
        {
            this.config = config;
            this.db = db;
            HostEnvironment = hostEnvironment;
            ComId = config.GetValue<string>("ComId");
            DeviceName = config.GetValue<string>("DeviceName");
            ViewOption = config.GetValue<string>("ViewOption");
            ClearOption = config.GetValue<string>("ClearOption");
        }

        SDKHelper SDK = new SDKHelper();

        private void getDeviceInfo()
        {
            string sFirmver = "";
            string sMac = "";
            string sPlatform = "";
            string sSN = "";
            string sProductTime = "";
            string sDeviceName = "";
            int iFPAlg = 0;
            int iFaceAlg = 0;
            string sProducter = "";

            SDK.sta_GetDeviceInfo(out sFirmver, out sMac, out sPlatform, out sSN, out sProductTime, out sDeviceName, out iFPAlg, out iFaceAlg, out sProducter);

        }

        private void getCapacityInfo()
        {
            int adminCnt = 0;
            int userCount = 0;
            int fpCnt = 0;
            int recordCnt = 0;
            int pwdCnt = 0;
            int oplogCnt = 0;
            int faceCnt = 0;
            SDK.sta_GetCapacityInfo(out adminCnt, out userCount, out fpCnt, out recordCnt, out pwdCnt, out oplogCnt, out faceCnt);

        }
        public IActionResult Index()
        {
            ViewBag.option = ViewOption;
            ViewBag.clear = ClearOption;
            return View();
        }


        #region GetUesrInfo

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public IActionResult GetAllUserInfo(List<HR_MachineNoZKT> Device)
        {
            try
            {
                List<HR_ZktFinger_unilever> vm = new();
                foreach (var d in Device)
                {


                    int ret = SDK.sta_ConnectTCP(d.IpAddress, d.PortNo, d.ZKtPassword);

                    if (ret == 1)
                    {
                        var data = SDK.GetAllUserFPInfo();
                        vm.AddRange(data);
                    }
                    else
                    {
                        return Json("DisConnected");
                    }
                }
                return Json(vm);
            }
            catch (Exception ex)
            {

                return Json($"Unable to retrive data due to {ex.InnerException}");
            }
            
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public IActionResult SaveAllUserInfo(HR_MachineNoZKT Device, List<HR_ZktFinger_unilever> SelectedData)
        {
            try
            {
                  var dataCount = 0;
            List<HR_ZktFinger_unilever> EmpInfoList = new List<HR_ZktFinger_unilever>();
            List<string> Ermsg = new List<string>();
            List<string> SucIp = new List<string>();

            foreach (var dd in SelectedData)
            {

                    dd.comId = ComId;
                    dd.DeviceName = DeviceName;
                    dd.createdAt=DateTime.Now;

                var dbdata = (from d in db.HR_ZktFinger_unilever where d.empCode == dd.empCode && d.comId == ComId  select d).FirstOrDefault();


                if (dbdata == null)
                {
                    EmpInfoList.Add(dd);

                }
                else
                { 
                        dbdata.cardNo=dd.cardNo;
                        dbdata.empName = dd.empName;
                        dbdata.fingerindex1 = dd.fingerindex1;
                        dbdata.fingerindex2 = dd.fingerindex2;
                        dbdata.fingerindex3 = dd.fingerindex3;
                        dbdata.fingerindex4 = dd.fingerindex4;
                        dbdata.fingerindex5 = dd.fingerindex5;
                        dbdata.fingerindex6 = dd.fingerindex6;
                        dbdata.fingerindex7 = dd.fingerindex7;
                        dbdata.fingerindex8 = dd.fingerindex8;
                        dbdata.fingerindex9 = dd.fingerindex9;
                        dbdata.fingerindex10 = dd.fingerindex10;
                        dbdata.totalIndex = dd.totalIndex;
                        dbdata.updatedAt= DateTime.Now;
                        

                        db.HR_ZktFinger_unilever.Update(dbdata);
                        db.SaveChanges();
                        dataCount++;


                }
            }
            if (EmpInfoList.Count > 0)
            {
                db.HR_ZktFinger_unilever.AddRange(EmpInfoList);
                db.SaveChanges();

            }
                return Json(EmpInfoList.Count() + " Data are Added Successfully and " + dataCount + " are Updated");
            }
            catch (Exception ex)
            {

                return Json($"Unable to save data due to {ex.InnerException}");
            }
          







            


        }

        [HttpGet]
        public IActionResult Empinfo()
        {
            List<ZKTDataTranVM> viewModels = new List<ZKTDataTranVM>();
            var dbconfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
            try
            {
                var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
                string sql = $"exec [prcGetDevicesDataTranZKT]'{ComId}'";
                using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 0;
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ZKTDataTranVM vm = new ZKTDataTranVM();
                            vm.empCode =dataReader["empCode"] == null ? null : dataReader["empCode"].ToString();
                            vm.cardNo = dataReader["cardNo"] == null ? null :dataReader["cardNo"].ToString();
                            vm.empName = dataReader["empName"] == null ? null : dataReader["empName"].ToString();
                            vm.deptName = dataReader["DeptName"] == null ? null :dataReader["DeptName"].ToString();
                            vm.sectName = dataReader["SectName"] == null ? null :dataReader["SectName"].ToString();
                            vm.desigName = dataReader["DesigName"] == null ? null : dataReader["DesigName"].ToString();
                            vm.floor =dataReader["Floor"] == null ? null : dataReader["Floor"].ToString();
                            vm.line = dataReader["Line"] == null ? null : dataReader["Line"].ToString();
                            
                            vm.fingerindex1 = dataReader["fingerindex1"]==null?null : dataReader["fingerindex1"].ToString();
                            vm.fingerindex2 =dataReader["fingerindex2"]==null?null : dataReader["fingerindex2"].ToString();
                            vm.fingerindex3 =dataReader["fingerindex3"]==null?null : dataReader["fingerindex3"].ToString();
                            vm.fingerindex4 =dataReader["fingerindex4"]==null?null : dataReader["fingerindex4"].ToString();
                            vm.fingerindex5 =dataReader["fingerindex5"]==null?null : dataReader["fingerindex5"].ToString();
                            vm.fingerindex6 =dataReader["fingerindex6"]==null?null : dataReader["fingerindex6"].ToString();
                            vm.fingerindex7 =dataReader["fingerindex7"]==null?null : dataReader["fingerindex7"].ToString();
                            vm.fingerindex8 =dataReader["fingerindex8"]==null?null : dataReader["fingerindex8"].ToString();
                            vm.fingerindex9 =dataReader["fingerindex9"]==null?null : dataReader["fingerindex9"].ToString();
                            vm.fingerindex10= dataReader["fingerindex10"]== null ? null : dataReader["fingerindex10"].ToString();
                            vm.totalIndex = dataReader["totalIndex"] == DBNull.Value ? (int?)null : Convert.ToInt32(dataReader["totalIndex"]);

                            viewModels.Add(vm);

                        }
                    }
                }
                return Json(viewModels);
            }
            catch (Exception ex)
            {
                return Json($"Unable to retrive data due to {ex.InnerException}");
            }
            
        }

        [HttpGet]
        public IActionResult EmpWeekday()
        {
            List<ZKTDataTranVM> viewModels = new();
            var dbconfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
            try
            {
                var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
                string sql = $"exec prcGetWeekday'{ComId}', 0";
                using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 0;
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ZKTDataTranVM vm = new();
                            vm.empCode = dataReader["EmpCode"].ToString();
                            vm.cardNo = dataReader["CardNo"].ToString();
                            vm.empName = dataReader["EmpName"].ToString();
                            vm.deptName = dataReader["DeptName"].ToString();
                            vm.sectName = dataReader["SectName"].ToString();
                            vm.desigName = dataReader["DesigName"].ToString();
                            vm.floor = dataReader["Floor"].ToString();
                            vm.line = dataReader["Line"].ToString();
                      
                            vm.dayName = dataReader["weekend"].ToString();
                            vm.weekday = (int)dataReader["userGroup"];

                           
                            viewModels.Add(vm);

                        }
                    }
                }
                return Json(viewModels);
            }
            catch (Exception ex)
            {
                return Json($"Unable to retrive data due to {ex.InnerException}");
            }
          
        }

        #endregion
        #region SetUserInfo
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public IActionResult SetFingerPrint(List<ZKTDataTranVM> SelectedData, List<HR_MachineNoZKT> Device)
        {
            try
            {
                List<string> Ermsg = new List<string>();
                List<viewModel> data = new List<viewModel>();
                int result = 0;
                // old code. 

               
                var deviceCount = 0;
                foreach (var dev in Device)
                {

                    int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);

                    if (ret == 1)
                    {
                        result = SDK.sta_SetAllUserFPInfo(SelectedData);
                        deviceCount++;
                    }
                    else
                    {
                        var er = "Device Ip " + dev.IpAddress + "is failed to connect";
                        Ermsg.Add(er);
                        continue;
                    }

                }

                return Json("Data Sucessfully set to [ " + deviceCount + "] devices");
            }
            catch (Exception ex)
            {

                return Json($"Unable to set data due to {ex.InnerException}");
            }
           



        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public IActionResult sta_SetSuperAdmin(List<empData> SelectedData, List<HR_MachineNoZKT> Device)
        {
            try
            {
                List<string> Ermsg = new List<string>();
                List<viewModel> data = new List<viewModel>();
                int result = 0;
                // old code. 

                foreach (var a in SelectedData)
                {
                    viewModel vm = new viewModel();
                    vm.EmpCode = a.empCode;
                    vm.CardNo = a.cardNo;
                    vm.EmpName = a.empName;
                    vm.userGroup = a.userGroup;

                    if (a.empCode != null)
                    {
                        var dbdata = (from d in db.HR_Emp_DeviceInfoHIK where d.empCode == a.empCode && d.comId == ComId select d).FirstOrDefault();

                        if (dbdata != null)
                        {
                            vm.emp_image = dbdata.empImage == null ? "" : Convert.ToBase64String(dbdata.empImage);
                            vm.finger_Data = dbdata.fingerData == null ? "" : Convert.ToBase64String(dbdata.fingerData);
                        }
                    }

                    data.Add(vm);
                }
                var deviceCount = 0;
                foreach (var dev in Device)
                {

                    int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);

                    if (ret == 1)
                    {
                        result = SDK.sta_SetSuperAdmin(data);
                        deviceCount++;
                    }
                    else
                    {
                        var er = "Device Ip " + dev.IpAddress + "is failed to connect";
                        Ermsg.Add(er);
                        continue;
                    }

                }

                return Json("Data Sucessfully set to [ " + deviceCount + "] devices");

            }
            catch (Exception ex)
            {


                return Json($"Unable to set data due to {ex.InnerException}");
            }
           


        }

        public IActionResult DltUserInfo(List<HR_MachineNoZKT> Device)
        {

            var deviceCount = 0;
            foreach (var dev in Device)
            {

                int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);

                if (ret == 1)
                {
                    if (SDK.axCZKEM1.ClearData(1, 5))
                    {
                        deviceCount++;
                        continue;
                    };

                }
                else
                {
                    return Json("Connection failed");
                }

            }

            return Json("Data have been deleted from " + deviceCount + "devices");
        }

        public IActionResult DltSingleUserInfo(List<HR_MachineNoZKT> Device, List<empData> SelectedData)
        {
            try
            {
                var deviceCount = 0;
                foreach (var dev in Device)
                {

                    int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);

                    if (ret == 1)
                    {
                        foreach (var user in SelectedData)
                        {
                            if (SDK.axCZKEM1.SSR_DeleteEnrollData(1, user.empCode, 12))
                            {
                                SDK.axCZKEM1.RefreshData(1);//the data in the device should be refreshed

                                deviceCount++;
                                continue;
                            };
                        }

                    }
                    else
                    {
                        return Json("Connection failed");
                    }

                }

                return Json("Data have been deleted from " + deviceCount + "devices");
            }
            catch (Exception ex)
            {

                return Json($"Unable to delete data due to {ex.InnerException}");
            }
           
        }
        #endregion
        
        #region GetDeviceInfo
        [HttpGet]
        public IActionResult deviceInfo()
        {

            var a = (from b in db.HR_MachineNoZkt where b.ComId == ComId select b).ToList();
            return Json(a);
        }

        [HttpGet]
        public IActionResult databaseData()
        {

            var a = (from b in db.HR_ZktFinger_unilever where b.comId == ComId select b).ToList();
            return Json(a);
        }



        #endregion




    }
}
