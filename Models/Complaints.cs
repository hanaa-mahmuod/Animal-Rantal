using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Animal_Rental.Models
{
    public class Complaints
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Please Enter  Email ")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "please enter correct email address")]
        public string Defendant_Email { get; set; }
        [Required]
        public string Notes { get; set; }
       
        public virtual Users Users { get; set; }
    }
}