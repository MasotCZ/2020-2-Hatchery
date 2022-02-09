using log4net;

namespace Logging.Cars
{
    public class CarShop
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CarShop));

        public CarShop()
        {
            // This message will not be appended anywhere, because Logging.Cars.CarShop doesn't have configured logger (and appender) so it inherits configuration from root logger
            // which has level = INFO
            _logger.Debug("CarShop has been created");
        }

        public void SellCar()
        {
            _logger.Info("Car was sold");
        }
    }
}
