using VideoStore.Boundary;

namespace VideoStore.UseCase
{
    public class PrintStatement
    {
        public PrintStatement(ViewStatement viewStatement)
        {
        }

        public PrintStatementResponse Execute()
        {
            return new PrintStatementResponse();
        }
    }
}