using System;
using System.Xml;

namespace XML.Ulohy
{
    /// <summary>
    /// <employee department="dep_001">
    /// <salary>35000</salary>
    /// <firstName>Pavel</firstName>
    /// <lastName>Novak</lastName>
    /// </employee>
    /// </summary>
    [Serializable]
    public class Employee
    {
        public Employee(string firstName, string lastName, decimal salary, string deptId)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            DeptId = deptId;
        }

        public string DeptId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public decimal Salary { get; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Salary}";
        }

        public static Employee LoadFromXML(XmlNode employeeXML)
        {
            if (employeeXML is null || employeeXML.Name != "employee")
            {
                throw new ArgumentException($"{nameof(employeeXML)}");
            }

            var dept = employeeXML.Attributes.GetNamedItem("department")?.InnerText;
            var salary = 0m;
            var firstName = "";
            var lastName = "";
            foreach (XmlNode node in employeeXML.ChildNodes)
            {
                switch (node.Name)
                {
                    case "salary":
                        salary = decimal.Parse(node.InnerText);
                        break;
                    case "firstName":
                        firstName = node.InnerText;
                        break;
                    case "lastName":
                        lastName = node.InnerText;
                        break;
                    default:
                        throw new ArgumentException($"incorrent elemet: {node.Name}, val: {node.Value}");
                }
            }

            return new Employee(firstName, lastName, salary, dept);
        }
    }
}
