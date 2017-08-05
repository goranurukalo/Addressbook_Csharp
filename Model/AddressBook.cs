using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Model
{
    
    [Serializable]
    [DataContract]
    public class AddressBook : NotifyingObject
    {
        [DataMember]
        public ObservableCollection<Person> Persons { get; private set; }

        public AddressBook()
        {
            this.Persons = new ObservableCollection<Model.Person>();
        }
    }
}
