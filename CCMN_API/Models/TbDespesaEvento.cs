﻿using CCMZ_API;
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

    public decimal? DseCozinha { get; set; }

    public decimal? DseHostiaria { get; set; }

    public virtual TbEvento? EveCodigoNavigation { get; set; }
}
