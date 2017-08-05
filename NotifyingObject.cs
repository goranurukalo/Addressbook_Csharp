using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    [Serializable]
    [DataContract]
    public class NotifyingObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetAndNotify<T>(ref T field, T fieldValue, [CallerMemberName]string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, fieldValue))
            {
                field = fieldValue;
                NotifyPropertyChanged(propertyName);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
