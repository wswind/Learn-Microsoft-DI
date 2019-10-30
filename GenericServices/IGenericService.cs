using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftDIStudy.GenericServices
{
    public interface IGenericService<in T>
    {
        bool Equal(T a, T b);
    }


}
