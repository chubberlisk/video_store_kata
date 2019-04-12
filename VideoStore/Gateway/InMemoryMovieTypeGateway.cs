using System.Collections.Generic;
using VideoStore.Domain_Object;
using VideoStore.Factory;

namespace VideoStore.Gateway
{
    public class InMemoryMovieTypeGateway : IMovieTypeRetriever
    {
        public InMemoryMovieTypeGateway(MovieTypeFactory movieTypeFactory)
        {
        }

        public Movie Retrieve(string name, int days)
        {
            return new Movie();
        }
    }
}
