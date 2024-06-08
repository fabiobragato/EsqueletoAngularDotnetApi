namespace API.Entities;

public class Usuario
{
    public int id { get; set; } // Por default, o entity framework entende que essa propriedade é uma chave primária

    public string nome { get; set; }

    public string email { get; set; }
    
}
