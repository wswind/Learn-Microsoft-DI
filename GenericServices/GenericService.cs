﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftDIStudy.GenericServices
{
    public class GenericService<T> : IGenericService<T>
    {
        public bool Equal(T a, T b)
        {
            return a.Equals(b);
        }
    }
}
