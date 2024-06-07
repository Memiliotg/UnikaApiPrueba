using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UnikaApiPrueba.Helper.Dto
{
    public class PersonUpdateDto
    {
        public int? Id { get; set; }

        public Guid? UniqueIdentifier { get; set; }
        [JsonIgnore]
        public string? Cedula { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Sex { get; set; }

        public DateTime BirthDate { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
