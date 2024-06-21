using System;
using System.Collections.Generic;

namespace CCMN_API.Models;

public partial class TbCategoria
{
    public int CatCodigo { get; set; }

    public string? CatNome { get; set; }

    public virtual ICollection<TbProduto> TbProdutos { get; set; } = new List<TbProduto>();
}
