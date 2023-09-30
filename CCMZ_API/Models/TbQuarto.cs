using System;
using System.Collections.Generic;

namespace CCMZ_API.Models;

public partial class TbQuarto
{
    public int QuaCodigo { get; set; }

    public string? QuaNome { get; set; }

    public int? BloCodigo { get; set; }

    public int? QuaQtdcamas { get; set; }

    public virtual TbBloco? BloCodigoNavigation { get; set; }

    public virtual ICollection<TbEventoQuarto> TbEventoQuartos { get; set; } = new List<TbEventoQuarto>();

    public virtual ICollection<TbQuartoPessoa> TbQuartoPessoas { get; set; } = new List<TbQuartoPessoa>();
}
