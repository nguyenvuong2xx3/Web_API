using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class LoaiModel
    {
        [Required]
        [MaxLength(255)]
        public string TenLoai { get; set; }
    }
}
