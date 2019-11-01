namespace MicrosoftDI.Sample.Services
{
    public class SampleService : ISampleService
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }

    public interface ISampleService
    {
        int Sum(int a, int b);
    }
}
