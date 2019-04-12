using VideoStore.Boundary;

namespace VideoStore.Gateway
{
    public class InMemoryRentalGateway : IRentalSaver
    {
        public RentalSaverResponse Save(string movieName, double cost)
        {
            return new RentalSaverResponse();
        }
    }
}
