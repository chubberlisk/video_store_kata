using VideoStore.Boundary;

namespace VideoStore.Gateway
{
    public interface IRentalSaver
    {
        RentalSaverResponse Save(string movieName, double cost);
    }
}
