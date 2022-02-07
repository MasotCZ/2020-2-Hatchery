using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace IO.Ulohy
{
    interface IReader<_T>
    {
        IEnumerable<_T> Read(string path, CultureInfo info);
    }

    class ReportParser : IReader<Report>
    {
        public IEnumerable<Report> Read(string inputPath, CultureInfo info)
        {
            IEnumerable<Report> reports = new List<Report>();
            try
            {
                //read data
                using (FileStream fs = File.OpenRead(inputPath))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        var ret = new List<Report>();
                        //read header
                        sr.ReadLine();

                        while (!sr.EndOfStream)
                        {
                            ret.Add(ReadReport(sr.ReadLine(), info));
                        }
                        return ret;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"err {e.Message}");
            }

            return reports;
        }

        private Report ReadReport(string line, CultureInfo info)
        {
            var split = line.Split(';');

            return new Report(
                split[0],
                split[1],
                 DateTime.Parse(split[2], info),
                 DateTime.Parse(split[3], info),
                 DateTime.Parse(split[4], info)
                );
        }
    }
}
