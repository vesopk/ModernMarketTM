using System.ComponentModel.DataAnnotations;

namespace ModernMarketTM.Web.Areas.Admin.Models.BindingModels
{
    public class AddCategorieBindingModel
    {
        [Required(ErrorMessage = "Името е задължително.")]
        [MinLength(3, ErrorMessage = "Името трябва да е поне 3 букви.")]
        [MaxLength(50, ErrorMessage = "Името трябва да е максимум 50 букви.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Наставката е задължителна.")]
        [MinLength(3, ErrorMessage = "Наставката трябва да е поне 3 букви.")]
        [MaxLength(50, ErrorMessage = "Наставката трябва да е максимум 50 букви.")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Типа е задължителен.")]
        [MinLength(3, ErrorMessage = "Типа трябва да е поне 3 букви.")]
        [MaxLength(50, ErrorMessage = "Типа трябва да е максимум 50 букви.")]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "Картинката е задължителна.")]
        [DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; }
    }
}
