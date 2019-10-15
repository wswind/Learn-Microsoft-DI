using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftDIStudy.GenericServices
{
    public class GenericService<T> : IGenericServices<T>
    {
        public bool Equals(T a, T b)
        {
            return a.Equals(b);
        }
    }
}
