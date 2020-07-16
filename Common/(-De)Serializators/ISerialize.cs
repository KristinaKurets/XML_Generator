using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface ISerialize<TSource>
    {
        void Write(TSource item);
    }
}
