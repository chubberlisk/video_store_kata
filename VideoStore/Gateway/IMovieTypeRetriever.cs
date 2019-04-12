using VideoStore.Domain_Object;

namespace VideoStore.Gateway
{
    public interface IMovieTypeRetriever
    {
        Movie Retrieve(string name, int days);
    }
}
