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
using StudentApp.BLL;
using StudentApp.BOL;

namespace StudentApp
{
    
    public partial class MainWindow : Window
    {
        private StudentBll _bll;
        public MainWindow()
        {
            InitializeComponent();
            _bll = new StudentBll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DtgStudents.ItemsSource = _bll.GetAll();
            }
            catch (Exception exception)
            {

                MessageBox.Show($"Could not get the students. {exception.Message}");
            }

            
        }

        private void DtgStudents_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DtgStudents.SelectedItem == null)
                return;
            Student studentFromDb = null;

            var selectedStudent = DtgStudents.SelectedItem as Student;
            try
            {
                studentFromDb = _bll.Get(selectedStudent.Id);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Could not get the specified student. {exception.Message}");
            }
            
            TxtFirstName.Text = studentFromDb.FirstName;
            TxtLastName.Text = studentFromDb.LastName;
            TxtMajor.Text = studentFromDb.Major;
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            if (DtgStudents.SelectedItem == null)
                return;
            var student = DtgStudents.SelectedItem as Student;

            student.FirstName = TxtFirstName.Text;
            student.LastName = TxtLastName.Text;
            student.Major = TxtMajor.Text;
            try
            {
                _bll.Update(student.Id, student);
            }
            catch(Exception exception)
            {
                MessageBox.Show($"Could not make the desired changes. {exception.Message}");
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this???????", "Confirmation",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (DtgStudents.SelectedItem == null)
                        MessageBox.Show("You have not selected any item.");
                    var student = DtgStudents.SelectedItem as Student;

                    _bll.Delete(student.Id);
                    DtgStudents.ItemsSource = _bll.GetAll();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Could not delete the student. {exception.Message}");
            }
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            var student = new Student()
            {
                FirstName = TxtFirstName.Text,
                LastName = TxtLastName.Text,
                Major = TxtMajor.Text
            };
            try
            {
                _bll.Create(student);
                DtgStudents.ItemsSource = _bll.GetAll();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Could not create the student. {exception.Message}");
            }
        }
    }
}
