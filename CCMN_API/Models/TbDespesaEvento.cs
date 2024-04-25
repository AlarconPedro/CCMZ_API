using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbDespesaEvento
{
    public int DseCodigo { get; set; }

    public int? EveCodigo { get; set; }

    public string? DseNome { get; set; }

    public int? DseQuantidade { get; set; }

    public decimal? DseValor { get; set; }
}
