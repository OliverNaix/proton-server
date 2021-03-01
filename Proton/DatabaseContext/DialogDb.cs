using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proton.DatabaseContext
{
    public class DialogDbContext : DbContext
    {
        public DbSet<Dialog> Dialogs { get; set; }
        public DialogDbContext(DbContextOptions<DialogDbContext> options) : base(options)
        {

        }


    }

    public class Dialog
    { 
        public int Id { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
        public string Datetime { get; set; }
    }
}
