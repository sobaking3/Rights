using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rights.DataFolder
{
    public partial class DBEntities : DbContext
    {
        private static DBEntities context;

        public static DBEntities GetContext()
        {
            if (context == null)
            {
                context = new DBEntities();
#if DEBUG

                context.Database.Log = (s) => Debug.WriteLine(s);
#endif
            }
            return context;
        }
    }
}
