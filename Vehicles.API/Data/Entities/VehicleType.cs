using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Data.Entities
{
    public class VehicleType
    {
        [Key]
        public int ID { set; get; }

        [Display(Name ="Type")]
        [MaxLength(50,ErrorMessage ="This field {0} cannot over {1} digits.")]
        [Required(ErrorMessage ="This field {0} is requiered.")]
        public string Description { set; get; }
    }
}
