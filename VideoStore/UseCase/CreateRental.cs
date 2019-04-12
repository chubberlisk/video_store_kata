using System;
using VideoStore.Boundary;
using VideoStore.Gateway;

namespace VideoStore.UseCase
{
    public class CreateRental
    {
        public CreateRental(InMemoryMovieTypeGateway movieTypeGateway, InMemoryRentalGateway rentalGateway)
        {
        }

        public void Execute(CreateRentalRequest p0)
        {
        }
    }
}
