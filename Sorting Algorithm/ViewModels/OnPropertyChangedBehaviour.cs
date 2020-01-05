using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    /// <summary>
    /// A base view model that fire the Property Changed event
    /// </summary>
    public abstract class OnPropertyChangedBehaviour : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fire when any property when a property changes value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the Property Changed event to force the UI to update
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
