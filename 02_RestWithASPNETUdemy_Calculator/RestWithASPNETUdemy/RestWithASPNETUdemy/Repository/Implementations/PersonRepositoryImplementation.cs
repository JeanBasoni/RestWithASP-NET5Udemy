using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySQLContext _context;
        public PersonRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }
        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindByID(long id)
        {
            return _context.Persons.SingleOrDefault(x => x.Id == id);
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
                return person;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();
            var result = _context.Persons.SingleOrDefault(x => x.Id == person.Id);
            if (result != null)
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();

                }
                catch (Exception)
                {

                    throw;
                }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(x => x.Id == id);
            if (result != null)
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();

                }
                catch (Exception)
                {

                    throw;
                }
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(x => x.Id == id);
        }
    }
}
