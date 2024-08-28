using System;
using System.Collections.Generic;
using CCMN_API;
using CCMN_API.Models;
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

    public virtual DbSet<TbCategoria> TbCategorias { get; set; }

    public virtual DbSet<TbDespesaComunidadeEvento> TbDespesaComunidadeEventos { get; set; }

    public virtual DbSet<TbDespesaEvento> TbDespesaEventos { get; set; }

    public virtual DbSet<TbEvento> TbEventos { get; set; }

    public virtual DbSet<TbEventoPessoa> TbEventoPessoas { get; set; }

    public virtual DbSet<TbEventoQuarto> TbEventoQuartos { get; set; }

    public virtual DbSet<TbParticipantesCupon> TbParticipantesCupons { get; set; }

    public virtual DbSet<TbPromoco> TbPromocoes { get; set; }

    public virtual DbSet<TbPromocoesCupon> TbPromocoesCupons { get; set; }

    public virtual DbSet<TbPromocoesParticipante> TbPromocoesParticipantes { get; set; }

    public virtual DbSet<TbPromocoesPremio> TbPromocoesPremios { get; set; }

    public virtual DbSet<TbPromocoesSorteio> TbPromocoesSorteios { get; set; }

    public virtual DbSet<TbFormulario> TbFormularios { get; set; }

    public virtual DbSet<TbFornecedore> TbFornecedores { get; set; }

    public virtual DbSet<TbMovimentoProduto> TbMovimentoProdutos { get; set; }

    public virtual DbSet<TbPessoa> TbPessoas { get; set; }

    public virtual DbSet<TbProduto> TbProdutos { get; set; }

    public virtual DbSet<TbQuarto> TbQuartos { get; set; }

    public virtual DbSet<TbQuartoPessoa> TbQuartoPessoas { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("CCMNConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Add Collate pt-br
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

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

        modelBuilder.Entity<TbCategoria>(entity =>
        {
            entity.HasKey(e => e.CatCodigo);

            entity.ToTable("TB_CATEGORIAS");

            entity.Property(e => e.CatCodigo)
                .ValueGeneratedNever()
                .HasColumnName("CAT_CODIGO");
            entity.Property(e => e.CatNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CAT_NOME");
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

            entity.HasOne(d => d.ComCodigoNavigation).WithMany(p => p.TbDespesaComunidadeEventos)
                .HasForeignKey(d => d.ComCodigo)
                .HasConstraintName("ComunidadeEventoDespesa");

            entity.HasOne(d => d.EveCodigoNavigation).WithMany(p => p.TbDespesaComunidadeEventos)
                .HasForeignKey(d => d.EveCodigo)
                .HasConstraintName("DespesasEvento");
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

            entity.HasOne(d => d.EveCodigoNavigation).WithMany(p => p.TbDespesaEventos)
                .HasForeignKey(d => d.EveCodigo)
                .HasConstraintName("FK_TB_DESPESA_EVENTO_TB_EVENTOS");
        });

        modelBuilder.Entity<TbEvento>(entity =>
        {
            entity.HasKey(e => e.EveCodigo);

            entity.ToTable("TB_EVENTOS");

            entity.Property(e => e.EveCodigo)
                .ValueGeneratedNever()
                .HasColumnName("EVE_CODIGO");
            entity.Property(e => e.DseCozinha)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("DSE_COZINHA");
            entity.Property(e => e.DseHostiaria)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("DSE_HOSTIARIA");
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
            entity.Property(e => e.EveTipoCobranca)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("EVE_TIPO_COBRANCA");
            entity.Property(e => e.EveValor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("EVE_VALOR");
        });

        modelBuilder.Entity<TbEventoPessoa>(entity =>
        {
            entity.HasKey(e => e.EvpCodigo);

            entity.ToTable("TB_EVENTO_PESSOAS");

            entity.Property(e => e.EvpCodigo)
                .ValueGeneratedNever()
                .HasColumnName("EVP_CODIGO");
            entity.Property(e => e.EveCodigo).HasColumnName("EVE_CODIGO");
            entity.Property(e => e.EvpCobrante).HasColumnName("EVP_COBRANTE");
            entity.Property(e => e.EvpPagante).HasColumnName("EVP_PAGANTE");
            entity.Property(e => e.PesCodigo).HasColumnName("PES_CODIGO");

            entity.HasOne(d => d.EveCodigoNavigation).WithMany(p => p.TbEventoPessoas)
                .HasForeignKey(d => d.EveCodigo)
                .HasConstraintName("Evento");

            entity.HasOne(d => d.PesCodigoNavigation).WithMany(p => p.TbEventoPessoas)
                .HasForeignKey(d => d.PesCodigo)
                .HasConstraintName("Pessoa");
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

        modelBuilder.Entity<TbFormulario>(entity =>
        {
            entity.HasKey(e => e.ForCodigo);

            entity.ToTable("TB_FORMULARIOS");

            entity.Property(e => e.ForCodigo)
                .ValueGeneratedNever()
                .HasColumnName("FOR_CODIGO");
            entity.Property(e => e.ComCodigo).HasColumnName("COM_CODIGO");
            entity.Property(e => e.EveCodigo).HasColumnName("EVE_CODIGO");
            entity.Property(e => e.ForDatacriacao)
                .HasColumnType("datetime")
                .HasColumnName("FOR_DATACRIACAO");
            entity.Property(e => e.ForEndereco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOR_ENDERECO");
            entity.Property(e => e.ForNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FOR_NOME");
            entity.Property(e => e.ForStatus).HasColumnName("FOR_STATUS");
            entity.Property(e => e.ForTipo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("FOR_TIPO");

            entity.HasOne(d => d.ComCodigoNavigation).WithMany(p => p.TbFormularios)
                .HasForeignKey(d => d.ComCodigo)
                .HasConstraintName("ComunidadeFormulario");

            entity.HasOne(d => d.EveCodigoNavigation).WithMany(p => p.TbFormularios)
                .HasForeignKey(d => d.EveCodigo)
                .HasConstraintName("EventoFomulario");
        });

        modelBuilder.Entity<TbFornecedore>(entity =>
        {
            entity.HasKey(e => e.ForCodigo);

            entity.ToTable("TB_FORNECEDORES");

            entity.Property(e => e.ForCodigo)
                .ValueGeneratedNever()
                .HasColumnName("FOR_CODIGO");
            entity.Property(e => e.ForNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FOR_NOME");
        });

        modelBuilder.Entity<TbMovimentoProduto>(entity =>
        {
            entity.HasKey(e => e.MovCodigo).HasName("PK_TB_MOVIMENTO_ESTOQUE");

            entity.ToTable("TB_MOVIMENTO_PRODUTOS");

            entity.Property(e => e.MovCodigo)
                .ValueGeneratedNever()
                .HasColumnName("MOV_CODIGO");
            entity.Property(e => e.MovData)
                .HasColumnType("datetime")
                .HasColumnName("MOV_DATA");
            entity.Property(e => e.MovQuantidade).HasColumnName("MOV_QUANTIDADE");
            entity.Property(e => e.MovTipo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("MOV_TIPO");
            entity.Property(e => e.ProCodigo).HasColumnName("PRO_CODIGO");

            entity.HasOne(d => d.ProCodigoNavigation).WithMany(p => p.TbMovimentoProdutos)
                .HasForeignKey(d => d.ProCodigo)
                .HasConstraintName("MOVIMENTO_PRODUTO");
        });

        modelBuilder.Entity<TbParticipantesCupon>(entity =>
        {
            entity.HasKey(e => e.ParcupCodigo);

            entity.ToTable("TB_PARTICIPANTES_CUPONS");

            entity.Property(e => e.ParcupCodigo)
                .ValueGeneratedNever()
                .HasColumnName("PARCUP_CODIGO");
            entity.Property(e => e.CupCodigo).HasColumnName("CUP_CODIGO");
            entity.Property(e => e.ParCodigo).HasColumnName("PAR_CODIGO");

            entity.HasOne(d => d.CupCodigoNavigation).WithMany(p => p.TbParticipantesCupons)
                .HasForeignKey(d => d.CupCodigo)
                .HasConstraintName("CUPONS");

            entity.HasOne(d => d.ParCodigoNavigation).WithMany(p => p.TbParticipantesCupons)
                .HasForeignKey(d => d.ParCodigo)
                .HasConstraintName("PARTICIPANTES");
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

        modelBuilder.Entity<TbProduto>(entity =>
        {
            entity.HasKey(e => e.ProCodigo);

            entity.ToTable("TB_PRODUTOS");

            entity.Property(e => e.ProCodigo)
                .ValueGeneratedNever()
                .HasColumnName("PRO_CODIGO");
            entity.Property(e => e.CatCodigo).HasColumnName("CAT_CODIGO");
            entity.Property(e => e.ProCodBarras)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PRO_COD_BARRAS");
            entity.Property(e => e.ProDescricao)
                .HasColumnType("text")
                .HasColumnName("PRO_DESCRICAO");
            entity.Property(e => e.ProMedida)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PRO_MEDIDA");
            entity.Property(e => e.ProNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PRO_NOME");
            entity.Property(e => e.ProQuantidade).HasColumnName("PRO_QUANTIDADE");
            entity.Property(e => e.ProUniMedida)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("PRO_UNI_MEDIDA");
            entity.Property(e => e.ProValor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("PRO_VALOR");

            entity.HasOne(d => d.CatCodigoNavigation).WithMany(p => p.TbProdutos)
                .HasForeignKey(d => d.CatCodigo)
                .HasConstraintName("CATEGORIA");
        });

        modelBuilder.Entity<TbPromoco>(entity =>
        {
            entity.HasKey(e => e.ProCodigo).HasName("PK_TB_PROMOCAO");

            entity.ToTable("TB_PROMOCOES");

            entity.Property(e => e.ProCodigo).HasColumnName("PRO_CODIGO");
            entity.Property(e => e.ProDatafim)
                .HasColumnType("datetime")
                .HasColumnName("PRO_DATAFIM");
            entity.Property(e => e.ProDatainicio)
                .HasColumnType("datetime")
                .HasColumnName("PRO_DATAINICIO");
            entity.Property(e => e.ProDescricao)
                .HasColumnType("text")
                .HasColumnName("PRO_DESCRICAO");
            entity.Property(e => e.ProNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PRO_NOME");
        });

        modelBuilder.Entity<TbPromocoesCupon>(entity =>
        {
            entity.HasKey(e => e.CupCodigo).HasName("PK_TB_CUPOM");

            entity.ToTable("TB_PROMOCOES_CUPONS");

            entity.Property(e => e.CupCodigo).HasColumnName("CUP_CODIGO");
            entity.Property(e => e.CupNumero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUP_NUMERO");
            entity.Property(e => e.CupSorteado).HasColumnName("CUP_SORTEADO");
            entity.Property(e => e.CupVendido).HasColumnName("CUP_VENDIDO");
            entity.Property(e => e.ParCodigo).HasColumnName("PAR_CODIGO");
            entity.Property(e => e.ProCodigo).HasColumnName("PRO_CODIGO");

            entity.HasOne(d => d.ParCodigoNavigation).WithMany(p => p.TbPromocoesCupons)
                .HasForeignKey(d => d.ParCodigo)
                .HasConstraintName("FK_TB_PROMOCOES_CUPONS_TB_PROMOCOES_PARTICIPANTES");

            entity.HasOne(d => d.ProCodigoNavigation).WithMany(p => p.TbPromocoesCupons)
                .HasForeignKey(d => d.ProCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_CUPOM_TB_PROMOCAO");
        });

        modelBuilder.Entity<TbPromocoesParticipante>(entity =>
        {
            entity.HasKey(e => e.ParCodigo);

            entity.ToTable("TB_PROMOCOES_PARTICIPANTES");

            entity.Property(e => e.ParCodigo).HasColumnName("PAR_CODIGO");
            entity.Property(e => e.ParCidade)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAR_CIDADE");
            entity.Property(e => e.ParCpf)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAR_CPF");
            entity.Property(e => e.ParDatanasc)
                .HasColumnType("date")
                .HasColumnName("PAR_DATANASC");
            entity.Property(e => e.ParEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAR_EMAIL");
            entity.Property(e => e.ParEndereco)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PAR_ENDERECO");
            entity.Property(e => e.ParFone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAR_FONE");
            entity.Property(e => e.ParNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAR_NOME");
            entity.Property(e => e.ParUf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("PAR_UF");
            entity.Property(e => e.ProCodigo).HasColumnName("PRO_CODIGO");

            entity.HasOne(d => d.ProCodigoNavigation).WithMany(p => p.TbPromocoesParticipantes)
                .HasForeignKey(d => d.ProCodigo)
                .HasConstraintName("FK_TB_PROMOCOES_PARTICIPANTES_TB_PROMOCOES");
        });

        modelBuilder.Entity<TbPromocoesPremio>(entity =>
        {
            entity.HasKey(e => e.PreCodigo);

            entity.ToTable("TB_PROMOCOES_PREMIOS");

            entity.Property(e => e.PreCodigo).HasColumnName("PRE_CODIGO");
            entity.Property(e => e.PreDescricao)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("PRE_DESCRICAO");
            entity.Property(e => e.PreNome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PRE_NOME");
            entity.Property(e => e.ProCodigo).HasColumnName("PRO_CODIGO");

            entity.HasOne(d => d.ProCodigoNavigation).WithMany(p => p.TbPromocoesPremios)
                .HasForeignKey(d => d.ProCodigo)
                .HasConstraintName("FK_TB_PROMOCOES_PREMIOS_TB_PROMOCOES");
        });

        modelBuilder.Entity<TbPromocoesSorteio>(entity =>
        {
            entity.HasKey(e => e.SorCodigo);

            entity.ToTable("TB_PROMOCOES_SORTEIO");

            entity.Property(e => e.SorCodigo).HasColumnName("SOR_CODIGO");
            entity.Property(e => e.CupCodigo).HasColumnName("CUP_CODIGO");
            entity.Property(e => e.ParCodigo).HasColumnName("PAR_CODIGO");
            entity.Property(e => e.PreCodigo).HasColumnName("PRE_CODIGO");
            entity.Property(e => e.ProCodigo).HasColumnName("PRO_CODIGO");
            entity.Property(e => e.SorData)
                .HasColumnType("datetime")
                .HasColumnName("SOR_DATA");

            entity.HasOne(d => d.CupCodigoNavigation).WithMany(p => p.TbPromocoesSorteios)
                .HasForeignKey(d => d.CupCodigo)
                .HasConstraintName("FK_TB_PROMOCOES_SORTEIO_TB_PROMOCOES_CUPONS");

            entity.HasOne(d => d.ParCodigoNavigation).WithMany(p => p.TbPromocoesSorteios)
                .HasForeignKey(d => d.ParCodigo)
                .HasConstraintName("FK_TB_PROMOCOES_SORTEIO_TB_PROMOCOES_PARTICIPANTES");

            entity.HasOne(d => d.PreCodigoNavigation).WithMany(p => p.TbPromocoesSorteios)
                .HasForeignKey(d => d.PreCodigo)
                .HasConstraintName("FK_TB_PROMOCOES_SORTEIO_TB_PROMOCOES_PREMIOS");

            entity.HasOne(d => d.ProCodigoNavigation).WithMany(p => p.TbPromocoesSorteios)
                .HasForeignKey(d => d.ProCodigo)
                .HasConstraintName("FK_TB_PROMOCOES_SORTEIO_TB_PROMOCOES");
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
            entity.Property(e => e.PesChave).HasColumnName("PES_CHAVE");
            entity.Property(e => e.PesCheckin).HasColumnName("PES_CHECKIN");
            entity.Property(e => e.PesCodigo).HasColumnName("PES_CODIGO");
            entity.Property(e => e.PesNaovem).HasColumnName("PES_NAOVEM");
            entity.Property(e => e.QuaCodigo).HasColumnName("QUA_CODIGO");

            entity.HasOne(d => d.PesCodigoNavigation).WithMany(p => p.TbQuartoPessoas)
                .HasForeignKey(d => d.PesCodigo)
                .HasConstraintName("FK_TB_QUARTO_PESSOAS_TB_PESSOAS");

            entity.HasOne(d => d.QuaCodigoNavigation).WithMany(p => p.TbQuartoPessoas)
                .HasForeignKey(d => d.QuaCodigo)
                .HasConstraintName("FK_TB_QUARTO_PESSOAS_TB_QUARTOS");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.HasKey(e => e.UsuCodigo);

            entity.ToTable("TB_USUARIOS");

            entity.Property(e => e.UsuCodigo)
                .ValueGeneratedNever()
                .HasColumnName("USU_CODIGO");
            entity.Property(e => e.UsuAcessoEstoque).HasColumnName("USU_ACESSO_ESTOQUE");
            entity.Property(e => e.UsuAcessoFinanceiro).HasColumnName("USU_ACESSO_FINANCEIRO");
            entity.Property(e => e.UsuAcessoHospedagem).HasColumnName("USU_ACESSO_HOSPEDAGEM");
            entity.Property(e => e.UsuCodigoFirebase)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USU_CODIGO_FIREBASE");
            entity.Property(e => e.UsuEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USU_EMAIL");
            entity.Property(e => e.UsuNome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USU_NOME");
            entity.Property(e => e.UsuSenha)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USU_SENHA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
