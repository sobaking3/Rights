using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rights.DataFolder
{
    public partial class Staff
    {
        public string FullName => $"{MiddleName} {FirstName} {LastName}".Trim();

        // Where(x => x.FullName.Contains(text));
    }
}
