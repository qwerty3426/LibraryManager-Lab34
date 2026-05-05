using System.Collections.Generic;

namespace BankSystem.Domain
{
    public interface IRepository<T> {
        IEnumerable<T> GetAll();
        void Add(T entity);
        T GetById(int id);
    }
}