using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbPromoco
{
    public int ProCodigo { get; set; }

    public string? ProNome { get; set; }

    public DateTime? ProDatainicio { get; set; }

    public DateTime? ProDatafim { get; set; }

    public string? ProDescricao { get; set; }

    public virtual ICollection<TbPromocoesCupon> TbPromocoesCupons { get; set; } = new List<TbPromocoesCupon>();

    public virtual ICollection<TbPromocoesParticipante> TbPromocoesParticipantes { get; set; } = new List<TbPromocoesParticipante>();

    public virtual ICollection<TbPromocoesPremio> TbPromocoesPremios { get; set; } = new List<TbPromocoesPremio>();

    public virtual ICollection<TbPromocoesSorteio> TbPromocoesSorteios { get; set; } = new List<TbPromocoesSorteio>();
}
