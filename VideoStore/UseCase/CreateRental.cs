using System;
using VideoStore.Boundary;
using VideoStore.Gateway;

namespace VideoStore.UseCase
{
    public class CreateRental
    {
        private readonly IMovieTypeRetriever _movieTypeRetriever;
        private readonly IRentalSaver _rentalSaver;

        public CreateRental(IMovieTypeRetriever movieTypeRetriever, IRentalSaver rentalSaver)
        {
            _movieTypeRetriever = movieTypeRetriever;
            _rentalSaver = rentalSaver;
        }

        public void Execute(CreateRentalRequest createRentalRequest)
        {
            var movie = _movieTypeRetriever.Retrieve(createRentalRequest.MovieName, createRentalRequest.Days);

            _rentalSaver.Save(createRentalRequest.MovieName, movie.Cost);
        }
    }
}
