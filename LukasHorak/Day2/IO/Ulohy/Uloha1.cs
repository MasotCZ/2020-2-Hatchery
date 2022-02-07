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
        public string inputPath = "./input";
        public string outputPath = "./output";

        public void Execute()
        {
            if (!File.Exists(inputPath))
            {
                //file doesnt exist
                throw new ArgumentException($"file does not exist, input file needs to be next to the .exe {nameof(inputPath)}");
            }

            var info = new CultureInfo("de-DE");

            IEnumerable<Report> reports;
            try
            {
                //read data
                using (FileStream fs = File.OpenRead(inputPath))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        var rp = new ReportParser();
                        reports = rp.Read(sr, info);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"err {e.Message}");
                return;
            }

            var timeReports = ConvertReports(reports);

            try
            {
                //write data
                using (FileStream fs = File.OpenWrite(outputPath))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        var trp = new TimeReportParser();
                        trp.Write(sw, info, timeReports);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"err {e.Message}");
                return;
            }
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
