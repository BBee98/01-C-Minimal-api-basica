namespace c_basic_api.INE.AvailableOperations;

public class AvailableOperationsDTO: IAvailableOperationsModel
{
    public int Id { get; set; }
    public string Cod_IOE { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Codigo { get; set; } = "";
}