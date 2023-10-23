using System;
using System.Collections.Generic;

namespace CCMZ_API;

public partial class TbCasai
{
    public int CasCodigo { get; set; }

    public int? CasEsposo { get; set; }

    public int? CasEsposa { get; set; }

    public virtual TbPessoa? CasEsposaNavigation { get; set; }

    public virtual TbPessoa? CasEsposoNavigation { get; set; }
}
