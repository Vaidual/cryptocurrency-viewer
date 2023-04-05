using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Models
{
    public class Response<T> where T : class
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
