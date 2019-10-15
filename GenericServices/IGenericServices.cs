using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftDIStudy.GenericServices
{
    public interface IGenericServices<in T>
    {
        bool Equals(T a, T b);
    }
}
