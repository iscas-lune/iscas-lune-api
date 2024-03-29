﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using iscaslune.Api.Domain.Context;

#nullable disable

namespace iscaslune.Api.Migrations
{
    [DbContext(typeof(IscasLuneContext))]
    [Migration("20231220135645_PesosMigration")]
    partial class PesosMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("iscas_lune_api.Domain.Entities.PrecoProduto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<long>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Numero"));

                    b.Property<decimal>("Preco")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<decimal?>("PrecoCusto")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<decimal?>("PrecoPromocional")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<Guid>("TamanhoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TamanhoId")
                        .IsUnique();

                    b.ToTable("PrecosProduto");
                });

            modelBuilder.Entity("iscas_lune_api.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Numero"));

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.Banner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<byte[]>("Foto")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<long>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Numero"));

                    b.HasKey("Id");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<byte[]>("Foto")
                        .HasColumnType("bytea");

                    b.Property<long>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Numero"));

                    b.HasKey("Id");

                    b.HasIndex("Descricao");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.Peso", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Numero"));

                    b.HasKey("Id");

                    b.HasIndex("Descricao");

                    b.ToTable("Pesos");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.PesosProdutos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PesoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PesoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("PesosProdutos");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("EspecificacaoTecnica")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<byte[]>("Foto")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<long>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Numero"));

                    b.Property<string>("Referencia")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("Descricao");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.Tamanho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Numero"));

                    b.HasKey("Id");

                    b.HasIndex("Descricao");

                    b.ToTable("Tamanhos");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.TamanhosProdutos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TamanhoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("TamanhoId");

                    b.ToTable("TamanhosProdutos");
                });

            modelBuilder.Entity("iscas_lune_api.Domain.Entities.PrecoProduto", b =>
                {
                    b.HasOne("iscaslune.Api.Domain.Entities.Tamanho", "Tamanho")
                        .WithOne("PrecoProduto")
                        .HasForeignKey("iscas_lune_api.Domain.Entities.PrecoProduto", "TamanhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tamanho");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.PesosProdutos", b =>
                {
                    b.HasOne("iscaslune.Api.Domain.Entities.Peso", "Peso")
                        .WithMany()
                        .HasForeignKey("PesoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iscaslune.Api.Domain.Entities.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Peso");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.Produto", b =>
                {
                    b.HasOne("iscaslune.Api.Domain.Entities.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.TamanhosProdutos", b =>
                {
                    b.HasOne("iscaslune.Api.Domain.Entities.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iscaslune.Api.Domain.Entities.Tamanho", "Tamanho")
                        .WithMany()
                        .HasForeignKey("TamanhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");

                    b.Navigation("Tamanho");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("iscaslune.Api.Domain.Entities.Tamanho", b =>
                {
                    b.Navigation("PrecoProduto")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
