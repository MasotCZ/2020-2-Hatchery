namespace XML.Ulohy
{
    class DepartmentReport
    {
        public DepartmentReport(string deptId, decimal avgSalary)
        {
            DeptId = deptId;
            AvgSalary = avgSalary;
        }

        public string DeptId { get; }
        public decimal AvgSalary { get; }
    }
}
