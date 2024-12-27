using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pjGestionEmpleados.Entidades
{
    public class E_Empleado
    {
        public int ID_Empleado { get; set; }
        public string Nombre_Empleado { get; set; }
        public string Direccion_Empleado { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public String Telefono_Empleado { get; set; }

        public decimal Salario_Empleado { get; set; }
        public int ID_Departamento { get; set; }
        public int ID_Cargo { get; set; }



    }
}
