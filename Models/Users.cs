using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animal_Rental.Models
{
    public class Users
    {
        [Key]
        public int U_Id { get; set; }



        [Display(Name = "FName")]
        [Required(ErrorMessage = "Please Enter Your First Name")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string First_Name { get; set; }


        [Display(Name = "LName")]
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string Last_Name { get; set; }



        [Display(Name = "National_ID")]
        [Required(ErrorMessage = "Please Enter Your National_ID ")]
        [RegularExpression(@"^\(?([0-9]{14})$",
        ErrorMessage = "Entered National_ID format is not valid.")]
        public string National_ID { get; set; }


        [Display(Name = "Age")]
        [Required(ErrorMessage = "Please Enter Your Age ")]
        [Range(18, 80)]
        public int Age { get; set; }



        [Required(ErrorMessage = "Please Enter Your Phone ")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Entered phone format is not valid.")]

        public string Phone { get; set; }



        [Display(Name = "Adress")]
        [Required]
        public string Address { get; set; }




        [Display(Name = "Job")]
        public string Jop { get; set; }



        [Display(Name = "Gender")]
        [Required]
        public string Gender { get; set; }



        [Display(Name = "EMail")]
        [Required(ErrorMessage = "Please Enter Your Email ")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "please enter correct email address")]
        public string Email { get; set; }



        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter Your Password ")]
        [StringLength(50, ErrorMessage = "The Password must be at least 8 characters long.", MinimumLength = 8)]       
        public string Password { get; set; }
       
        public virtual IList<Animals> Animals { get; set; }
        public virtual IList<Complaints> Complaints { get; set; }
    }
}
