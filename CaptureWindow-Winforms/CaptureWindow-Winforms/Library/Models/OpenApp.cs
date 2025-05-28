using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureWindow_Winforms.Library.Models
{
    public class OpenApp
    {
        public IntPtr Handle { get; set; }
        public string Title { get; set; }

        public override string ToString() => Title;
    }
}
