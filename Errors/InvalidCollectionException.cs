using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honu_census.Errors {

    /// <summary>
    ///     Occurs when a collection could not be accessed
    /// </summary>
    public class InvalidCollectionException : Exception {

        public string Collection { get; private set; }

        public InvalidCollectionException(string collection)
            : base($"the collection '{collection}' does not exist") {

            Collection = collection;
        }

        public InvalidCollectionException(string collection, Exception inner)
            : base($"the collection '{collection}' does not exist", inner) {

            Collection = collection;
        }

    }
}
