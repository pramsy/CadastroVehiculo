using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroVehiculo.Models
{
    [Table("Fuels")]
    public class Fuel
    {
        [Key]
        public int FuelId { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = " O tipo de combustivel vehiculo deve ser informado! ")]
        [Display(Name ="Tipo de Combustivel")]
        public string FuelType { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
