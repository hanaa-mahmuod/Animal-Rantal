using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Animal_Rental.Models
{
    public class Animals
    {
        [Key]
        public int A_Id { get; set; }

        [ForeignKey("Users")]
        public int User_Id { get; set; }
        public string Name { get; set; }
       
        [Required]
        [Range(0, 80)]
        public int Age { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Birth_Certificate { get; set; }
        [Required]
        public string Type { get; set; }
        public string Gender { get; set; }
        [NotMapped]
        public IFormFile? ClientFile { get; set; }
        public byte[] Animal_Images { get; set; }
        public virtual Users Users { get; set; }
    }
}
