using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Ciudad
    {
        [Key]
        public int CiudadID { get; set; }

        [Display(Name = "Nombre Ciudad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1}", MinimumLength = 3)]
        public string Nombre { get; set; }

        public int DepartamentoID { get; set; }

        public virtual Departamento Departamento { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
