using System.ComponentModel.DataAnnotations;

namespace RulesSimulator.Models
{
    public class Rules
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
