//ESTE VIEWMODEL ES UNA CLASSE QUE 
//DA FORMA A UN FORMULARIO, AGREGANDO ASI
//VALDIACIONES

using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class ComentarioViewModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Comentario { get; set; }
    }
}
