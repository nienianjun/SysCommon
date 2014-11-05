using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Common.Exception
{
    public class SysException : System.Exception
    {
        public SysException() { }

        public SysException(string message) : base(message) { }

        public SysException(string message, int statusCode) : base(message) { this.StatusCode = statusCode; }

        /// <summary>
        /// 异常状态值
        /// </summary>
        public int StatusCode
        {
            get;
            set;
        }
    }
}
