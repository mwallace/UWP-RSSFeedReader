using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RSSFeedReader
{ 
    public class RSSLink : INotifyPropertyChanged
    {
        private string _name;
        private string _link;

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }
        public string Name
        {
            get => _name;
            // Raises the OnPropertyChanged event to enable two-way data binding
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
