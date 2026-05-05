using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using BankSystem.Domain;

namespace BankSystem.Infrastructure
{
    public class JsonRepository<T> : IRepository<T> where T : class
    {
        private readonly string _filePath;
        private List<T> _items;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
            _items = Load();
        }

        public IEnumerable<T> GetAll() => _items;

        public void Add(T entity)
        {
            _items.Add(entity);
            Save();
        }

        public T GetById(int id)
        {
            // Пошук по Id через рефлексію
            return _items.FirstOrDefault(x => 
                (int)x.GetType().GetProperty("Id")?.GetValue(x, null)! == id);
        }

        private void Save()
        {
            var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        private List<T> Load()
        {
            if (!File.Exists(_filePath)) return new List<T>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
    }
}