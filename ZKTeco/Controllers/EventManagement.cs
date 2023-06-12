using Microsoft.AspNetCore.Mvc;
using StandaloneSDKDemo;
using System.Data;
using ZKTeco.Models;

namespace ZKTeco.Controllers
{
    public class EventManagement : Controller
    {
        private readonly IConfiguration config;
        private readonly ZKTDbContext db;
        private IWebHostEnvironment HostEnvironment;
        private string ComId;
        private string DeviceName;
        private string dowloadOption;
        private string ViewOption;
        private readonly ILogger<EventManagement> _logger;

        public EventManagement(IConfiguration config, ZKTDbContext db, IWebHostEnvironment hostEnvironment, ILogger<EventManagement> logger)
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

        public IActionResult Index()
        {
            ViewBag.option = dowloadOption;
            ViewBag.viewOption = ViewOption;
            return View();
        }


        [HttpPost]
        public IActionResult ipSet(string adress, string name, string port, string password, string location, string inout)
        {


            HR_MachineNoZKT mn = new HR_MachineNoZKT();
            mn.Id = new Guid();
            mn.IpAddress = adress;
            mn.ZKtUser = name;
            mn.Location = location;
            mn.ZKtPassword = password;
            mn.Inout = inout;
            mn.PortNo = "4370";
            mn.ComId = ComId;

            var device = (from d in db.HR_MachineNoZkt where d.IpAddress == adress && d.ComId == ComId select d).FirstOrDefault();
            if (device == null)
            {
                db.HR_MachineNoZkt.Add(mn);
                db.SaveChanges();
                HttpContext.Session.SetString("IPresult", "Saved to data base");

            }
            else
            {
                HttpContext.Session.SetString("IPresultExist", "already in data base");

            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult update(List<HR_MachineNoZKT> Device)
        {
            foreach (var dd in Device)
            {
                var device = (from d in db.HR_MachineNoZkt where d.ComId == ComId select d).FirstOrDefault();
                db.HR_MachineNoZkt.Remove(device);
                db.SaveChanges();
                HR_MachineNoZKT mn = new HR_MachineNoZKT();
                mn.Id = new Guid();
                mn.IpAddress = dd.IpAddress;
                mn.ZKtUser = dd.ZKtUser;
                mn.Location = dd.Location;
                mn.ZKtPassword = dd.ZKtPassword;
                mn.Inout = dd.Inout;
                mn.PortNo = "4370";
                mn.ComId = ComId;



                db.HR_MachineNoZkt.Add(mn);
                db.SaveChanges();


            }

            return Ok();
        }
        public IActionResult delete(List<HR_MachineNoZKT> Device)
        {
            foreach (var dd in Device)
            {
                var device = (from d in db.HR_MachineNoZkt where d.IpAddress == dd.IpAddress && d.ComId == ComId select d).FirstOrDefault();
                db.HR_MachineNoZkt.Remove(device);
                db.SaveChanges();
            }
            return Ok("Device deleted");
        }
        public IActionResult getEvent(string fdate, string tdate, List<HR_MachineNoZKT> Device)
        {
            try
            {
                int DeviceCount = 0;
                string unableConnection = "";


                List<EventViewModel> Evm = new List<EventViewModel>();
                foreach (var dev in Device)
                {
                    DeviceCount++;
                    int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);
                    EventViewModel Ev = new EventViewModel();

                    if (ret == 1)
                    {
                        var result = SDK.sta_readLogByPeriod(fdate, tdate);
                        foreach (var r in result)
                        {

                            r.deviceNo = dev.IpAddress;
                            Evm.Add(r);
                        }


                    }
                    else
                    {
                        SDK.axCZKEM1.EnableDevice(1, true);
                        unableConnection +="[" +dev.IpAddress+"];";
                        continue;
                    }

                }
                SDK.axCZKEM1.EnableDevice(1, true);
                string errormsg = "";
                if (unableConnection != "")
                {
                     errormsg = unableConnection + "unable to connect";
                }
               
                return Json(new { data=Evm,error=errormsg});

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Warning", ex.Message);
                _logger.LogError("Error", ex.InnerException);
                throw;
            }
        }
        [HttpPost]
        public IActionResult saveTxt(List<EventViewModel> fileData)
        {
            try
            {
                string FileLocation = @".\wwwroot\TextFile\";
                string FileName = DateTime.Now.ToString("dd-MMM-yyyy") + "_" +
                                   DateTime.Now.ToString("hh" + "'Hr'" + "-mm" + "'Min'");
                if (!System.IO.File.Exists(FileLocation + FileName + ".txt"))
                {
                    System.IO.File.Create(FileLocation + FileName + ".txt").Close();
                }
                StreamWriter sw = System.IO.File.CreateText(FileLocation + FileName + ".txt");
                foreach (EventViewModel row in fileData)
                {
                    string DeviceNo = row.deviceNo.Substring(row.deviceNo.Length - 3);
                    string CardNo = row.cardNo;
                    string pDate = row.date;
                    string pTime = row.time;
                    string pInfoByGTR_txt = DeviceNo + ":" + CardNo + ":" + pDate + ":" + pTime;
                    sw.WriteLine("{0}", pInfoByGTR_txt);
                } //for each
                sw.Close();
                return Json("Data Download successfully!.");

            }  //try
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        [HttpPost]
        public IActionResult clearEvent(List<HR_MachineNoZKT> Device)
        {
            List<EventViewModel> Evm = new List<EventViewModel>();
            foreach (var dev in Device)
            {

                int ret = SDK.sta_ConnectTCP(dev.IpAddress, dev.PortNo, dev.ZKtPassword);
                EventViewModel Ev = new EventViewModel();

                if (ret == 1)
                {
                    var result = SDK.axCZKEM1.ClearGLog(1);
                    SDK.axCZKEM1.EnableDevice(1, true);

                }
                else
                {
                    return Json("Connection failed");
                }

            }
            SDK.axCZKEM1.EnableDevice(1, true);
            return Json("Previous logs are deleted");
        }
    }
}
