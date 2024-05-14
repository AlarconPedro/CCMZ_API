using System;
using System.Collections.Generic;

namespace CCMN_API;

public partial class TbComunidade
{
    public int ComCodigo { get; set; }

    public string? ComNome { get; set; }

    public string? ComCidade { get; set; }

    public string? ComUf { get; set; }

    public virtual ICollection<TbDespesaComunidadeEvento> TbDespesaComunidadeEventos { get; set; } = new List<TbDespesaComunidadeEvento>();

    public virtual ICollection<TbPessoa> TbPessoas { get; set; } = new List<TbPessoa>();
}
