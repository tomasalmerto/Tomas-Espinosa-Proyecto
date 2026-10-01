using System.ComponentModel.DataAnnotations;

namespace Heladeria.Data.Entidades
{
    public class Categoria
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

    }
}
