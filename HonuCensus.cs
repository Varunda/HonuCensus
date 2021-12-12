using honu_census.Errors;
using honu_census.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace honu_census {

    public class HonuCensus {

        private readonly ILogger<HonuCensus> _Logger;

        private readonly List<string> serviceIds = new List<string>();

        private readonly static HttpClient _Http = new HttpClient(new HttpClientHandler() {
            AllowAutoRedirect = false
        });

        public HonuCensus(ILogger<HonuCensus> logger) { 
            _Logger = logger;
        }

        /// <summary>
        ///     Create a new query
        /// </summary>
        /// <param name="collection">Collection to use</param>
        public CensusQuery New(string collection) {
            if (serviceIds.Count == 0) {
                throw new InvalidOperationException($"You must call {nameof(AddServiceId)} at least once before creating a query");
            }
            return new CensusQuery(collection, serviceIds[0]);
        }

        /// <summary>
        ///     Add a new service ID to add
        /// </summary>
        /// <param name="serviceId">Service ID to add</param>
        public void AddServiceId(string serviceId) {
            this.serviceIds.Add(serviceId);
        }

        public async Task<JToken?> GetSingle(CensusQuery query, CancellationToken cancel) {
            List<JToken> options = await GetList(query, cancel);
            if (options.Count == 0) {
                return null;
            }
            return options[0];
        }

        public async Task<List<JToken>> GetList(CensusQuery query, CancellationToken cancel) {
            string url = query.Url;

            _Logger.LogDebug($"Making query to '{url}'");

            HttpResponseMessage response;

            try {
                response = await _Http.GetAsync(url, cancel);
            } catch (Exception) {
                throw;
            }
            if (response.StatusCode == HttpStatusCode.Redirect) {
                _Logger.LogTrace($"Got redirected to {response.Content}");
            }

            _Logger.LogTrace($"response = {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.OK) {
                throw new CensusNonSuccessException(response.StatusCode);
            }

            string responseContent = await response.Content.ReadAsStringAsync(cancel);
            JToken result;

            try {
                result = JToken.Parse(responseContent);
            } catch (Exception ex) {
                throw new CensusBadJsonException("", ex);
            }

            _Logger.LogDebug($"{result}");

            string? error = result.Value<string?>("error");
            if (error != null) {
                if (error == "No data found.") {
                    throw new InvalidCollectionException(query.Collection);
                } else if (error == "service_unavailable") {
                    throw new CensusUnavailableException();
                }
            }

            JToken listToken = result.SelectToken($"{query.Collection}_list")
                ?? throw new CensusMissingListException($"expected {query.Collection}_list to exist");

            return listToken.ToList();
        }

    }
}
