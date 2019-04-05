using System;
using System.IO;
using FluentAssertions;
using FluentSim;
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
            var videoStoreApi = new FluentSimulator("http://localhost:8069/");
            
            var videoStoreApiBatmanBeginsMovieTypeResponse = File.ReadAllText(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "../../../VideoStoreApiResponse/BatmanBeginsMovieTypeResponse.json"
                )
            );

            videoStoreApi.Get("/api/movie/type/batmanbegins").Responds(videoStoreApiBatmanBeginsMovieTypeResponse);
            
            var videoStoreApiTheDarkKnightMovieTypeResponse = File.ReadAllText(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "../../../VideoStoreApiResponse/TheDarkKnightMovieTypeResponse.json"
                )
            );

            videoStoreApi.Get("/api/movie/type/batmanbegins").Responds(videoStoreApiTheDarkKnightMovieTypeResponse);
            
            var videoStoreApiTheDarkKnightRisesMovieTypeResponse = File.ReadAllText(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "../../../VideoStoreApiResponse/TheDarkKnightRisesMovieTypeResponse.json"
                )
            );

            videoStoreApi.Get("/api/movie/type/batmanbegins").Responds(videoStoreApiTheDarkKnightRisesMovieTypeResponse);
            
            


            var movieTypeFactory = new MovieTypeFactory();
            var movieTypeGateway = new MovieTypeGateway(
                "http://localhost:8069/",
                "xxxx-xxxxxxxxx-xxxx",
                movieTypeFactory
            );
            var rentalGateway = new InMemoryRentalGateway();

            var createRental = new CreateRental(movieTypeGateway, rentalGateway);

            createRental.Execute(new CreateRentalRequest
            {
                Name = "Batman Begins",
                Days = 1
            });

            createRental.Execute(new CreateRentalRequest
            {
                Name = "The Dark Knight",
                Days = 2
            });

            createRental.Execute(new CreateRentalRequest
            {
                Name = "The Dark Knight Rises",
                Days = 3
            });

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