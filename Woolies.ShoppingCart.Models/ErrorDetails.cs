using System;
using System.Collections.Generic;
using System.Text;

namespace Woolies.Shopping.Models
{
    /// <summary>
    ///model for exception handler
    /// </summary>
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
