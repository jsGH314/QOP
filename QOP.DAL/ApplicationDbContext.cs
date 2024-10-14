using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QOP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace QOP.DAL
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }
        //public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<TestModel> TestModels { get; set; }

        public DbSet<StaffMember> StaffMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            /*modelBuilder.Entity<Journal>().HasData(
                new Journal
                {
                    Id = 1,
                    ClientId = 1,
                    //AppointmentId = 1,
                    //JournalEntry = "Journal 1 Content",
                    //EntryDate = DateTime.Now,
                },
                new Journal
                {
                    Id = 2,
                    ClientId = 2,  
                    //AppointmentId = 2,
                    //JournalEntry = "Journal 3 Content",
                });
            modelBuilder.Entity<JournalEntry>().HasData(
                new JournalEntry
                {
                    Id = 1,
                    ClientId = 1,
                    JournalId = 1,
                    Entry = "Journal 1 Entry 1 Content",
                    EntryDate = DateTime.Now
                    //AppointmentId = 1,
                    //JournalEntry = "Journal 1 Content",
                    //EntryDate = DateTime.Now,
                },
                new JournalEntry
                {
                    Id = 2,
                    ClientId = 2,
                    JournalId = 2,
                    Entry = "Journal 2 Entry 1 Content",
                    EntryDate = DateTime.Now
                    //AppointmentId = 2,
                    //JournalEntry = "Journal 3 Content",
                });*/
        }
    }
}
