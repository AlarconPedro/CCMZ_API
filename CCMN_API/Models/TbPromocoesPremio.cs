using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbPromocoesPremio
{
    public int PreCodigo { get; set; }

    public string? PreNome { get; set; }

    public string? PreDescricao { get; set; }

    public int? ProCodigo { get; set; }

    public virtual TbPromoco? ProCodigoNavigation { get; set; }

    public virtual ICollection<TbPromocoesSorteio> TbPromocoesSorteios { get; set; } = new List<TbPromocoesSorteio>();
}
