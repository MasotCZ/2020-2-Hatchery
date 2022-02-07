using System.Collections.Generic;

namespace XML.Ulohy
{
    /// <summary>
    /// <report>
    //  <department id = "dep001" >
    //    < averageSalary > 31500 </ averageSalary >
    //  </ department >
    //  < department id="dep002">
    //    <averageSalary>34500</averageSalary>
    //  </department>
    //</report>
    /// </summary>
    class Report : List<DepartmentReport>
    {
    }
}
