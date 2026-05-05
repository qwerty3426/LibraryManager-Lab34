using System.Collections.Generic;
using System.Linq;
using BankSystem.Domain;

namespace BankSystem.Infrastructure
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _items = new();

        public IEnumerable<T> GetAll() => _items;

        public void Add(T entity) => _items.Add(entity);

        // Додаємо реалізацію GetById тут теж
        public T GetById(int id)
        {
            return _items.FirstOrDefault(x => (int?)x.GetType().GetProperty("Id")?.GetValue(x, null) == id)!;
        }
    }
}