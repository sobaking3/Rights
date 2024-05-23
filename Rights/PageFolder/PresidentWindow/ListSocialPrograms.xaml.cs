using Rights.ClassFolder;
using Rights.DataFolder;
using Rights.Helpers;
using Rights.PageFolder.ManagerWindow;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rights.PageFolder.PresidentWindow
{
    /// <summary>
    /// Логика взаимодействия для ListSocialPrograms.xaml
    /// </summary>
    public partial class ListSocialPrograms : Page
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
        public ListSocialPrograms()
        {
            InitializeComponent();
            DataContext = this;
            _onLoadingBlockingControls.Add(SearchStaffByFullNameTb);
            _onLoadingBlockingControls.Add(AddSocialProgramsBtn);
        }

        private void UpdateStaffList()
        {
            var query = DBEntities.GetContext().SocialPrograms.Select(x => x);

            if (!string.IsNullOrEmpty(_searchText))
            {
                query = query.Where(x => (x.ProgramName).Contains(_searchText));
            }


            List<SocialPrograms> result = query.ToList();

            StaffListItemsControl.ItemsSource = result;
        }
        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as FrameworkElement).DataContext is SocialPrograms socialPrograms)
                {
                    if (socialPrograms == null)
                    {
                        MBClass.ErrorMB("Программа не выбрана");
                    }
                    else
                    {
                        if (MBClass.QuestionMB($"Удалить программу " +
                        $"с названием {socialPrograms.ProgramName}?"))
                        {
                            DBEntities.GetContext().SocialPrograms.Remove(socialPrograms);
                            DBEntities.GetContext().SaveChanges();
                            MBClass.InfoMB("Программа удалена");
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
            if ((sender as FrameworkElement).DataContext is SocialPrograms socialPrograms)
            {
                WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.EditSocialPrograms(socialPrograms));

                UpdateStaffList();
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStaffList();
        }

        private void AddSocialProgramsBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowDialogWithBlur(this, new PresidentWindow.AddSocialPrograms());
            UpdateStaffList();
        }
    }
}
