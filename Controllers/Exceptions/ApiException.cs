using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace WorkTracker.Controllers.Exceptions
{
    class ApiException : HttpStatusCodeResult
    {
        public ApiException(string message)
            : base(500, message)
        {

        }
    }
}
