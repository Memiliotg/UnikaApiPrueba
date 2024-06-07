using System.ComponentModel.DataAnnotations;

namespace UnikaApiPrueba.Models
{
    public class Person
    {
        [Key]
        public int? Id { get; set; }
 
        public Guid? UniqueIdentifier { get; set; }

        public string? Cedula { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        
    }
}
