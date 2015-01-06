using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Appraisal.Models
{
    public interface API<T1, T2>
    {
        T2 To<T1>(T1 Value);
    }    
}