using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailSender.Models;

namespace EmailSender.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T item);
        public Task<IEnumerable<T>> FindAllAsync();
    }
}
