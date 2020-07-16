using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IDeserialize<TSource>
    {
        TSource Load(string path);
    }
}
