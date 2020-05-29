﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RCA.Models;

namespace RCA.Data
{
    public class RCAContext : DbContext
    {
        public RCAContext(DbContextOptions<RCAContext> options) : base(options) { }

        public DbSet<Class_Book>                Class_Book { get; set; }
        public DbSet<Class_Company>             Class_Company { get; set; }
        public DbSet<Class_GroupLevel>          Class_GroupLevel { get; set; }
        public DbSet<Class_GroupLevelItem>      Class_GroupLevelItem { get; set; }
        public DbSet<Class_GroupLevelItemTax>   Class_GroupLevelItemTax { get; set; }
        public DbSet<Class_Guest>               Class_Guest { get; set; }
    }
}