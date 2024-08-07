﻿using CCMN_API.Models;
using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbEventoPessoa
{
    public int EvpCodigo { get; set; }

    public int? EveCodigo { get; set; }

    public int? PesCodigo { get; set; }

    public bool? EvpPagante { get; set; }

    public bool? EvpCobrante { get; set; }

    public virtual TbEvento? EveCodigoNavigation { get; set; }

    public virtual TbPessoa? PesCodigoNavigation { get; set; }
}
