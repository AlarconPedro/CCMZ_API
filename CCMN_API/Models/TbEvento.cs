using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbEvento
{
    public int EveCodigo { get; set; }

    public string? EveNome { get; set; }

    public DateTime? EveDatainicio { get; set; }

    public DateTime? EveDatafim { get; set; }

    public decimal? EveValor { get; set; }

    public string? EveTipoCobranca { get; set; }

    public virtual ICollection<TbDespesaComunidadeEvento> TbDespesaComunidadeEventos { get; set; } = new List<TbDespesaComunidadeEvento>();

    public virtual ICollection<TbDespesaEvento> TbDespesaEventos { get; set; } = new List<TbDespesaEvento>();

    public virtual ICollection<TbEventoPessoa> TbEventoPessoas { get; set; } = new List<TbEventoPessoa>();

    public virtual ICollection<TbEventoQuarto> TbEventoQuartos { get; set; } = new List<TbEventoQuarto>();
}
