namespace DependecnyInjection
{

    public class PdfPrinter : IPrintable
    {
        private IPrinterDependency _printerDependency;
        private static int created = 0;

        public PdfPrinter(IPrinterDependency printerDependency)
        {
            _printerDependency = printerDependency;
            created++;
        }

        public string Print()
        {
            return $"PDF print created: {created} and: {_printerDependency.GetMessage()}";
        }
    }
}
