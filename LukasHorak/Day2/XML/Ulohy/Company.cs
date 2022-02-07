using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

namespace XML.Ulohy
{

    [Serializable]
    class Company : List<Employee>
    {
        public void ReadFromXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(path);

                var employees = doc.GetElementsByTagName("employee");
                foreach (XmlNode employee in employees)
                {
                    Add(Employee.LoadFromXML(employee));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not load xml file {path}");
                return;
            }
        }

        public Report CreateReport()
        {
            var ret = new Report();

            foreach (var dept in this.GroupBy(d => d.DeptId).Select(d => (d.Key, d.Average(d => d.Salary))))
            {
                ret.Add(new DepartmentReport(dept.Key, dept.Item2));
            }

            return ret;
        }

        public void WriteContinualToXML(string path, Report report)
        {
            try
            {
                using (var fs = File.OpenWrite(path))
                {
                    var settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.Encoding = System.Text.Encoding.UTF8;
                    settings.NewLineOnAttributes = true;

                    using (XmlWriter writer = XmlWriter.Create(fs, settings))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("report");

                        foreach (var item in report)
                        {
                            writer.WriteStartElement("department");
                            writer.WriteAttributeString("id", item.DeptId);
                            writer.WriteStartElement("avarageSalary");
                            writer.WriteString(item.AvgSalary.ToString());
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not load xml file {path}");
                return;
            }
        }

        public override string ToString()
        {
            var ret = "";

            foreach (var item in this)
            {
                ret += $"-------------------------------\n{item}\n";
            }

            return ret;
        }
    }
}
