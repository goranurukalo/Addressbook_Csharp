using AddressBook.Model;
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

namespace AddressBook
{
    /// <summary>
    /// Interaction logic for PersonDialog.xaml
    /// </summary>
    public partial class PersonDialog : Window
    {
        public Person Person { get; set; }
        public PersonDialog(Person person = null)
        {
            InitializeComponent();
            if (person == null)
                person = new Person();
            this.Person = person;
            this.DataContext = person;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //foreach (BindingExpression expression in BindingOperations.GetSourceUpdatingBindings(null))
            //    expression.UpdateSource();

            this.DialogResult = true;
        }
    }
}
