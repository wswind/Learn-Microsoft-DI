namespace MicrosoftDI.Sample.GenericServices
{
    public class ExplicitService : IGenericService<int>
    {
        public bool Equal(int a, int b)
        {
            return a == b;
        }
    }
}
