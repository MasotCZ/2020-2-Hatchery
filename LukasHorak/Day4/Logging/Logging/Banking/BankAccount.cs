using log4net;

namespace Logging.Banking
{
    public class BankAccount
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(BankAccount));

        public BankAccount()
        {
            // This message will be printed to the Console because there is a configured Logger for Logging.Banking, so Logging.Banking.BankAccount inherits from it
            // See that this message will be printed twice (because of appenders additivity) because this class is also a descendant of the root logger
            _logger.Debug("Bank account has been created");
        }
    }
}
