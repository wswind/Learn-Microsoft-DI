using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftDIStudy.Services
{
    public class Service : IService
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
