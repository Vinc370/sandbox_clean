using System.ComponentModel.DataAnnotations;

namespace dotnet.Models
{
    public class UserData
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Picture { get; set; }
        public string Bio { get; set; }
    }
}
