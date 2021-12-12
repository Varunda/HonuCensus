using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honu_census.Models {

    /// <summary>
    ///     Represents information about a where clause on a condition
    /// </summary>
    public class QueryWhere {

        /// <summary>
        ///     What field the where is on
        /// </summary>
        public string Field { get; }

        /// <summary>
        ///     What operator is being performed on the where
        /// </summary>
        public string Operator { get; }

        /// <summary>
        ///     What value the where is
        /// </summary>
        public string Value { get; }

        public QueryWhere(string field, string op, string value) {
            Field = field;
            Operator = op;
            Value = value;
        }

    }
}
