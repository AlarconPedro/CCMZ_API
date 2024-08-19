using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbPromocoesCupon
{
    public int CupCodigo { get; set; }

    public string CupNumero { get; set; } = null!;

    public int ProCodigo { get; set; }

    public bool? CupSorteado { get; set; }

    public bool? CupVendido { get; set; }

    public int? ParCodigo { get; set; }

    public virtual TbPromocoesParticipante? ParCodigoNavigation { get; set; }

    public virtual TbPromoco ProCodigoNavigation { get; set; } = null!;

    public virtual ICollection<TbParticipantesCupon> TbParticipantesCupons { get; set; } = new List<TbParticipantesCupon>();

    public virtual ICollection<TbPromocoesSorteio> TbPromocoesSorteios { get; set; } = new List<TbPromocoesSorteio>();
}
