namespace DependecnyInjection
{
    public class NumberImpl : INumber
    {
        private static int Created = 0;

        public NumberImpl()
        {
            Created++;
        }

        public int GetNumber()
        {
            return Created;
        }
    }
}
