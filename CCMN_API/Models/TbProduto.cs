using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbProduto
{
    public int ProCodigo { get; set; }

    public string? ProNome { get; set; }

    public string? ProCodBarras { get; set; }

    public decimal? ProValor { get; set; }

    public string? ProMedida { get; set; }

    public string? ProUniMedida { get; set; }

    public int? ProQuantidade { get; set; }

    public int? ProQuantidadeMin { get; set; }

    public int? CatCodigo { get; set; }

    public string? ProDescricao { get; set; }

    public string? ProImagem { get; set; }

    public virtual TbCategoria? CatCodigoNavigation { get; set; }

    public virtual ICollection<TbMovimentoProduto> TbMovimentoProdutos { get; set; } = new List<TbMovimentoProduto>();
}
