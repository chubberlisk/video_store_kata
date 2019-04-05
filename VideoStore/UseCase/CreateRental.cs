using System;
using VideoStore.AcceptanceTest;
using VideoStore.Boundary;
using VideoStore.Gateway;

namespace VideoStore.UseCase
{
    public class CreateRental
    {
        public CreateRental(MovieTypeGateway movieTypeGateway, InMemoryRentalGateway rentalGateway)
        {
        }

        public void Execute(CreateRentalRequest p0)
        {
        }
    }
}