using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbDespesaComunidadeEvento
{
    public int DceCodigo { get; set; }

    public int? EveCodigo { get; set; }

    public int? ComCodigo { get; set; }

    public string? DceNome { get; set; }

    public int? DceQuantiadde { get; set; }

    public decimal? DceValor { get; set; }
}
