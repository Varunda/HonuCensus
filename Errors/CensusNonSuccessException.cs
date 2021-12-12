using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace honu_census.Errors {

    /// <summary>
    ///     Occurs when a non-200 status code is returned from Census
    /// </summary>
    public class CensusNonSuccessException : Exception {

        public int Status { get; }

        public CensusNonSuccessException(int status)
            : base($"got status code {status}, expected 200") {

            Status = status;
        }

        public CensusNonSuccessException(HttpStatusCode status)
            : this((int)status) { }

        public CensusNonSuccessException(int status, Exception inner)
            : base($"got status code {status}, expected 200", inner) {

            Status = status;
        }

        public CensusNonSuccessException(HttpStatusCode status, Exception inner)
            : this((int)status, inner) { }

    }
}
