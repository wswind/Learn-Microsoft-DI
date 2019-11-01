namespace MicrosoftDI.Sample.Services
{
    public class MoreInTypeService<T1, T2> : IMoreInTypeService<T1>
    {
        public bool Equal(T1 a, T1 b)
        {
            return a.Equals(b);
        }
    }

    public interface IMoreInTypeService<in T>
    {
        bool Equal(T a, T b);
    }
}
