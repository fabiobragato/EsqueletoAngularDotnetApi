namespace API.Entities;

public class Usuario
{
    public int Id { get; set; } // Por default, o entity framework entende que essa propriedade é uma chave primária

    public string Email { get; set; }

    public bool EmailConfirmado { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public string Celular { get; set; }

    public bool EhAdmin { get; set; }

}
