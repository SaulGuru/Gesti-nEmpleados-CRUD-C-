using pjGestionEmpleados.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjGestionEmpleados.Presentacion
{
    public partial class frmRecuperarEmpleado : Form
    {

        private frmEmpleados formEmpleados;
        public frmRecuperarEmpleado(frmEmpleados formEmpleados)
        {
            InitializeComponent();
            this.formEmpleados =  formEmpleados;
        }

        #region Metodos
        private void CargarEmpleadosDesactivados(string cBusqueda)
        {
            D_Empleados Datos = new D_Empleados();
            dgvLista.DataSource = Datos.Listar_Empleados_Desactivados(cBusqueda);

            FormatoListaEmpleados();
        }

        private void FormatoListaEmpleados()
        {
            dgvLista.Columns[0].Width = 25;
            dgvLista.Columns[1].Width = 120;
            dgvLista.Columns[2].Width = 140;
            dgvLista.Columns[5].Width = 200;

        }


        private void ActivarEmpleados(int iCodigoEmpleado)
        {


            D_Empleados Datos = new D_Empleados();
            string respuesta = Datos.Activar_Empleado(formEmpleados.iCodigoEmpleado);

            if (respuesta == "OK")
            {
                CargarEmpleadosDesactivados("%");

                MessageBox.Show("Registro Devuelto Correctamente!", "Sistema Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(respuesta, "Sistema Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
        }


        #endregion

        private void frmRecuperarEmpleado_Load(object sender, EventArgs e)
        {
            CargarEmpleadosDesactivados("%");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarEmpleadosDesactivados(txtBuscar.Text);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            

            if (formEmpleados.iCodigoEmpleado == 0)
            {
                MessageBox.Show("Selecciona un registro", "Sistema de Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult resultado = MessageBox.Show("¿Estas seguro de devolver empleado?", "Sistema de Gestión de empleados", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                {
                    ActivarEmpleados(formEmpleados.iCodigoEmpleado);
                    formEmpleados.CargarEmpleados("%");
                }
            }
            formEmpleados.iCodigoEmpleado = 0;


        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            formEmpleados.iCodigoEmpleado = Convert.ToInt32(dgvLista.CurrentRow.Cells["ID"].Value);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
                

                this.Close();

        }
    }
}
