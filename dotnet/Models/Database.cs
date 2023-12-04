using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.Models
{
    public class Database
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("UserData")]
        public int DataID { get; set; }
        public UserData? Data { get; set; }
    }
}
