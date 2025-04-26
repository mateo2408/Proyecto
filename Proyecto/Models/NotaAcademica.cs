using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models  //  Adjust namespace as needed
{
    public class Grade  //  Renamed from NotaAcademica
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El nombre del estudiante es requerido.")]
        public string Estudiante { get; set; }
        [Required(ErrorMessage = "El nombre de la materia es requerido.")]
        public string Materia { get; set; }
        [Required(ErrorMessage = "La calificación es requerida.")]
        [Range(0, 100, ErrorMessage = "La calificación debe estar entre 0 y 100.")]
        public decimal Calificacion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaEvaluacion { get; set; }
    }
}