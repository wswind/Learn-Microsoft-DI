namespace MicrosoftDI.Sample.Services
{
    public class CustomService : ICustomService
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
