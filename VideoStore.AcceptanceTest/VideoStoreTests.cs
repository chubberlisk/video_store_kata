using FluentAssertions;
using NUnit.Framework;
using VideoStore.Boundary;
using VideoStore.Factory;
using VideoStore.Gateway;
using VideoStore.UseCase;

namespace VideoStore.AcceptanceTest
{
    public class VideoStoreTest
    {
        [Test]
        public void CanBuildAStatementForMultipleRegularRentals()
        {
            var movieTypeFactory = new MovieTypeFactory();
            var movieTypeGateway = new InMemoryMovieTypeGateway(movieTypeFactory);
            var rentalGateway = new InMemoryRentalGateway();

            var createRental = new CreateRental(movieTypeGateway, rentalGateway);

            createRental.Execute(
                new CreateRentalRequest
                {
                    MovieName = "Batman Begins",
                    Days = 1
                }
             );

            createRental.Execute(
                new CreateRentalRequest
                {
                    MovieName = "The Dark Knight",
                    Days = 2
                }
            );

            createRental.Execute(
                new CreateRentalRequest
                {
                    MovieName = "The Dark Knight Rises",
                    Days = 3
                }
            );

            var viewStatement = new ViewStatement(rentalGateway);

            var printStatement = new PrintStatement(viewStatement);

            var printStatementResponse = printStatement.Execute();

            printStatementResponse.Statement.Should().Be(
                "Rental Record for Customer\n" +
                "  Batman Begins 2.0\n" +
                "  The Dark Knight 2.0\n" +
                "  The Dark Knight Rises 3.5\n" +
                "You owe 7.5\n" +
                "You earned 3 frequent renter points"
            );
        }
    }
}
