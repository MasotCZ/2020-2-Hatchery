namespace IO.Ulohy
{
    /// <summary>
    /// ID_DEPARTMENT; HOURS
    /// </summary>
    class TimeReport
    {
        public TimeReport(string deptID, double hours)
        {
            DeptID = deptID;
            Hours = hours;
        }

        public string DeptID { get; }
        public double Hours { get; }
    }
}
