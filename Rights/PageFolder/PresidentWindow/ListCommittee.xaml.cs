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
    /// Логика взаимодействия для ListCommittee.xaml
    /// </summary>
    public partial class ListCommittee : Page
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
        public ListCommittee()
        {
            InitializeComponent();
            DataContext = this;
            _onLoadingBlockingControls.Add(SearchStaffByFullNameTb);
            _onLoadingBlockingControls.Add(AddCommitteeBtn);
        }

        private void UpdateStaffList()
        {
            var query = DBEntities.GetContext().Committee.Select(x => x);

            if (!string.IsNullOrEmpty(_searchText))
            {
                query = query.Where(x => (x.NameCommittee).Contains(_searchText));
            }


            List<Committee> result = query.ToList();

            StaffListItemsControl.ItemsSource = result;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStaffList();
        }

        private void AddCommitteeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.AddCommittee());
            UpdateStaffList();
        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as FrameworkElement).DataContext is Committee committee)
                {
                    if (committee == null)
                    {
                        MBClass.ErrorMB("Комитет не выбран");
                    }
                    else
                    {
                        if (MBClass.QuestionMB($"Удалить комитет " +
                        $"с названием {committee.NameCommittee}?"))
                        {
                            DBEntities.GetContext().Committee.Remove(committee);
                            DBEntities.GetContext().SaveChanges();
                            MBClass.InfoMB("Комитет удален");
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
            if ((sender as FrameworkElement).DataContext is Committee committee)
            {
                WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.EditCommittee(committee));

                UpdateStaffList();
            }
        }
    }
}
