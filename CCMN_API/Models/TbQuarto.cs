﻿using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbQuarto
{
    public int QuaCodigo { get; set; }

    public string? QuaNome { get; set; }

    public int? BloCodigo { get; set; }

    public int? QuaQtdcamas { get; set; }

    public virtual TbBloco? BloCodigoNavigation { get; set; }

    public virtual TbEventoQuarto? EveCodigoNavigation { get; set; }

    public virtual TbQuartoPessoa? QupCodigoNavigation { get; set; }

    public virtual ICollection<TbEventoQuarto> TbEventoQuartos { get; set; } = new List<TbEventoQuarto>();

    public virtual ICollection<TbQuartoPessoa> TbQuartoPessoas { get; set; } = new List<TbQuartoPessoa>();
}
