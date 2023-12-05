using System.ComponentModel.DataAnnotations;

namespace dotnet.Models
{
    public class Person
    {
        [Key]
        public int id { get; set; } 
        public String name { get; set; }
        public DateTime dob { get; set; }
    }
}
