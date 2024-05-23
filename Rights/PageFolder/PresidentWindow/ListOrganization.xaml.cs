using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
using Rights.PageFolder.ManagerWindow;
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


namespace Rights.PageFolder.PresidentWindow
{
    /// <summary>
    /// Логика взаимодействия для ListOrganization.xaml
    /// </summary>
    public partial class ListOrganization : Page
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
        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as FrameworkElement).DataContext is Organizations organizations)
                {
                    if (organizations == null)
                    {
                        MBClass.ErrorMB("Организация не выбрана");
                    }
                    else
                    {
                        if (MBClass.QuestionMB($"Удалить отдел " +
                        $"с названием {organizations.NameOrganization}?"))
                        {
                            DBEntities.GetContext().Organizations.Remove(organizations);
                            DBEntities.GetContext().SaveChanges();
                            MBClass.InfoMB("Отдел удален");
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
        private void EditM1_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = sender as Grid;
            if ((sender as FrameworkElement).DataContext is Organizations organizations)
            {
                WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.EditOrganization(organizations));

                UpdateStaffList();
            }
        }

        private List<FrameworkElement> _onLoadingBlockingControls = new List<FrameworkElement>();
        public ListOrganization()
        {
            InitializeComponent();
            DataContext = this;
            _onLoadingBlockingControls.Add(SearchStaffByFullNameTb);
            _onLoadingBlockingControls.Add(AddOrganizationBtn);
        }

        private void UpdateStaffList()
        {
            var query = DBEntities.GetContext().Organizations.Select(x => x);

            if (!string.IsNullOrEmpty(_searchText))
            {
                query = query.Where(x => (x.NameOrganization).Contains(_searchText));
            }


            List<Organizations> result = query.ToList();

            StaffListItemsControl.ItemsSource = result;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStaffList();
        }

        private void AddOrganizationBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.AddOrganization());
            UpdateStaffList();
        }
    }
}
