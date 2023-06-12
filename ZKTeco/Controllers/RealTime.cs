//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using StandaloneSDKDemo;
//using System.Configuration;
//using ZKTeco.Models;
//using Microsoft.JSInterop;

//namespace ZKTeco.Controllers
//{
//    public class RealTime : Controller
//    {
//        private readonly IConfiguration config;
//        private readonly ZKTDbContext _db;
//        private IWebHostEnvironment HostEnvironment;
//        private string ComId;
//        private string DeviceName;
//        private string ViewOption;

//        public List<RTevevnt> lists;


//        public RealTime(IConfiguration config, ZKTDbContext db, IWebHostEnvironment hostEnvironment)
//        {
//            this.config = config;
//            _db = db;
//            HostEnvironment = hostEnvironment;
//            ComId = config.GetValue<string>("ComId");
//            DeviceName = config.GetValue<string>("DeviceName");
//            ViewOption = config.GetValue<string>("ViewOption");
//        }


//        SDKHelper SDK = new SDKHelper();
//        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
//        List<RTevevnt> rtl = new List<RTevevnt>();
//        private List<string> gRealEventListBox = new List<string>();
//        string Conect = "";
//        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
//        private int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.

//        //If your device supports the TCP/IP communications, you can refer to this.
//        //when you are using the tcp/ip communication,you can distinguish different devices by their IP address.
//        private void btnConnect_Click(object sender, EventArgs e)
//        {
            
//            int idwErrorCode = 0;

          
//            if (bIsConnected == true)
//            {
//                axCZKEM1.Disconnect();

//                this.axCZKEM1.OnFinger -= new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
//                this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
//                this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
//                this.axCZKEM1.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
//                this.axCZKEM1.OnEnrollFingerEx -= new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
//                this.axCZKEM1.OnDeleteTemplate -= new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
//                this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
//                this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
//                this.axCZKEM1.OnAlarm -= new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
//                this.axCZKEM1.OnDoor -= new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
//                this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
//                this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);

//                bIsConnected = false;
               
//                return;
//            }

         
//            if (bIsConnected == true)
//            {
              
//                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.

//                //this.rtTimer.Enabled = true;
//                if (axCZKEM1.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
//                {
//                    this.axCZKEM1.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
//                    this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
//                    this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
//                    this.axCZKEM1.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
//                    this.axCZKEM1.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
//                    this.axCZKEM1.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
//                    this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
//                    this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
//                    this.axCZKEM1.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
//                    this.axCZKEM1.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
//                    this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
//                    this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
//                }

//            }
//            else
//            {
//                axCZKEM1.GetLastError(ref idwErrorCode);
                
//            }
            
//        }
//        public IActionResult RTIndex()
//        {

//            return View();
//        }

//        [HttpPost]
//        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
//        [RequestSizeLimit(int.MaxValue)]
//        public IActionResult GetRTInfo(List<HR_MachineNoZKT> Device)
//        {
            
//            List<viewModel> vm = new List<viewModel>();
//            var count = 0;
//            foreach (var d in Device)
//            {
                
                
//                if( axCZKEM1.Connect_Net(d.IpAddress, Convert.ToInt32(d.PortNo))==true)
//                {
//                    if (Conect != "Connected")
//                    {
//                        sta_RegRealTime();
                  
//                        count++;
//                        Conect = count+" device Connected";
//                    }
                  
                    
//                }
//                else
//                {
//                    Conect = "Not Connected";
//                }
//            }
//            return Json(Conect);
//        }

//        [HttpPost]
//        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
//        [RequestSizeLimit(int.MaxValue)]
//        public IActionResult DisconectRT(List<HR_MachineNoZKT> Device)
//        {

//            List<viewModel> vm = new List<viewModel>();
//            var count = 0;
//            foreach (var d in Device)
//            {
                

//                    if (Conect != "DisConnected")
//                    {
//                         axCZKEM1.Disconnect();
//                        sta_UnRegRealTime();
                        
//                        count++;
//                        Conect = count + " device DisConnected";
//                    }

                

             
//            }
//            return Json(Conect);
//        }
//        #region RealTimeEvent

   

//        public void sta_UnRegRealTime()
//        {

//            this.axCZKEM1.OnFinger -= new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
//            this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
//            this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
//            this.axCZKEM1.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
//            this.axCZKEM1.OnEnrollFingerEx -= new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
//            this.axCZKEM1.OnDeleteTemplate -= new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
//            this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
//            this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
//            this.axCZKEM1.OnAlarm -= new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
//            this.axCZKEM1.OnDoor -= new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
//            this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
//            this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);

//        }

//        public int sta_RegRealTime()
//        {
                 

//            if (axCZKEM1.RegEvent(1, 1))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
//            {
//                //common interface
//                this.axCZKEM1.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);

//                this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
//                this.axCZKEM1.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
//                this.axCZKEM1.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
//                this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
//                this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
//                this.axCZKEM1.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
//                this.axCZKEM1.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);

//                //only for color device
//                this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
//                this.axCZKEM1.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);

