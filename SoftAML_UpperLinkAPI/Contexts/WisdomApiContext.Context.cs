﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoftAML_UpperLinkAPI.Contexts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WisdomApiEntities : DbContext
    {
        public WisdomApiEntities()
            : base("name=WisdomApiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PepList> PepLists { get; set; }
        public virtual DbSet<Watchlist> Watchlists { get; set; }
        public virtual DbSet<Ambassador> Ambassadors { get; set; }
    
        public virtual ObjectResult<spPepList_Result> spPepList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spPepList_Result>("spPepList");
        }
    }
}
