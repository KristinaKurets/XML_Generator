using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public abstract class AbstractSerializator<TSource> : ISerialize<TSource>
    {
        protected string path;
        public AbstractSerializator(string path)
        {
            this.path = path;
        }
        public abstract void Write(TSource item);
        
    }

    
}
