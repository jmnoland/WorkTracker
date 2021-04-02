using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace WorkTracker.Controllers.Exceptions
{
    public class ApiException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
