using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace IO.Ulohy
{
    /// <summary>
    /// ID_EMPLOYEE;ID_DEPARTMENT;DATE;TIME_FROM;TIME_TO
    /// </summary>
    class Report
    {
        public string ID { get; }
        public string Dept_ID { get; }
        public DateTime Date { get; }
        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }
    }

    class ReportParser
    {
        public IEnumerable<Report> Load(FileStream fs)
        {
            if (!fs.CanRead)
            {
                throw new ArgumentException($"cant read file {nameof(fs)}");
            }

            var split = fs.Read
        }


        private string ReadUntill( FileStream fs, IEnumerable<char> endCharacter)

        private Report LoadReport(string line) { }
    }

    class Uloha1 : IUloha
    {
        public string inputPath;
        public string outputPath;

        public void Execute()
        {
            if (!File.Exists(inputPath))
            {
                //file doesnt exist
                throw new ArgumentException($"file does not exist {nameof(inputPath)}");
            }

            using (FileStream fs = File.OpenRead(inputPath))
            {

            }

            //create or overwrite
            File.Create(outputPath);

            using (FileStream fs = File.OpenWrite(outputPath))
            {

            }
        }
    }
}
