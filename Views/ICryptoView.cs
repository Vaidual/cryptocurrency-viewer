using cryptocurrency_viewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace cryptocurrency_viewer.Views
{
    interface ICryptoView
    {
        void BlinkTableRowAsync(DataGridRow row, Color color);
    }
}
