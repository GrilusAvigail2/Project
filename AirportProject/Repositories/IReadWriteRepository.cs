
using System;
using System.Collections.Generic;
using System.Text;


namespace Repositories
{
    public interface IReadWriteRepository<T>
    {
        public int GetNumOfItems();
        public List<T> GetAll();
        public T GetById(int id);
    }
}
