using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftDIStudy.GenericServices
{

    public class GenericService2 : IGenericService<int>
    {
        public bool Equal(int a, int b)
        {
            return a == b;
        }
    }


}
