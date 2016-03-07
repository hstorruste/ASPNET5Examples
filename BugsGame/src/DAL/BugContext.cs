using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class BugContext : DbContext
    {
        public BugContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Bugs> Bugs { get; set; }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<BugLists> BugLists { get; set; }
        public DbSet<Backgrounds> Backgrounds { get; set; }
        public DbSet<Scoreboards> Scoreboards { get; set; }
    }

    public class Bugs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }
        public int Strength { get; set; }
        public int Value { get; set; }
        public string Picture { get; set; }
    }

    public class Levels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Seconds { get; set; }
        [ForeignKey("Background")]
        public int BackgroundId { get; set; }

        public virtual Backgrounds Background { get; set; }
        public virtual List<BugLists> BugList { get; set; }
    }

    public class BugLists
    {
        [Key, ForeignKey("Level")]
        [Column(Order = 0)]
        public int LevelId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int SequenceNumber { get; set; }
        [ForeignKey("Bug")]
        public int BugId { get; set; }

        public virtual Levels Level { get; set; }
        public virtual Bugs Bug { get; set; }
    }

    public class Backgrounds
    {
        public int Id { get; set; }
        public string picture { get; set; }

    }

    public class Scoreboards
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }

    }
}
