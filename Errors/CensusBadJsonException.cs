using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honu_census.Errors {

    /// <summary>
    ///     Occurs when the response from Census could not be parsed to JSON
    /// </summary>
    public class CensusBadJsonException : Exception {

        public CensusBadJsonException() : base() { }

        public CensusBadJsonException(string message) : base(message) { }

        public CensusBadJsonException(string message, Exception inner) : base(message, inner) { }

    }
}
