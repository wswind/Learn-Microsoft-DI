using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftDIStudy.GenericServices
{
    public class GenericCaller<T>
    {
        public IGenericService<T> Serv { get; }
          public GenericCaller(IGenericService<T> serv)
        {
            Serv = serv;
        }

        public bool Equal(T a, T b)
        {
            return Serv.Equal(a, b);
        }
    }
}
