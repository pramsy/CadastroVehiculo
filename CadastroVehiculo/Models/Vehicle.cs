using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroVehiculo.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = " O modelo do vehiculo deve ser informado! ")]
        [MaxLength(100)]
        [Display(Name = "Modelo do Vehiculo")]
        public string VehicleModel { get; set; }

        [Required(ErrorMessage = " A Marca do vehiculo deve ser informado! ")]
        [MaxLength(100)]
        [Display(Name = "Marca do Vehiculo")]
        public string VehicleBrand { get; set; }

        [Required(ErrorMessage = " O numero de porta do vehiculo deve ser informado! ")]
        [Display(Name = "Quantidade de Portas")]
        [Range(1,6, ErrorMessage="O valor deve estar entre 1 a 6")]
        public uint VehicleNomberOfDoors { get; set; }
        [Required(ErrorMessage = " O ano de fabricacao deve ser informado! ")]
        [Display(Name = "Ano de Fabricacao")]
        public DateTime VehicleYearOfManifacture { get; set; }
       
        public int GearboxId { get; set; }
        public virtual Gearbox Gearbox { get; set; }

        public int FuelId { get; set; }
        public virtual Fuel Fuel { get; set; }
    }
}