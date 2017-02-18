using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class NuevaVentaVista
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1}", MinimumLength = 3)]
        [DataType(DataType.PhoneNumber)]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentarios { get; set; }

        public List<VentaDetalleTmp> Detalles { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get { return Detalles == null ? 0 : Detalles.Sum(d => d.Cantidad); } }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal TotalValor { get { return Detalles == null ? 0 : Detalles.Sum(d => d.Valor); } }
    }
}
