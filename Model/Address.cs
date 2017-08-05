using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Model
{
    
    [Serializable]
    [DataContract]
    public class Address : NotifyingObject //, ISerializable
    {
        private string streetName;
        private string streetNumber;

        [DataMember(Order =0)] 
        public string StreetName
        {
            get
            {
                return streetName;
            }

            set
            {
                SetAndNotify(ref streetName, value);
            }
        }

        [DataMember(Order = 1)]
        public string StreetNumber
        {
            get
            {
                return streetNumber;
            }

            set
            {
                SetAndNotify(ref streetNumber, value);
            }
        }

        //protected Address(SerializationInfo info, StreamingContext context)
        //{
        //    this.StreetName = info.GetString("imeUlice");
        //    this.streetNumber = info.GetString("broj");
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("imeUlice", this.streetName);
        //    info.AddValue("broj", this.StreetNumber);
        //}
    }
}
