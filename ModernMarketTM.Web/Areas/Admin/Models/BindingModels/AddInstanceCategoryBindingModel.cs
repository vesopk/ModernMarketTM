using System.ComponentModel.DataAnnotations;

namespace ModernMarketTM.Web.Areas.Admin.Models.BindingModels
{
    public class AddInstanceCategoryBindingModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Името е задължително.")]
        [MinLength(3, ErrorMessage = "Името трябва да е поне 3 букви.")]
        [MaxLength(50, ErrorMessage = "Името трябва да е максимум 50 букви.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Наставката е задължителна.")]
        [MinLength(3, ErrorMessage = "Наставката трябва да е поне 3 букви.")]
        [MaxLength(50, ErrorMessage = "Наставката трябва да е максимум 50 букви.")]
        public string Slug { get; set; }

        public string CategorySlug { get; set; }

        [Required(ErrorMessage = "Картинката е задължителна.")]
        [DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; }

        [Required(ErrorMessage = "Цената е задължителна.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Описанието е задължително.")]
        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
