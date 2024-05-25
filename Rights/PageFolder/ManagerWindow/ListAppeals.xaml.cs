using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
using Rights.PageFolder.ManagerWindow;
using Rights.PageFolder.WindowForAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Rights.PageFolder.ManagerWindow
{
    /// <summary>
    /// Логика взаимодействия для ListAppeals.xaml
    /// </summary>
    public partial class ListAppeals : Page
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (!object.Equals(value, _searchText))
                {
                    _searchText = value;
                    UpdateStaffList();
                }

            }
        }

        private List<FrameworkElement> _onLoadingBlockingControls = new List<FrameworkElement>();
        public ListAppeals()
        {
            InitializeComponent();
            DataContext = this;
            _onLoadingBlockingControls.Add(SearchStaffByFullNameTb);
            _onLoadingBlockingControls.Add(AddAppealsBtn);
        }

        private void UpdateStaffList()
        {
            var query = DBEntities.GetContext().AppealsAndComplaints.AsQueryable();

            Staff staff = App.CurrentStaff;

            // Фильтруем жалобы, чтобы оставить только те, которые написал текущий сотрудник
            query = query.Where(x => x.Staff.IdStaff == staff.IdStaff);

            query = query.Where(x => x.IdStatus != 9);



            if (!string.IsNullOrEmpty(_searchText))
            {
                query = query.Where(x => (x.Staff.MiddleName + " " + x.Staff.FirstName + " " + x.Staff.LastName).Contains(_searchText));
            }


            List<AppealsAndComplaints> result = query.ToList();

            StaffListItemsControl.ItemsSource = result;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStaffList();
        }

        private void AddAppealsBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowDialogWithBlur(this, new WindowForAll.AddAppeal());
            UpdateStaffList();
        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as FrameworkElement).DataContext is AppealsAndComplaints appealsAndComplaints)
                {
                    if (appealsAndComplaints == null)
                    {
                        MBClass.ErrorMB("Жалоба не выбрана");
                    }
                    else
                    {
                        if (MBClass.QuestionMB($"Удалить эту жалобу?"))
                        {
                            appealsAndComplaints.IdStatus = 9;
                            DBEntities.GetContext().SaveChanges();
                            MBClass.InfoMB("Жалоба удалена");
                            UpdateStaffList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
    }
}
