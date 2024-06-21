using System;
using System.Collections.Generic;

namespace CCMN_API.Models;

public partial class TbMovimentoProduto
{
    public int MovCodigo { get; set; }

    public int? ProCodigo { get; set; }

    public int? MovQuantidade { get; set; }

    public DateTime? MovData { get; set; }

    public string? MovTipo { get; set; }

    public virtual TbProduto? ProCodigoNavigation { get; set; }
}