//                ////only for black&white device
//                //this.axCZKEM1.OnAttTransaction -= new zkemkeeper._IZKEMEvents_OnAttTransactionEventHandler(axCZKEM1_OnAttTransaction);
//                //this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
//                //this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
//                //this.axCZKEM1.OnKeyPress += new zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(axCZKEM1_OnKeyPress);
//                //this.axCZKEM1.OnEnrollFinger += new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(axCZKEM1_OnEnrollFinger);


                
//            }
//            else
//            {
//                //axCZKEM1.GetLastError(ref idwErrorCode);
//                //ret = idwErrorCode;

//                //if (idwErrorCode != 0)
//                //{
//                //    lblOutputInfo.Add("*RegEvent failed,ErrorCode: " + idwErrorCode.ToString());
//                //}
//                //else
//                //{
//                //    lblOutputInfo.Add("*No data from terminal returns!");
//                //}
//            }
//            return 0;
//        }

//        //When you are enrolling your finger,this event will be triggered.
//        void axCZKEM1_OnEnrollFingerEx(string EnrollNumber, int FingerIndex, int ActionResult, int TemplateLength)
//        {
//            if (ActionResult == 0)
//            {
//                gRealEventListBox.Add("Enroll finger succeed. UserID=" + EnrollNumber.ToString() + "...FingerIndex=" + FingerIndex.ToString());
//            }
//            else
//            {
//                gRealEventListBox.Add("Enroll finger failed. Result=" + ActionResult.ToString());
//            }
//            throw new NotImplementedException();
//        }

//        //Door sensor event
//        void axCZKEM1_OnDoor(int EventType)
//        {
//            gRealEventListBox.Add("Door opened" + "...EventType=" + EventType.ToString());

//            throw new NotImplementedException();
//        }

//        //When the dismantling machine or duress alarm occurs, trigger this event.
//        void axCZKEM1_OnAlarm(int AlarmType, int EnrollNumber, int Verified)
//        {
//            gRealEventListBox.Add("Alarm triggered" + "...AlarmType=" + AlarmType.ToString() + "...EnrollNumber=" + EnrollNumber.ToString() + "...Verified=" + Verified.ToString());

//            throw new NotImplementedException();
//        }

//        //When you swipe a card to the device, this event will be triggered to show you the card number.
//        void axCZKEM1_OnHIDNum(int CardNumber)
//        {
//            gRealEventListBox.Add("Card event" + "...Cardnumber=" + CardNumber.ToString());

//            throw new NotImplementedException();
//        }

//        //When you have enrolled a new user,this event will be triggered.
//        void axCZKEM1_OnNewUser(int EnrollNumber)
//        {

//            gRealEventListBox.Add("Enroll a　new user" + "...UserID=" + EnrollNumber.ToString());

//            throw new NotImplementedException();
//        }

//        //When you have deleted one one fingerprint template,this event will be triggered.
//        void axCZKEM1_OnDeleteTemplate(int EnrollNumber, int FingerIndex)
//        {
//            gRealEventListBox.Add("Delete a finger template" + "...UserID=" + EnrollNumber.ToString() + "..FingerIndex=" + FingerIndex.ToString());

//            throw new NotImplementedException();
//        }

//        //When you have enrolled your finger,this event will be triggered and return the quality of the fingerprint you have enrolled
//        void axCZKEM1_OnFingerFeature(int Score)
//        {
//            gRealEventListBox.Add("Press finger score=" + Score.ToString());

//            //throw new NotImplementedException();
//        }

//        //If your fingerprint(or your card) passes the verification,this event will be triggered,only for color device
//        public void axCZKEM1_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
//        {
//            string date = Year + "-" + Month + "-" + Day;
//            string time= Hour + ":" + Minute + ":" + Second;

//            //gRealEventListBox.Add("Verify OK.UserID=" + EnrollNumber + " isInvalid=" + IsInValid.ToString() + " state=" + AttState.ToString() + " verifystyle=" + VerifyMethod.ToString() + " time=" + time);
//            gRealEventListBox.Add("Verify OK.UserID=" + EnrollNumber.ToString() + ";" + " time=" + time);
//            RTevevnt rt=  new RTevevnt();
//            rt.empCode = EnrollNumber;
//            rt.verifystyle = "finger";
//            rt.State = "Ok";
//            rt.date = Convert.ToDateTime(date);
//            rt.time = time;
//            rt.remark = "";

          

//            //  SqlConnection sqlConnection = null;
//            //  SqlCommand command = null;
//            //  var dbconfig = new ConfigurationBuilder()
//            //.SetBasePath(Directory.GetCurrentDirectory())
//            //.AddJsonFile("appsettings.json").Build();
//            //  var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];

//            //    string query = $"INSERT INTO RTevevnt VALUES ('{EnrollNumber}','ok','finger','{time}','','{Convert.ToDateTime(date)}')";

//            //    try
//            //    {
//            //        using (sqlConnection = new(dbconnectionStr))
//            //        {
//            //            sqlConnection?.Open();
//            //            command = new(query, sqlConnection);
//            //            command.ExecuteNonQuery();
//            //        };
//            //    }
//            //    catch (Exception ex)
//            //    {

