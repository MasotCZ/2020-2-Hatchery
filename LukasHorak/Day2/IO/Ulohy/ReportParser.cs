using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace IO.Ulohy
{
    interface IReader<_T>
    {
        IEnumerable<_T> Read(StreamReader sr, CultureInfo info);
    }

    class ReportParser : IReader<Report>
    {
        public IEnumerable<Report> Read(StreamReader sr, CultureInfo info)
        {
            var ret = new List<Report>();
            try
            {
                //read header
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    ret.Add(ReadReport(sr.ReadLine(), info));
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Load err {e.Message}");
            }

            return ret;
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
