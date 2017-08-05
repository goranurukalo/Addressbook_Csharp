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
using AddressBook.Model;
using System.ComponentModel;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace AddressBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListCollectionView personsView;

        AddressBook.Model.AddressBook addressBook = new AddressBook.Model.AddressBook();
        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = addressBook; 

            FillAddressBook(addressBook);
            SetupAddressBookDefaultViewCollection();
            
        }

        private void SetupAddressBookDefaultViewCollection()
        {
            ICollectionView iPersonsView = CollectionViewSource.GetDefaultView(addressBook.Persons);
            personsView = iPersonsView as ListCollectionView;

            personsView.Filter = PersonsFilter;
            personsView.IsLiveFiltering = true;
            personsView.LiveFilteringProperties.Add("Name");
        }

        private bool PersonsFilter(object o)
        {
            Person person = o as Person;
            return person.Name.Contains(txtSearch.Text);
        }

        private void FillAddressBook(Model.AddressBook addressBook)
        {
            addressBook.Persons.Add(new Person()
            {
                Name = "Goran Urukalo",
                BirthDate = new DateTime(1996, 1, 15),
                Height = 196,
                Gender = Gender.Male
            });
            addressBook.Persons[0].Addresses.Add(new Address
            {
                StreetName = "Isidora Stojanovica",
                StreetNumber = "84"
            });
            addressBook.Persons.Add(new Person()
            {
                Name = "Mika Mikić",
                BirthDate = new DateTime(1885, 5, 15),
                Height = 185
            });
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            personsView.Filter = PersonsFilter;
        }

        private void miPersonsAdd_Click(object sender, RoutedEventArgs e)
        {
            PersonDialog dialog = new AddressBook.PersonDialog();
            if (dialog.ShowDialog() ?? false)
            {
                this.addressBook.Persons.Add(dialog.Person);
                this.personsView.MoveCurrentTo(dialog.Person);
            }
        }

        private void miPersonsEdit_Click(object sender, RoutedEventArgs e)
        {
            Person person = personsView.CurrentItem as Person;
            PersonDialog dialog = new AddressBook.PersonDialog(person);
            dialog.ShowDialog();
        }

        private void miPersonsRemove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Address Book", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Person person = personsView.CurrentItem as Person;
                this.addressBook.Persons.Remove(person);
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Address Book Binary File (*.adb)|*.adb|Address Book XML File (*.adbx)|*.adbx";
            if (dialog.ShowDialog() ?? false)
            {
                FileInfo fi = new FileInfo(dialog.FileName);
                switch (fi.Extension)
                {
                    case ".adb":
                        using (FileStream fs = File.OpenRead(dialog.FileName))
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            this.addressBook = (Model.AddressBook)bf.Deserialize(fs);
                            
                        }
                        break;
                    case ".adbx":                  
                        using (XmlReader xr = XmlReader.Create(dialog.FileName))
                        {
                            DataContractSerializer serializer = new DataContractSerializer(typeof(Model.AddressBook));
                            this.addressBook = (Model.AddressBook)serializer.ReadObject(xr);
                        }
                        break;
                }
                this.DataContext = this.addressBook;
                SetupAddressBookDefaultViewCollection();

            }
        }

        private void btnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Address Book Binary File (*.adb)|*.adb|Address Book XML File (*.adbx)|*.adbx";
            if (dialog.ShowDialog() ?? false)
            {
                FileInfo fi = new FileInfo(dialog.FileName); 
                switch(fi.Extension)
                {
                    case ".adb":
                        using (FileStream fs = File.OpenWrite(dialog.FileName))
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            bf.Serialize(fs, this.addressBook);
                        }
                        break;
                    case ".adbx":
                        XmlWriterSettings xmlSettings = new XmlWriterSettings();
                        xmlSettings.Indent = true;
                        using (XmlWriter xw = XmlWriter.Create(dialog.FileName, xmlSettings)) 
                        {
                            DataContractSerializer serializer = new DataContractSerializer(typeof(Model.AddressBook));
                            serializer.WriteObject(xw, this.addressBook);
                        }
                        break;
                }
                
            }
        }
    }
}
