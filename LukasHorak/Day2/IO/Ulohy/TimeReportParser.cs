using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace IO.Ulohy
{
    interface IWriter<_T>
    {
        void Write(string outputPath, CultureInfo info, IEnumerable<_T> timeReports);
    }

    class TimeReportParser : IWriter<TimeReport>
    {
        public void Write(string outputPath, CultureInfo info, IEnumerable<TimeReport> timeReports)
        {
            try
            {
                //write data
                using (FileStream fs = File.OpenWrite(outputPath))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine("ID_DEPARTMENT;HOURS");

                        foreach (var item in timeReports)
                        {
                            WriteReport(sw, info, item);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"err {e.Message}");
            }
        }

        private void WriteReport(StreamWriter sw, CultureInfo info, TimeReport timeReport)
        {
            sw.WriteLine($"{timeReport.DeptID};{timeReport.Hours}");
        }

    }
}
