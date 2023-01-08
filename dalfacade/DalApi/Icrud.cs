using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface Icrud<T>
    {
        public int Add(T item);
        public T ReadSingle(Func<T, bool> func);
        public IEnumerable<T> ReadAll(Func<T, bool>? func = null);
        public void Update(T item);
        public void Delete(int id);

    }
}
