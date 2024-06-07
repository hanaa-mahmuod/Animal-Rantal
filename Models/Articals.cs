using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Animal_Rental.Models
{
    public class Articals
    {
        [Key]
        public int Id { get; set; }
        public string Type_Of_Animals { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }
    }
}
