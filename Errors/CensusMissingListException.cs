using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honu_census.Errors {

    /// <summary>
    ///     Occurs when the _list field in the JSON was not found, not sure how it could happen really
    /// </summary>
    public class CensusMissingListException : Exception {

        public CensusMissingListException() : base() { }

        public CensusMissingListException(string msg) : base(msg) { }

    }
}
