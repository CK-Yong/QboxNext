using System;
using System.Collections.Generic;
using System.Linq;
using QboxNext.Core.Utils;
using QboxNext.Qserver.Core.Statistics;
using QboxNext.Qserver.Core.Utils;

namespace QboxNext.Qservice.Classes
{
    /// <summary>
    /// Class to retrieve live data from QBX files.
    /// </summary>
    public class LiveDataRetriever
    {
        private readonly ISeriesRetriever _seriesRetriever;


        public LiveDataRetriever()
	    {
            _seriesRetriever = new SeriesRetriever();
	    }


        /// <summary>
        /// Retrieve live energy data for an account.
        /// </summary>
        /// <remarks>
        /// Does not check read permissions.
        /// </remarks>
        /// <returns>
        /// Live data for each active device of the account.
        /// </returns>
        public IList<LiveDataForDevice> Retrieve(string qboxSerial, DateTime nowUtc)
        {
            var toUtc = nowUtc.Truncate(TimeSpan.FromMinutes(1));
            var fromUtc = toUtc.AddMinutes(-5);

            var liveData = new List<LiveDataForDevice>();
            IEnumerable<ValueSerie> seriesList = _seriesRetriever.RetrieveSerieValuesForAccount(qboxSerial, fromUtc, toUtc, SeriesResolution.OneMinute);
            foreach (var series in seriesList)
            {
                decimal? power = Series2Power(series.Data);
                liveData.Add(new LiveDataForDevice
                {
                    EnergyType = series.EnergyType,
                    Name = series.Name,
                    Power = power
                });
            }

            return liveData;
        }


        private static decimal? Series2Power(IEnumerable<SeriesValue> values)
        {
            var items = values.Where(s => s.Value != null).ToList();
            return !items.Any() ? null : items.Last().Value;
        }
    }
}
