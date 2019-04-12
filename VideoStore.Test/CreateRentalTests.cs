using FluentAssertions;
using NUnit.Framework;
using VideoStore.Boundary;
using VideoStore.Domain_Object;
using VideoStore.Gateway;
using VideoStore.UseCase;

namespace VideoStore.Test
{
    public class CreateRentalTests
    {
        // Dummmies
        private class MovieTypeRetrieverDummy : IMovieTypeRetriever
        {
            public Movie Retrieve(string name, int days)
            {
                return new Movie();
            }
        }
        
        private class InMemoryRentalGatewayDummy : IRentalSaver
        {
            public RentalSaverResponse Save(string movieName, double cost)
            {
                return new RentalSaverResponse();
            }
        }
        
        // Spies
        private class MovieTypeRetrieverSpy : IMovieTypeRetriever
        {
            public bool IsRetrieveCalled { get; private set; }
            public object[] RetrieveArguments { get; private set; }

            public Movie Retrieve(string name, int days)
            {
                IsRetrieveCalled = true;
                RetrieveArguments = new object[]
                {
                    name,
                    days
                };
                
                return new Movie();
            }
        }
        
        // Stubs
        private class MovieTypeRetrieverStub : IMovieTypeRetriever
        {
            public Movie Retrieve(string name, int days)
            {
                return new Movie
                {
                    Name = name,
                    Cost = 2.0
                };
            }
        }

        private class InMemoryRentalGatewaySpy : IRentalSaver
        {
            public bool IsSaveCalled { get; private set; }
            public object[] SaveArguments { get; private set; }

            public RentalSaverResponse Save(string movieName, double cost)
            {
                IsSaveCalled = true;
                SaveArguments = new object[]
                {
                    movieName,
                    cost
                };
                
                return new RentalSaverResponse();
            }
        }

        [Test]
        public void CanGetTheMovieTypeForAFilm()
        {
            var movieTypeGateway = new MovieTypeRetrieverSpy();
            var rentalGateway = new InMemoryRentalGatewayDummy();
            var createRental = new CreateRental(movieTypeGateway, rentalGateway);

            createRental.Execute(new CreateRentalRequest());

            movieTypeGateway.IsRetrieveCalled.Should().BeTrue();
        }
        
        [Test]
        public void CanGetTheMovieTypeForRequestedFilm()
        {
            var movieTypeGateway = new MovieTypeRetrieverSpy();
            var rentalGateway = new InMemoryRentalGatewayDummy();
            var createRental = new CreateRental(movieTypeGateway, rentalGateway);

            const string movieName = "Man of Steel";
            
            createRental.Execute(
                new CreateRentalRequest
                {
                    MovieName = movieName
                }
            );

            movieTypeGateway.RetrieveArguments[0].Should().Be(movieName);
        }
        
        [Test]
        public void CanGetTheMovieTypeForRequestedNumberOfDays()
        {
            var movieTypeGateway = new MovieTypeRetrieverSpy();
            var rentalGateway = new InMemoryRentalGatewayDummy();
            var createRental = new CreateRental(movieTypeGateway, rentalGateway);

            const int days = 1;
            
            createRental.Execute(
                new CreateRentalRequest
                {
                    Days = days
                }
            );

            movieTypeGateway.RetrieveArguments[1].Should().Be(days);
        }

        [Test]
        public void CanSaveARental()
        {
            var movieTypeGateway = new MovieTypeRetrieverDummy();
            var rentalGateway = new InMemoryRentalGatewaySpy();
            var createRental = new CreateRental(movieTypeGateway, rentalGateway);
            
            createRental.Execute(new CreateRentalRequest());

            rentalGateway.IsSaveCalled.Should().BeTrue();
        }
        
        [Test]
        public void CanSaveARentalForRequestedMovie()
        {
            var movieTypeGateway = new MovieTypeRetrieverDummy();
            var rentalGateway = new InMemoryRentalGatewaySpy();
            var createRental = new CreateRental(movieTypeGateway, rentalGateway);
            
            const string movieName = "Batman V Superman: Dawn of Justice";

            createRental.Execute(
                new CreateRentalRequest
                {
                    MovieName = movieName
                }
            );

            rentalGateway.SaveArguments[0].Should().Be(movieName);
        }
        
        [Test]
        public void CanSaveARentalForRequestedMovieWithCost()
        {
            var movieTypeGateway = new MovieTypeRetrieverStub();
            var rentalGateway = new InMemoryRentalGatewaySpy();
            var createRental = new CreateRental(movieTypeGateway, rentalGateway);

            createRental.Execute(
                new CreateRentalRequest
                {
                    MovieName = "Wonder Woman",
                    Days = 1
                }
            );

            rentalGateway.SaveArguments[1].Should().Be(2.0);
        }
    }
}
