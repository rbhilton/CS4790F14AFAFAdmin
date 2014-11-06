using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace AFAF_Admin.Models
{
    public class AdminEntities : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventInstance> EventInstances { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Runners> Runners { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}