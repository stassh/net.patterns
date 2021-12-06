using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using FlixOne.InventoryManagement.Models;

namespace FlixOne.InventoryManagement.Repository
{
    public interface IInventoryContext : IInventoryReadContext, IInventoryWriteContext { }

    public interface IInventoryReadContext
    {
        Book[] GetBooks();
    }
    public interface IInventoryWriteContext
    {
        bool AddBook(string name);
        bool UpdateQuantity(string name, int quantity);
    }

    public class InventoryContext : IInventoryContext
    {
        private static object inventoryContextLock = new object();

        private readonly ConcurrentDictionary<string, Book> books;

        public InventoryContext()
        {
            books = new ConcurrentDictionary<string, Book>();
        }

        //private static InventoryContext _context;
        //public static InventoryContext Singleton {
        //    get
        //    {
        //        if (_context != null) return _context;
        //        lock (inventoryContextLock)
        //        {
        //            _context ??= new InventoryContext();
        //        }
        //        return _context;
        //    }
        //}


        public Book[] GetBooks()
        {
            return books.Values.ToArray();
        }

        public bool AddBook(string name)
        {
            return books.TryAdd(name, new Book { Name = name });
        }

        public bool UpdateQuantity(string name, int quantity)
        {
            lock (inventoryContextLock)
            {
                books[name].Quantity += quantity;
            }
            return true;
        }
    }
}