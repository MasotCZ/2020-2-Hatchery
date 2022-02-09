using log4net;

namespace Logging.Banking
{
    // See that this class has configured Logging.Banking.Bank logger with RollingFile appender
    public class Bank
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Bank));

        public Bank()
        {
            // This message will be printed to the Console because there is a configured Logger for Logging.Banking, so Logging.Banking.Bank inherits from it
            // See that this message will be printed twice (because of appenders additivity) because this class is also a descendant of the root logger
            _logger.Debug("Bank has been created");
        }

        public void SomeLogic()
        {
            for(int i = 0; i < 1000; i++)
            {
                _logger.Info($"{i}");
            }
        }
    }
}
