﻿namespace MicrosoftDI.Sample.GenericServices
{
    public class GenericService<T> : IGenericService<T>
    {
        public bool Equal(T a, T b)
        {
            return a.Equals(b);
        }
    }
}
