// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eAuditoria.Data;

#nullable disable

namespace eAuditoria.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220106033737_InitialCreation")]
    partial class InitialCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("eAuditoria.Data.Repository.ModelEntity.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("CLIENTE");
                });

            modelBuilder.Entity("eAuditoria.Data.Repository.ModelEntity.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClassificacaoIndicativa")
                        .HasColumnType("int");

                    b.Property<short>("Lancamento")
                        .HasColumnType("smallint");

                    b.Property<string>("Tituto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("FILME");
                });

            modelBuilder.Entity("eAuditoria.Data.Repository.ModelEntity.Locacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataDevolucao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataLocacao")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Id_Cliente")
                        .HasColumnType("int");

                    b.Property<int>("Id_Filme")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Cliente");

                    b.HasIndex("Id_Filme");

                    b.ToTable("LOCACAO");
                });

            modelBuilder.Entity("eAuditoria.Data.Repository.ModelEntity.Locacao", b =>
                {
                    b.HasOne("eAuditoria.Data.Repository.ModelEntity.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("Id_Cliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuditoria.Data.Repository.ModelEntity.Filme", "Filme")
                        .WithMany()
                        .HasForeignKey("Id_Filme")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Filme");
                });
#pragma warning restore 612, 618
        }
    }
}
