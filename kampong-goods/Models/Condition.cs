using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class Condition
    {

        [Required, MinLength(2), MaxLength(8)]
        public string ConditionId { get; set; } = string.Empty;
        [Required, MaxLength(30)]
        public string ConditionName { get; set; } = string.Empty;

        public List<Product>? Products { get; set; }
    }
}
