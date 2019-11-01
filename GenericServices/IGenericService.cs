namespace MicrosoftDI.Sample.GenericServices
{
    public interface IGenericService<in T>
    {
        bool Equal(T a, T b);
    }


}
