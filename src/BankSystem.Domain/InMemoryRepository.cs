using System.Collections.Generic;
using System.Linq;

namespace BankSystem.Domain
{
    public class InMemoryRepository<T> : IRepository<T> where T : class 
    {
        protected readonly List<T> _items = new();
        
        public IEnumerable<T> GetAll() => _items;
        
        public void Add(T entity) => _items.Add(entity);

        // Реалізація методу, якого не вистачало
        public virtual T GetById(int id)
        {
            // Використовуємо рефлексію, щоб знайти поле Id, бо T — це будь-який клас
            return _items.FirstOrDefault(x => (int?)x.GetType().GetProperty("Id")?.GetValue(x, null) == id)!;
        }
    }
}