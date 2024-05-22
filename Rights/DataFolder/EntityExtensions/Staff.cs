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
        public string NotFullName => $"{LastName} {FirstName[0]}." +
                (string.IsNullOrEmpty(MiddleName) ? string.Empty : MiddleName[0] + ".");
        // Where(x => x.FullName.Contains(text));
    }
}
