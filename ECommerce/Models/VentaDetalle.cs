﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class VentaDetalle
    {
        [Key]
        public int VentaDetalleID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int VentaID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ProductoID { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        [Display(Name = "Tasa Impuesto")]
        public double Tasa { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public decimal Precio { get; set; }

        [Display(Name = "Cantidad")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public double Cantidad { get; set; }

        public virtual Venta Venta { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
