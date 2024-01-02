using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EfCoreRelation.DTOs.ImageDt
{
    public class TableProductImgDto
    {
        public int Id { get; set; }
       
        public string? Productcode { get; set; }

        public byte[]? Productimage { get; set; }
    }
}
