using System;

namespace IO.Ulohy
{
    /// <summary>
    /// ID_EMPLOYEE;ID_DEPARTMENT;DATE;TIME_FROM;TIME_TO
    /// </summary>
    class Report
    {
        public Report(string iD, string dept_ID, DateTime date, DateTime dateFrom, DateTime dateTo)
        {
            ID = iD;
            DeptID = dept_ID;
            Date = date;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public string ID { get; }
        public string DeptID { get; }
        public DateTime Date { get; }
        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }

        public override string ToString()
        {
            return $"[{ID}] {DeptID}, {Date}, {DateFrom.Hour}, {DateTo.Hour}";
        }
    }
}
