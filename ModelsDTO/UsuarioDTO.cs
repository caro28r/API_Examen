namespace API_Examen.ModelsDTOs
{
    public class UsuarioDTO
    {
        public int UsuarioID { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string? Telefono { get; set; }

        public string Contrasennia { get; set; } = null!;

        public int StrikeCount { get; set; }

        public string Correo { get; set; } = null!;

        public string? JobDescription { get; set; }

        public int UserStatusId { get; set; }

        public int CountryId { get; set; }

        public int RolID { get; set; }
    }
}
