using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Utilities
{
    public static class ViewLocator
    {
        public static Type GetViewType(Type viewModelType)
        {
            var viewTypeName = viewModelType.Name.Replace("VM", "Page");
            var viewType = Type.GetType("cryptocurrency_viewer.Views."+ viewTypeName);
            return viewType;
        }
    }
}
