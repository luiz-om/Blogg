
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Blogg.ViewModels
{
    public class EditorCategoryViewModel
    {

        [Required(ErrorMessage ="O nome é obrigatorio")]
        public string Name { get; set; }

        public string Slug { get; set; }
    }
}