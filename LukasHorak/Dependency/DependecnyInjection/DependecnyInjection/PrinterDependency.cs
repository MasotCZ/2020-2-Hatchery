namespace DependecnyInjection
{
    public class PrinterDependency : IPrinterDependency
    {
        private static int created = 0;

        public PrinterDependency()
        {
            created++;
        }

        public string GetMessage()
        {
            return $"dependency created {created} times";
        }
    }
}
