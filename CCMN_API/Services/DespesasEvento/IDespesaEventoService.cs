namespace CCMN_API.Services.DespesasEvento;

public interface IDespesaEventoService
{
    Task<TbDespesaEvento> GetDespesaEvento(int codigoEvento);
}
