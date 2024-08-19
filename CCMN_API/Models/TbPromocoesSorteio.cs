using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbPromocoesSorteio
{
    public int SorCodigo { get; set; }

    public DateTime? SorData { get; set; }

    public int? ParCodigo { get; set; }

    public int? PreCodigo { get; set; }

    public int? CupCodigo { get; set; }

    public int? ProCodigo { get; set; }

    public virtual TbPromocoesCupon? CupCodigoNavigation { get; set; }

    public virtual TbPromocoesParticipante? ParCodigoNavigation { get; set; }

    public virtual TbPromocoesPremio? PreCodigoNavigation { get; set; }

    public virtual TbPromoco? ProCodigoNavigation { get; set; }
}