//            //        throw ex;
//            //    }
//            //    finally
//            //    {

//            //    }

//            //RT_Log(rt);


//            //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:FUNCTIONNAME(); ", true);

//            //HomeController home = new();

//            //home.TestMethod();

//            //throw new NotImplementedException();
//        }

//        //If your fingerprint(or your card) passes the verification,this event will be triggered,only for black%white device
//        private void axCZKEM1_OnAttTransaction(int EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second)
//        {
//            string time = Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute + ":" + Second;
//            //gRealEventListBox.Add("Verify OK.UserID=" + EnrollNumber.ToString() + " isInvalid=" + IsInValid.ToString() + " state=" + AttState.ToString() + " verifystyle=" + VerifyMethod.ToString() + " time=" + time);


//            //throw new NotImplementedException();
//        }

//        //After you have placed your finger on the sensor(or swipe your card to the device),this event will be triggered.
//        //If you passes the verification,the returned value userid will be the user enrollnumber,or else the value will be -1;
//        void axCZKEM1_OnVerify(int UserID)
//        {
//            if (UserID != -1)
//            {
//                gRealEventListBox.Add("User fingerprint verified... UserID=" + UserID.ToString());
//            }
//            else
//            {
//                gRealEventListBox.Add("Failed to verify... ");
//            }

//            //throw new NotImplementedException();
//        }

//        //When you place your finger on sensor of the device,this event will be triggered
//        void axCZKEM1_OnFinger()
//        {
//            gRealEventListBox.Add("OnFinger event ");

//            //throw new NotImplementedException();
//        }

//        //When you have written into the Mifare card ,this event will be triggered.
//        void axCZKEM1_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
//        {
//            if (iActionResult == 0)
//            {
//                gRealEventListBox.Add("Write Mifare Card OK" + "...EnrollNumber=" + iEnrollNumber.ToString() + "...TmpLength=" + iLength.ToString());
//            }
//            else
//            {
//                gRealEventListBox.Add("...Write Failed");
//            }
//        }

//        //When you have emptyed the Mifare card,this event will be triggered.
//        void axCZKEM1_OnEmptyCard(int iActionResult)
//        {
//            if (iActionResult == 0)
//            {
//                gRealEventListBox.Add("Empty Mifare Card OK...");
//            }
//            else
//            {
//                gRealEventListBox.Add("Empty Failed...");
//            }
//        }

//        //When you press the keypad,this event will be triggered.
//        void axCZKEM1_OnKeyPress(int iKey)
//        {
//            gRealEventListBox.Add("RTEvent OnKeyPress Has been Triggered, Key: " + iKey.ToString());
//        }

//        //When you are enrolling your finger,this event will be triggered.
//        void axCZKEM1_OnEnrollFinger(int EnrollNumber, int FingerIndex, int ActionResult, int TemplateLength)
//        {
//            if (ActionResult == 0)
//            {
//                gRealEventListBox.Add("Enroll finger succeed. UserID=" + EnrollNumber + "...FingerIndex=" + FingerIndex.ToString());
//            }
//            else
//            {
//                gRealEventListBox.Add("Enroll finger failed. Result=" + ActionResult.ToString());
//            }
//            //throw new NotImplementedException();
//        }

//        #endregion
//        //public IActionResult RTView() {
            
//        //        return View("RT_Log", "RealTime", rt);
//        //}

//        public IActionResult RT_Log(RTevevnt rt) {


//            //List<RTevevnt> RTModels = new List<RTevevnt>();
//            //var dbconfig = new ConfigurationBuilder()
//            //.SetBasePath(Directory.GetCurrentDirectory())
//            //.AddJsonFile("appsettings.json").Build();
//            //try
//            //{
//            //    var date = DateTime.Now.Date;
//            //    var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
//            //    string sql = $"exec prcGetEvent'{ComId}', {date}";
//            //    using (SqlConnection connection = new SqlConnection(dbconnectionStr))
//            //    {
//            //        SqlCommand command = new SqlCommand(sql, connection);
//            //        command.CommandTimeout = 0;
//            //        connection.Open();
//            //        using (SqlDataReader dataReader = command.ExecuteReader())
//            //        {
//            //            while (dataReader.Read())
//            //            {
//            //                RTevevnt vm = new RTevevnt();
//            //                vm.empCode = dataReader["EmpCode"].ToString();
//            //                vm.State = dataReader["CardNo"].ToString();
//            //                vm.verifystyle = dataReader["EmpName"].ToString();
//            //                vm.date = Convert.ToDateTime(dataReader["DeptName"].ToString());
//            //                vm.time = dataReader["SectName"].ToString();
//            //                vm.remark = dataReader["DesigName"].ToString();

//            //                RTModels.Add(vm);

//            //            }
//            //        }
//            //    }
//            //}
//            //catch (Exception ex)
//            //{
//            //    throw;
//            //}
//            return View(rt);

            
        
        
//        }
//    }
    
//}
