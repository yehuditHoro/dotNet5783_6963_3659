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
        public T Read(int id);
        public IEnumerable <T> ReadAll();
        public void Update(T item);
        public void Delete(int id);

    }
}
