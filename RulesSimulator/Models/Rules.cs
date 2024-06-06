using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RulesSimulator.Models
{
    public class Rules
    {
        [Key]
        public int ID { get; set; }
        public int ruleTypeID { get; set; }
        public string symbol { get; set; }
        [Precision(18, 2)] public decimal MinPrice { get; set; }
        [Precision(18, 2)] public decimal MaxPrice { get; set; }
        [Precision(18, 2)] public decimal MinQty { get; set; }
        [Precision(18, 2)] public decimal MaxQty { get; set; }

        public string Description { get; set; }
    }
}

