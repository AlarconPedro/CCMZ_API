using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbParticipantesCupon
{
    public int ParcupCodigo { get; set; }

    public int? CupCodigo { get; set; }

    public int? ParCodigo { get; set; }

    public virtual TbPromocoesCupon? CupCodigoNavigation { get; set; }

    public virtual TbPromocoesParticipante? ParCodigoNavigation { get; set; }
}
