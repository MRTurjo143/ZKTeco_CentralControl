using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZKTeco.Models
{
    public class DbModel
    {
    }
    public class empData
    {
        public string? cardNo { get; set; }
        public string? empCode { get; set; }
        public string? empName { get; set; }
        public int? userGroup { get; set; }
        public int indexNo { get; set; }
        public string? finger_Data { get; set; }

    }
    public class viewModel
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? CardNo { get; set; }
        public string? ComId { get; set; }
        public string? desigName { get; set; }
        public string? floor { get; set; }
        public string? line { get; set; }
        public string? DeptName { get; set; }
        public string? SectName { get; set; }
        public string? IpAddress { get; set; }
        public byte[]? EmpImage { get; set; }
        public string? emp_image { get; set; }
        public byte[]? FingerData { get; set; }
        public string? finger_Data { get; set; }
        public int? indexNo { get; set; }
        public string sPassword { get; set; }
        public int? iPrivilege { get; set; }
        public bool? bEnabled { get; set; }
        public int? userGroup { get; set; }
        public string? weekend { get; set; }

    }

    public class EventViewModel
    {
        public string deviceNo { get; set; }
        public string cardNo { get; set; }

        public string date { get; set; }
        public string time { get; set; }
    }


    public class HR_MachineNoZKT
    {
        [Key]

        public Guid Id { get; set; }
        public string? ComId { get; set; }
        public string? Location { get; set; }
        public string? IsActive { get; set; }
        public string? PortNo { get; set; }
        public short? LuserId { get; set; }
        public string? Pcname { get; set; }
        public string? IpAddress { get; set; }
        public string? ZKtUser { get; set; }
        public string? ZKtPassword { get; set; }
        public string? Status { get; set; }
        public string? Inout { get; set; }

    }
    public class HR_ZktFinger_unilever
    {
        [Key]

        public Guid id { get; set; }
        public string? comId { get; set; }
        public string? empCode { get; set; }
        public string? empName { get; set; }
        public string? cardNo { get; set; }
        public string? empImage { get; set; }
        public string? fingerindex1 { get; set; }
        public string? fingerindex2 { get; set; }
        public string? fingerindex3 { get; set; }
        public string? fingerindex4 { get; set; }
        public string? fingerindex5 { get; set; }
        public string? fingerindex6 { get; set; }
        public string? fingerindex7 { get; set; }
        public string? fingerindex8 { get; set; }
        public string? fingerindex9 { get; set; }
        public string? fingerindex10 { get; set; }
        public string? DeviceName { get; set; }
        
        public long? totalIndex { get; set; }
        public bool isDelete { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        [NotMapped]
        public int? weekday { get; set; }

    }

    public class ZKTDataTranVM
    {
        
        public string? empCode { get; set; }
        public string? empName { get; set; }
        public string? cardNo { get; set; }
        public string? desigName { get; set; }
        public string? floor { get; set; }
        public string? line { get; set; }
        public string? deptName { get; set; }
        public string? sectName { get; set; }
        public string? fingerindex1 { get; set; }
        public string? fingerindex2 { get; set; }
        public string? fingerindex3 { get; set; }
        public string? fingerindex4 { get; set; }
        public string? fingerindex5 { get; set; }
        public string? fingerindex6 { get; set; }
        public string? fingerindex7 { get; set; }
        public string? fingerindex8 { get; set; }
        public string? fingerindex9 { get; set; }
        public string? fingerindex10 { get; set; }
        public int? totalIndex { get; set; }
        public int? weekday { get; set; }
        public string? dayName { get; set; }

    }

    public class HR_Emp_DeviceInfoHIK
    {
        [Key]
       
        public Guid id { get; set; }
        public string? comId { get; set; }
        public string? empCode { get; set; }
        public string? empName { get; set; }
        public string? cardNo { get; set; }
        public byte[]? empImage { get; set; }
        public byte[]? fingerData { get; set; }
        public string? IpAddress { get; set; }
        public string? DeviceName { get; set; }
        public long? userGroup { get; set; }

    }
    public class RTevevnt {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? empCode { get; set; }
        public string? State { get; set; }
        public string? verifystyle { get; set; }
        public DateTime date { get; set; }
        public string? time { get; set; }
        public string? remark { get; set; }
  

    }

    public class Blocklist
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? comId { get; set; }
        public string? empCode { get; set; }
        public string? empName { get; set; }
        public bool isBlock { get; set; }
        public DateTime? blockdate { get; set; }
        public DateTime? unblockdate { get; set; }
        public string? remark { get; set; }


    }

}
