using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroVehiculo.Models
{
    [Table("Gearbox")]
    public class Gearbox
    {
        [Key]
        public int GearboxId { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessage = " O tipo de Cambio do vehiculo deve ser informado! ")]
        [Display(Name = "Tipo de Cambio")]
        public string GearboxType { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
