using System;
using System.Collections.Generic;
using System.Text;

namespace Olypmics.Common
{
    public class BaseResult
    {
        public bool Status { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string ResultValue { get; set; }

    }
}
