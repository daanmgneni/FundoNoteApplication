using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Services;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class FunDoContext: DbContext
    {
        public FunDoContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity> UserTable { get; set; }
    }
}
