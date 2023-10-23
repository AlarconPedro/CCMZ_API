using System;
using System.Collections.Generic;
using CCMZ_API.Models;

namespace CCMZ_API;

public partial class TbPessoa
{
    public int PesCodigo { get; set; }

    public string? PesNome { get; set; }

    public string? PesGenero { get; set; }

    public int? ComCodigo { get; set; }

    public string? PesResponsavel { get; set; }

    public string? PesCatequista { get; set; }

    public string? PesSalmista { get; set; }

    public string? PesObservacao { get; set; }

    public virtual TbComunidade? ComCodigoNavigation { get; set; }

    public virtual ICollection<TbCasai> TbCasaiCasEsposaNavigations { get; set; } = new List<TbCasai>();

    public virtual ICollection<TbCasai> TbCasaiCasEsposoNavigations { get; set; } = new List<TbCasai>();

    public virtual ICollection<TbEventoPessoa> TbEventoPessoas { get; set; } = new List<TbEventoPessoa>();

    public virtual ICollection<TbQuartoPessoa> TbQuartoPessoas { get; set; } = new List<TbQuartoPessoa>();
}
