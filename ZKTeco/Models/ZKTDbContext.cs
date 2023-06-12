using Microsoft.EntityFrameworkCore;

namespace ZKTeco.Models
{
    public class ZKTDbContext : DbContext
    {

        public ZKTDbContext(DbContextOptions<ZKTDbContext> options)
                    : base(options)
        {
        }
        public virtual DbSet<HR_Emp_DeviceInfoHIK> HR_Emp_DeviceInfoHIK { get; set; }
        public virtual DbSet<HR_MachineNoZKT> HR_MachineNoZkt { get; set; }
        public virtual DbSet<RTevevnt> RTevevnt { get; set; }
        public virtual DbSet<Blocklist> Blocklist { get; set; }
        public virtual DbSet<HR_ZktFinger_unilever> HR_ZktFinger_unilever { get; set; }
        //    public virtual DbSet<HrRawDatum> HR_RawData { get; set; }
        //
    }
}
