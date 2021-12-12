using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honu_census.Models {

    /// <summary>
    ///     
    /// </summary>
    public class CensusQuery {

        /// <summary>
        ///     What collection this query acts on
        /// </summary>
        public string Collection { get; private set; }

        /// <summary>
        ///     URL produced by this
        /// </summary>
        public string Url {
            get {
                if (_url == null) {
                    _url = GetUrl();
                }
                return _url;
            }
        }

        private string? _url = null;

        /// <summary>
        ///     ServiceId used to perform this query
        /// </summary>
        public string ServiceId { get; private set; }

        private List<QueryWhere> Where { get; } = new List<QueryWhere>();

        private List<string> Resolves { get; } = new List<string>();

        public CensusQuery(string collection, string serviceId) {
            Collection = collection;
            ServiceId = serviceId;
        }

        public void WhereEquals(string field, object value) {
            Where.Add(new QueryWhere(field, "=", value?.ToString() ?? ""));
        }

        public void WhereEquals(string field, List<object> values) {
            string value = string.Join(",", values.Select(i => i.ToString()));
            Where.Add(new QueryWhere(field, "=", value));
        }

        public void WhereLike(string field, object value) {
            Where.Add(new QueryWhere(field, "=*", value?.ToString() ?? ""));
        }

        public void AddResolve(string resolve) {

        }

        public string GetUrl() {
            string url = $"https://census.daybreakgames.com/{ServiceId}/get/ps2:v2/{Collection}/";

            void AddQuery(string query) {
                if (url.EndsWith("/")) {
                    url += $"?{query}";
                } else {
                    url += $"&{query}";
                }
            }

            if (Where.Count > 0) {
                string where = string.Join("&", Where.Select(i => $"{i.Field}{i.Operator}{i.Value}"));
                AddQuery(where);
            }

            if (Resolves.Count > 0) {
                string resolve = $"c:resolve={string.Join(",", Resolves)}";
                AddQuery(resolve);
            }

            return url;
        }

    }
}
