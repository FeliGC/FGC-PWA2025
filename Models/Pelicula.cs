using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FGC_PWA2025.Models;

public class Pelicula
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "El género es obligatorio")]
    [StringLength(50, ErrorMessage = "El género no puede superar los 50 caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    [Range(1900, 2100, ErrorMessage = "La fecha debe estar entre 1900 y 2100")]
    public int Fecha { get; set; }

    [Required(ErrorMessage = "La duración es obligatoria")]
    [StringLength(20, ErrorMessage = "La duración no puede superar los 20 caracteres")]
    public string Duracion { get; set; }

    [Required(ErrorMessage = "El costo es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser un número positivo")]
    [DataType(DataType.Currency)]
    public decimal Costo { get; set; }
}

public class Item
{
    public Pelicula Pelicula { get; set; }
    public int Cantidad { get; set; }
}