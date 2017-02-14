﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Cliente
    {
        [Key]
        [Display(Name = "CLiente")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        [Display(Name = "Empresa")]
        public int EmpresaID { get; set; }

        [Display(Name = "Correo E")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(256, ErrorMessage = "El campo {0} debe tener entre {2} y {1}", MinimumLength = 3)]
        [DataType(DataType.EmailAddress)]
        [Index("Cliente_UserName_Index", IsUnique = true)]
        public string UserName { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener entre {2} y {1}", MinimumLength = 3)]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener entre {2} y {1}", MinimumLength = 3)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1}", MinimumLength = 3)]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener entre {2} y {1}", MinimumLength = 3)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Departamento")]
        public int DepartamentoID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        [Display(Name = "Ciudad")]
        public int CiudadID { get; set; }

        

        [Display(Name = "Cliente")]
        public string FullName { get { return string.Format("{0} {1}", Nombre, Apellido); } }

        public virtual Departamento Departamento { get; set; }
        public virtual Ciudad Ciudad { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}