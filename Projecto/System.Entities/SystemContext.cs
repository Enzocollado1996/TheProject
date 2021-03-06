﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Entities.AuditExtensions;
using Microsoft.EntityFrameworkCore;

namespace System.Entities
{
	public class SystemContext : EFCoreAudit
	{
		public SystemContext(DbContextOptions<SystemContext> options) : base(options)
		{
            //Database.SetInitializer<SystemContext>(new SystemInitializer());
			//Configuration.LazyLoadingEnabled = false;
			//Configuration.ProxyCreationEnabled = false;
		}

	
		//Tablas
		public DbSet<Auditoria> Auditoria { set; get; }
		public DbSet<Parametro> Parametro { set; get; }
		public DbSet<Permiso> Permiso { set; get; }
		public DbSet<Rol> Rol { set; get; }
		public DbSet<RolPermiso> RolPermiso { set; get; }
		public DbSet<Usuario> Usuario { set; get; }
		public DbSet<UsuarioRol> UsuarioRol { set; get; }
		public DbSet<Vista> Vista { set; get; }
		public DbSet<UsuarioToken> UsuarioToken { set; get; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Marca> Marca { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            
            //Habilitado
            modelBuilder.Entity<Auditoria>()
           .HasQueryFilter(auditoria => EF.Property<bool>(auditoria, "Habilitado") == true);
            modelBuilder.Entity<Usuario>()
           .HasQueryFilter(usuario => EF.Property<bool>(usuario, "Habilitado") == true);

            //Relationships

            modelBuilder.Entity<Producto>()
                .HasOne(e => e.Categoria)
                .WithMany(c => c.Productos);
            modelBuilder.Entity<Producto>()
                .HasOne(e => e.Marca)
                .WithMany(c => c.Productos);

            //rol-permiso
            modelBuilder.Entity<RolPermiso>()
            .HasKey(e => new { e.RolId, e.PermisoId });
            modelBuilder.Entity<RolPermiso>()
            .HasOne(x => x.Rol)
            .WithMany(x => x.RolPermisos)
            .HasForeignKey(x => x.RolId);
            modelBuilder.Entity<RolPermiso>()
            .HasOne(x => x.Permiso)
            .WithMany(x => x.RolPermisos)
            .HasForeignKey(x => x.PermisoId);

            //usuario backend - rol
            modelBuilder.Entity<UsuarioRol>()
            .HasKey(e => new { e.RolId, e.UsuarioId });
            modelBuilder.Entity<UsuarioRol>()
            .HasOne(x => x.Usuario)
            .WithMany(x => x.UsuarioRoles)
            .HasForeignKey(x => x.UsuarioId);
            modelBuilder.Entity<UsuarioRol>()
            .HasOne(x => x.Rol)
            .WithMany(x => x.UsuarioRoles)
            .HasForeignKey(x => x.RolId);

        }

    }
}
