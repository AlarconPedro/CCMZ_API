using System;
using System.Collections.Generic;

namespace CCMZ_API.Models;

public partial class TbPessoa
{
    public int PesCodigo { get; set; }

    public string? PesNome { get; set; }

    public string? PesGenero { get; set; }

    public int? ComCodigo { get; set; }

    public virtual TbComunidade? ComCodigoNavigation { get; set; }

    public virtual ICollection<TbCasal> TbCasaiCasEsposaNavigations { get; set; } = new List<TbCasal>();

    public virtual ICollection<TbCasal> TbCasaiCasEsposoNavigations { get; set; } = new List<TbCasal>();

    public virtual ICollection<TbEventoPessoa> TbEventoPessoas { get; set; } = new List<TbEventoPessoa>();

    public virtual ICollection<TbQuartoPessoa> TbQuartoPessoas { get; set; } = new List<TbQuartoPessoa>();
}
