using System;
using System.Collections.Generic;

namespace CCMZ_API.Models;

public partial class TbQuartoPessoa
{
    public int QupCodigo { get; set; }

    public int? PesCodigo { get; set; }

    public int? QuaCodigo { get; set; }

    public virtual TbPessoa? PesCodigoNavigation { get; set; }

    public virtual TbQuarto? QuaCodigoNavigation { get; set; }
}
