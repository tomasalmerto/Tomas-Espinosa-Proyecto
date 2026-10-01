using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heladeria.Data.Entidades
{
    public class Sabor
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del sabor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio por kilo es obligatorio.")]
        [Range(0.01, 100000, ErrorMessage = "El precio no puede ser negativo o 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioPorKilo { get; set; }

        public bool Disponible { get; set; } = true;




        [Required]
        public int CategoriaId { get; set; } 

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }
    }
}
