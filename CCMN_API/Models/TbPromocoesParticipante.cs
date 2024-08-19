using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbPromocoesParticipante
{
    public int ParCodigo { get; set; }

    public string? ParNome { get; set; }

    public string? ParCpf { get; set; }

    public string? ParFone { get; set; }

    public string? ParEndereco { get; set; }

    public string? ParCidade { get; set; }

    public string? ParUf { get; set; }

    public DateTime? ParDatanasc { get; set; }

    public string? ParEmail { get; set; }

    public int? ProCodigo { get; set; }

    public virtual TbPromoco? ProCodigoNavigation { get; set; }

    public virtual ICollection<TbParticipantesCupon> TbParticipantesCupons { get; set; } = new List<TbParticipantesCupon>();

    public virtual ICollection<TbPromocoesCupon> TbPromocoesCupons { get; set; } = new List<TbPromocoesCupon>();

    public virtual ICollection<TbPromocoesSorteio> TbPromocoesSorteios { get; set; } = new List<TbPromocoesSorteio>();
}
