using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Entities.Entities;

namespace Ticket.Entities.Context
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Urgencies> Urgencies { get; set; }
        public DbSet<Impacts> Impacts { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Situations> Situations { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Customers> Customers { get; set; }
    }
}
