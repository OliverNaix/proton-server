using Microsoft.EntityFrameworkCore;
using Proton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proton.DatabaseContext
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        { 
            
        }

        public int AccountExists(string email, string password)
        {
            try
            {
                return Accounts.Where(t => (t.Email == email && t.Password == password)).First().Id;
            }
            catch
            {
                return 0;
            }
        }
        public Account FindAccountById(int id)
        {
            return Accounts.Where(t => t.Id == id).FirstOrDefault();
        }

        public List<Account> FindAccountsByEmail(string email)
        {
            return Accounts.Where(t => t.Email.Contains(email)).ToList();
        }

        public IQueryable<Account> FindAccountByUsername(string username)
        {
            return Accounts.Where(t => t.Username == username);
        }

        public bool AccountExists(string email)
        {
            return Accounts.Where(t => t.Email == email).Count() > 0;
        }

        public void CreateAccount(RegisterBindingModel registerBindingModel)
        {
            Accounts.Add(new Account()
            {
                Email = registerBindingModel.Email,
                Password = registerBindingModel.Password,
                Datetime = registerBindingModel.Datetime,
                Address = registerBindingModel.Address
            });
        }

    }

    public class Account
    { 
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Datetime { get; set; }
    }
}
