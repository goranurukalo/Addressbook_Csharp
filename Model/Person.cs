using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Model
{
    [Serializable]
    [DataContract]
    public class Person : NotifyingObject
    {
        private string name;
        [DataMember(Name ="BirthDate")]
        private DateTime birthDate;
        private int height;
        private Gender gender;
        private ObservableCollection<Address> addresses = new ObservableCollection<Address>();
        
        public int Age
        {
            get
            {
                DateTime sada = DateTime.Now;
                DateTime rodjendan = new DateTime(sada.Year, 
                    this.birthDate.Month, this.birthDate.Day);
                TimeSpan x = sada - rodjendan;
                return sada.Year - this.birthDate.Year 
                    - (sada < rodjendan ? 1 : 0);
            }
        }
        [DataMember]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.SetAndNotify(ref this.name, value);                
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return this.birthDate;
            }

            set
            {
                if (this.birthDate != value)
                {
                    this.birthDate = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("Age");
                }
            }
        }
        [DataMember]
        public int Height
        {
            get
            {
                return this.height;
            }

            set
            {
                SetAndNotify(ref this.height, value);
            }
        }
        [DataMember]
        public Gender Gender
        {
            get
            {
                return gender;
            }

            set
            {
                SetAndNotify(ref gender, value);
            }
        }

        [DataMember]
        public ObservableCollection<Address> Addresses
        {
            get
            {
                return addresses;
            }
            private set
            {
                this.addresses = value;
            }
        }

        public override string ToString()
        {
            return this.name + ","
                + this.birthDate.ToString("dd.MM.yy") + ","
                + this.height;
        }


        
    }
}