using System;
using System.Collections.Generic;

namespace CCMZ_API.Models;

public partial class TbEventoQuarto
{
    public int EvqCodigo { get; set; }

    public int? QuaCodigo { get; set; }

    public int? EveCodigo { get; set; }

    public virtual TbEvento? EveCodigoNavigation { get; set; }

    public virtual TbQuarto? QuaCodigoNavigation { get; set; }
}
