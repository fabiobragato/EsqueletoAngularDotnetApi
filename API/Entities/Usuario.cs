namespace API.Entities;

public class Usuario
{
    public int Id { get; set; } // Por default, o entity framework entende que essa propriedade é uma chave primária

    public string Username { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

}
