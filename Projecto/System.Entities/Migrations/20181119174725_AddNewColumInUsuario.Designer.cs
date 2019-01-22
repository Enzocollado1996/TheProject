﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using System.Entities;

namespace System.Entities.Migrations
{
    [DbContext(typeof(SystemContext))]
    [Migration("20181119174725_AddNewColumInUsuario")]
    partial class AddNewColumInUsuario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("System.Entities.Auditoria", b =>
                {
                    b.Property<int>("AuditoriaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Descripcion");

                    b.Property<string>("Entity");

                    b.Property<string>("EntityId");

                    b.Property<bool>("Habilitado");

                    b.Property<DateTime?>("TSCreate");

                    b.Property<DateTime?>("TSEliminado");

                    b.Property<DateTime?>("TSModificado");

                    b.Property<string>("TransactionId");

                    b.Property<string>("UserName");

                    b.Property<string>("Value");

                    b.HasKey("AuditoriaId");

                    b.ToTable("Auditoria");
                });

            modelBuilder.Entity("System.Entities.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellido");

                    b.Property<string>("Contrasena");

                    b.Property<bool>("ContrasenaActualizada");

                    b.Property<string>("Email");

                    b.Property<bool>("Habilitado");

                    b.Property<string>("Nombre");

                    b.Property<DateTime>("TSCreado");

                    b.Property<DateTime?>("TSEliminado");

                    b.Property<DateTime?>("TSModificado");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("System.Entities.UsuarioRol", b =>
                {
                    b.Property<int>("RolId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("RolId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioRol");
                });

            modelBuilder.Entity("System.Entities.UsuarioToken", b =>
                {
                    b.Property<int>("UsuarioTokenId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaExpiracion");

                    b.Property<string>("Token");

                    b.Property<bool>("Usado");

                    b.Property<int>("UsuarioId");

                    b.HasKey("UsuarioTokenId");

                    b.ToTable("UsuarioToken");
                });

            modelBuilder.Entity("System.Entities.UsuarioRol", b =>
                {
                    b.HasOne("System.Entities.Rol", "Rol")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("System.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
