﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace practical_task_17
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MSSQLLocalOnlineStoreEntities : DbContext
    {
        public MSSQLLocalOnlineStoreEntities()
            : base("name=MSSQLLocalOnlineStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TableInfoBuyer> TableInfoBuyer { get; set; }
        public virtual DbSet<TableLoginPassword> TableLoginPassword { get; set; }
        public virtual DbSet<TableWorkingWithTheBuyer> TableWorkingWithTheBuyer { get; set; }
    }
}