using System;
using System.Collections.Generic;
using CCMN_API;
using CCMZ_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CCMZ_API;

public partial class CCMNContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public CCMNContext()
    {
    }

    public CCMNContext(DbContextOptions<CCMNContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public virtual DbSet<TbBloco> TbBlocos { get; set; }

    public virtual DbSet<TbCasai> TbCasais { get; set; }

    public virtual DbSet<TbComunidade> TbComunidades { get; set; }

    public virtual DbSet<TbDespesaComunidadeEvento> TbDespesaComunidadeEventos { get; set; }

    public virtual DbSet<TbDespesaEvento> TbDespesaEventos { get; set; }

    public virtual DbSet<TbEvento> TbEventos { get; set; }

    public virtual DbSet<TbEventoPessoa> TbEventoPessoas { get; set; }

    public virtual DbSet<TbEventoQuarto> TbEventoQuartos { get; set; }

    public virtual DbSet<TbPessoa> TbPessoas { get; set; }

    public virtual DbSet<TbQuarto> TbQuartos { get; set; }

    public virtual DbSet<TbQuartoPessoa> TbQuartoPessoas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("CCMNConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbBloco>(entity =>
        {
            entity.HasKey(e => e.BloCodigo);

            entity.ToTable("TB_BLOCOS");

            entity.Property(e => e.BloCodigo)
                .ValueGeneratedNever()
                .HasColumnName("BLO_CODIGO");
            entity.Property(e => e.BloNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BLO_NOME");
        });

        modelBuilder.Entity<TbCasai>(entity =>
        {
            entity.HasKey(e => e.CasCodigo);

            entity.ToTable("TB_CASAIS");

            entity.Property(e => e.CasCodigo)
                .ValueGeneratedNever()
                .HasColumnName("CAS_CODIGO");
            entity.Property(e => e.CasEsposa).HasColumnName("CAS_ESPOSA");
            entity.Property(e => e.CasEsposo).HasColumnName("CAS_ESPOSO");

            entity.HasOne(d => d.CasEsposaNavigation).WithMany(p => p.TbCasaiCasEsposaNavigations)
                .HasForeignKey(d => d.CasEsposa)
                .HasConstraintName("FK_ESPOSA");

            entity.HasOne(d => d.CasEsposoNavigation).WithMany(p => p.TbCasaiCasEsposoNavigations)
                .HasForeignKey(d => d.CasEsposo)
                .HasConstraintName("FK_ESPOSO");
        });

        modelBuilder.Entity<TbComunidade>(entity =>
        {
            entity.HasKey(e => e.ComCodigo);

            entity.ToTable("TB_COMUNIDADE");

            entity.Property(e => e.ComCodigo)
                .ValueGeneratedNever()
                .HasColumnName("COM_CODIGO");
            entity.Property(e => e.ComCidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COM_CIDADE");
            entity.Property(e => e.ComNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COM_NOME");
            entity.Property(e => e.ComUf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("COM_UF");
        });

        modelBuilder.Entity<TbDespesaComunidadeEvento>(entity =>
        {
            entity.HasKey(e => e.DceCodigo);

            entity.ToTable("TB_DESPESA_COMUNIDADE_EVENTO");

            entity.Property(e => e.DceCodigo)
                .ValueGeneratedNever()
                .HasColumnName("DCE_CODIGO");
            entity.Property(e => e.ComCodigo).HasColumnName("COM_CODIGO");
            entity.Property(e => e.DceNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DCE_NOME");
            entity.Property(e => e.DceQuantiadde).HasColumnName("DCE_QUANTIADDE");
            entity.Property(e => e.DceValor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("DCE_VALOR");
            entity.Property(e => e.EveCodigo).HasColumnName("EVE_CODIGO");
        });

        modelBuilder.Entity<TbDespesaEvento>(entity =>
        {
            entity.HasKey(e => e.DseCodigo);

            entity.ToTable("TB_DESPESA_EVENTO");

            entity.Property(e => e.DseCodigo)
                .ValueGeneratedNever()
                .HasColumnName("DSE_CODIGO");
            entity.Property(e => e.DseNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DSE_NOME");
            entity.Property(e => e.DseQuantidade).HasColumnName("DSE_QUANTIDADE");
            entity.Property(e => e.DseValor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("DSE_VALOR");
            entity.Property(e => e.EveCodigo).HasColumnName("EVE_CODIGO");
        });

        modelBuilder.Entity<TbEvento>(entity =>
        {
            entity.HasKey(e => e.EveCodigo);

            entity.ToTable("TB_EVENTOS");

            entity.Property(e => e.EveCodigo)
                .ValueGeneratedNever()
                .HasColumnName("EVE_CODIGO");
            entity.Property(e => e.EveDatafim)
                .HasColumnType("datetime")
                .HasColumnName("EVE_DATAFIM");
            entity.Property(e => e.EveDatainicio)
                .HasColumnType("datetime")
                .HasColumnName("EVE_DATAINICIO");
            entity.Property(e => e.EveNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EVE_NOME");
        });

        modelBuilder.Entity<TbEventoPessoa>(entity =>
        {
            entity.HasKey(e => e.EvpCodigo);

            entity.ToTable("TB_EVENTO_PESSOAS");

            entity.Property(e => e.EvpCodigo)
                .ValueGeneratedNever()
                .HasColumnName("EVP_CODIGO");
            entity.Property(e => e.EveCodigo).HasColumnName("EVE_CODIGO");
            entity.Property(e => e.PesCodigo).HasColumnName("PES_CODIGO");
            entity.Property(e => e.EvpPagante).HasColumnName("EVP_PAGANTE");
            entity.Property(e => e.EvpCobrante).HasColumnName("EVP_COBRANTE");

            entity.HasOne(d => d.PesCodigoNavigation).WithMany(p => p.TbEventoPessoas)
                .HasForeignKey(d => d.PesCodigo)
                .HasConstraintName("FK_TB_EVENTO_PESSOAS_TB_EVENTOS");
        });

        modelBuilder.Entity<TbEventoQuarto>(entity =>
        {
            entity.HasKey(e => e.EvqCodigo);

            entity.ToTable("TB_EVENTO_QUARTOS");

            entity.Property(e => e.EvqCodigo)
                .ValueGeneratedNever()
                .HasColumnName("EVQ_CODIGO");
            entity.Property(e => e.EveCodigo).HasColumnName("EVE_CODIGO");
            entity.Property(e => e.QuaCodigo).HasColumnName("QUA_CODIGO");

            entity.HasOne(d => d.EveCodigoNavigation).WithMany(p => p.TbEventoQuartos)
                .HasForeignKey(d => d.EveCodigo)
                .HasConstraintName("FK_TB_EVENTO_QUARTOS_TB_EVENTOS");

            entity.HasOne(d => d.QuaCodigoNavigation).WithMany(p => p.TbEventoQuartos)
                .HasForeignKey(d => d.QuaCodigo)
                .HasConstraintName("FK_TB_EVENTO_QUARTOS_TB_QUARTOS");
        });

        modelBuilder.Entity<TbPessoa>(entity =>
        {
            entity.HasKey(e => e.PesCodigo);

            entity.ToTable("TB_PESSOAS");

            entity.Property(e => e.PesCodigo)
                .ValueGeneratedNever()
                .HasColumnName("PES_CODIGO");
            entity.Property(e => e.ComCodigo).HasColumnName("COM_CODIGO");
            entity.Property(e => e.PesCatequista)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("PES_CATEQUISTA");
            entity.Property(e => e.PesGenero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("PES_GENERO");
            entity.Property(e => e.PesNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PES_NOME");
            entity.Property(e => e.PesObservacao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("PES_OBSERVACAO");
            entity.Property(e => e.PesResponsavel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("PES_RESPONSAVEL");
            entity.Property(e => e.PesSalmista)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("PES_SALMISTA");

            entity.HasOne(d => d.ComCodigoNavigation).WithMany(p => p.TbPessoas)
                .HasForeignKey(d => d.ComCodigo)
                .HasConstraintName("FK_TB_PESSOAS_TB_COMUNIDADE");
        });

        modelBuilder.Entity<TbQuarto>(entity =>
        {
            entity.HasKey(e => e.QuaCodigo);

            entity.ToTable("TB_QUARTOS");

            entity.Property(e => e.QuaCodigo)
                .ValueGeneratedNever()
                .HasColumnName("QUA_CODIGO");
            entity.Property(e => e.BloCodigo).HasColumnName("BLO_CODIGO");
            entity.Property(e => e.QuaNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("QUA_NOME");
            entity.Property(e => e.QuaQtdcamas).HasColumnName("QUA_QTDCAMAS");

            entity.HasOne(d => d.BloCodigoNavigation).WithMany(p => p.TbQuartos)
                .HasForeignKey(d => d.BloCodigo)
                .HasConstraintName("FK_TB_QUARTOS_TB_BLOCOS");
        });

        modelBuilder.Entity<TbQuartoPessoa>(entity =>
        {
            entity.HasKey(e => e.QupCodigo);

            entity.ToTable("TB_QUARTO_PESSOAS");

            entity.Property(e => e.QupCodigo)
                .ValueGeneratedNever()
                .HasColumnName("QUP_CODIGO");
            entity.Property(e => e.PesCodigo).HasColumnName("PES_CODIGO");
            entity.Property(e => e.QuaCodigo).HasColumnName("QUA_CODIGO");
            entity.Property(e => e.PesChave).HasColumnName("PES_CHAVE");
            entity.Property(e => e.PesCheckin).HasColumnName("PES_CHECKIN");
            entity.Property(e => e.PesNaovem).HasColumnName("PES_NAOVEM");

            entity.HasOne(d => d.PesCodigoNavigation).WithMany(p => p.TbQuartoPessoas)
                .HasForeignKey(d => d.PesCodigo)
                .HasConstraintName("FK_TB_QUARTO_PESSOAS_TB_PESSOAS");

            entity.HasOne(d => d.QuaCodigoNavigation).WithMany(p => p.TbQuartoPessoas)
                .HasForeignKey(d => d.QuaCodigo)
                .HasConstraintName("FK_TB_QUARTO_PESSOAS_TB_QUARTOS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
