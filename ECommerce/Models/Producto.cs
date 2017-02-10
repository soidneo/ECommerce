using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ECommerce.Models
{
    public class Producto
    {
        [Key]
        public int IProductoID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una empresa")]
        [Index("Producto_EmpresaID_Descripcion_Index", 1, IsUnique = true)]
        [Index("Producto_EmpresaID_BarCode_Index", 1, IsUnique = true)]
        public int EmpresaID { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1}", MinimumLength = 3)]
        [Index("Producto_EmpresaID_Descripcion_Index", 2, IsUnique = true)]
        public string Descripcion { get; set; }

        [Display(Name = "Código de barras")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(13, ErrorMessage = "El campo {0} debe tener entre {2} y {1}", MinimumLength = 3)]
        [Index("Producto_EmpresaID_BarCode_Index", 2, IsUnique = true)]
        public string BarCode { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        [Display(Name = "Categoria")]
        public int CategoriaID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        [Display(Name = "Categoria")]
        public int ImpuestoID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Debe seleccionar una {0} entre {1} y {2}")]
        public decimal Precio { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentarios { get; set; }

        public virtual Empresa Empresa { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Impuesto Impuesto { get; set; }


    }
}
