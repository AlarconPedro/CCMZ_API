using System;
using System.Collections.Generic;

namespace CCMN_API.Models;

public partial class TbUsuario
{
    public int UsuCodigo { get; set; }

    public string? UsuNome { get; set; }

    public string? UsuEmail { get; set; }

    public string? UsuSenha { get; set; }

    public string? UsuCodigoFirebase { get; set; }

    public bool? UsuAcessoHospedagem { get; set; }

    public bool? UsuAcessoFinanceiro { get; set; }

    public bool? UsuAcessoEstoque { get; set; }
}
