using System.ComponentModel.DataAnnotations;

namespace CP_LAB_5.Models.ViewModels
{
    public class LabWorkViewModel
    {
        [Required(ErrorMessage = "Input is required.")]
        public string Input { get;set;}
        public string Output { get;set;}
    }
}
