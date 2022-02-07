using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UlohyUtility;

namespace XML.Ulohy
{

    class Uloha1 : IUloha
    {
        public string xmlInput = "./xmlInput.xml";
        public string xmlOutput = "./xmlOutput.xml";

        public void Execute()
        {
            var company = new Company();
            company.ReadFromXML(xmlInput);
            company.WriteContinualToXML(xmlOutput, company.CreateReport());
        }
    }
}
