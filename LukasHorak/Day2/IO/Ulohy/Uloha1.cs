using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace IO.Ulohy
{

    class Uloha1 : IUloha
    {
        public string inputPath = "./input.txt";
        public string outputPath = "./output.txt";

        public void Execute()
        {
            if (!File.Exists(inputPath))
            {
                //file doesnt exist
                throw new ArgumentException($"file does not exist, input file needs to be next to the .exe {nameof(inputPath)}");
            }

            var info = new CultureInfo("de-DE");

            var rp = new ReportParser();
            var reports = rp.Read(inputPath, info);
            var timeReports = ConvertReports(reports);


            var trp = new TimeReportParser();
            trp.Write(outputPath, info, timeReports);
        }

        private IEnumerable<TimeReport> ConvertReports(IEnumerable<Report> reports)
        {
            var ret = new List<TimeReport>();

            var grouped = reports.GroupBy(d => d.DeptID);

            foreach (var groupedReports in grouped)
            {
                var hours = 0d;
                foreach (var report in groupedReports)
                {
                    hours += (report.DateTo - report.DateFrom).TotalHours;
                }
                ret.Add(new TimeReport(groupedReports.Key, hours));
            }

            return ret;
        }
    }
}
