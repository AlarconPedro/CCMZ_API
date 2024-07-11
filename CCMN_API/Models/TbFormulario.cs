using System;
using System.Collections.Generic;

namespace CCMN_API.Models;

public partial class TbFormulario
{
    public int ForCodigo { get; set; }

    public string? ForNome { get; set; }

    public string? ForEndereco { get; set; }

    public int? ComCodigo { get; set; }

    public int? EveCodigo { get; set; }

    public DateTime? ForDatacriacao { get; set; }

    public bool? ForStatus { get; set; }

    public string? ForTipo { get; set; }

    public virtual TbComunidade? ComCodigoNavigation { get; set; }

    public virtual TbEvento? EveCodigoNavigation { get; set; }
}
