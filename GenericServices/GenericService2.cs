namespace MicrosoftDI.Sample.GenericServices
{

    public class GenericService2 : IGenericService<int>
    {
        public bool Equal(int a, int b)
        {
            return a == b;
        }
    }

  

}
