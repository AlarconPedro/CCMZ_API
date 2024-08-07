﻿using System;
using System.Collections.Generic;

namespace CCMN_API.Models;

public partial class TbEvento
{
    public int EveCodigo { get; set; }

    public string? EveNome { get; set; }

    public DateTime? EveDatainicio { get; set; }

    public DateTime? EveDatafim { get; set; }

    public decimal? EveValor { get; set; }

    public string? EveTipoCobranca { get; set; }

    public decimal? DseHostiaria { get; set; }

    public decimal? DseCozinha { get; set; }

    public virtual ICollection<TbDespesaComunidadeEvento> TbDespesaComunidadeEventos { get; set; } = new List<TbDespesaComunidadeEvento>();

    public virtual ICollection<TbDespesaEvento> TbDespesaEventos { get; set; } = new List<TbDespesaEvento>();

    public virtual ICollection<TbEventoPessoa> TbEventoPessoas { get; set; } = new List<TbEventoPessoa>();

    public virtual ICollection<TbEventoQuarto> TbEventoQuartos { get; set; } = new List<TbEventoQuarto>();

    public virtual ICollection<TbFormulario> TbFormularios { get; set; } = new List<TbFormulario>();
}
