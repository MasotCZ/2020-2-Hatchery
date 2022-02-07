using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace IO.Ulohy
{
    interface IWriter<_T>
    {
        void Write(StreamWriter sw, CultureInfo info, IEnumerable<_T> timeReports);
    }

    class TimeReportParser : IWriter<TimeReport>
    {
        public void Write(StreamWriter sw, CultureInfo info, IEnumerable<TimeReport> timeReports)
        {
            sw.WriteLine("ID_DEPARTMENT;HOURS");

            foreach (var item in timeReports)
            {
                WriteReport(sw, info, item);
            }
        }

        private void WriteReport(StreamWriter sw, CultureInfo info, TimeReport timeReport)
        {
            sw.WriteLine($"{timeReport.DeptID};{timeReport.Hours}");
        }

    }
}
