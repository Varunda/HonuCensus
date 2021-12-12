using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honu_census.Errors {

    /// <summary>
    ///     Is thrown when a Census message gets a 'error: "service_unavailable"'
    /// </summary>
    public class CensusUnavailableException : Exception {

        public CensusUnavailableException() : base() { }

        public CensusUnavailableException(string message) : base (message) { }

        public CensusUnavailableException(string message, Exception inner) : base(message, inner) { }

    }
}
