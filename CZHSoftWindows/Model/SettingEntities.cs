using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace CZHSoftWindows.Model
{
    public class SettingEntities : DbContext
    {
        public DbSet<FlowData> Flows { get; set; }
        public DbSet<DicData> Dics { get; set; }
    }
}
