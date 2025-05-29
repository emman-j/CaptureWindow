using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureWindow_Winforms.Forms
{
    public partial class Child_Form : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        //private string _name;
        //public string Name
        //{
        //    get => _name;
        //    set
        //    {
        //        if (_name != value)
        //        {
        //            _name = value;
        //            NotifyPropertyChanged();
        //        }            
        //    }
        //}


        public Child_Form()
        {
            InitializeComponent();
            //this.DataBindings.Add("Text",this, nameof(Name), false, DataSourceUpdateMode.OnPropertyChanged);
            //Name = string.Empty;
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertname = "")
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertname));
        }
    }
}
