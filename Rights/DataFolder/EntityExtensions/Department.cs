using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Rights.DataFolder
{
    public partial class Departament
    {
        public bool DirectedSetted { get; set; } = false;

        [NotMapped]
        private Staff _director = null;
        [NotMapped]
        public Staff Director
        {
            get
            {
                if (_director == null && DirectedSetted == false)
                {
                    _director = DBEntities.GetContext().Staff.FirstOrDefault(x => x.IdStaff == IdStaff);
                    DirectedSetted = true;
                }
                return _director;
            }

        }



        // Вызываем когда мы обновили idStaff
        public void UpdateDirector()
        {
            _director = DBEntities.GetContext().Staff.FirstOrDefault(x => x.IdStaff == IdStaff);
        }
    }
}

