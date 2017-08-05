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
    public class Gender : IObjectReference
    {  
        public static readonly Gender Male = new Gender("Male");
        public static readonly Gender Female = new Gender("Female");
        public static readonly Gender[] Genders = new Gender[] { Gender.Male, Gender.Female };

        [DataMember]
        public string Name { get; private set; }


        protected Gender(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public object GetRealObject(StreamingContext context)
        {
            if (this.Name == "Male")
                return Gender.Male;
            if (this.Name == "Female")
                return Gender.Female;
            return null;
        }
    }
}
