using pjGestionEmpleados.Datos;
using pjGestionEmpleados.Entidades;
using pjGestionEmpleados.Presentacion.Reportes;
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
    public partial class frmEmpleados : Form
    {
        public frmEmpleados()
        {
            InitializeComponent();
        }

        #region"Variables"

        public int iCodigoEmpleado = 0;
        bool bEstadoGuardar = true;

        #endregion

        #region "Metodos"

        public void CargarEmpleados(string cBusqueda)
        {
            D_Empleados Datos = new D_Empleados();
            dgvLista.DataSource = Datos.Listar_Empleados(cBusqueda);

            FormatoListaEmpleados();
        }

        private void FormatoListaEmpleados()
        {
            dgvLista.Columns[0].Width = 25;
            dgvLista.Columns[1].Width = 120;
            dgvLista.Columns[2].Width = 140;
            dgvLista.Columns[5].Width = 200;


        }

        private void CargarDepartamentos()
        {
           D_Departamentos Datos = new D_Departamentos();
            cmbDepartamento.DataSource = Datos.Listar_Departamentos();
            cmbDepartamento.ValueMember = "id_departamento";
            cmbDepartamento.DisplayMember = "nombre_departamento";
            cmbDepartamento.SelectedIndex = -1;
        }

        private void CargarCargos()
        {
            D_Cargos cargos = new D_Cargos();
            cmbCargo.DataSource = cargos.Listar_Cargos();
            cmbCargo.ValueMember = "id_cargo";
            cmbCargo.DisplayMember = "nombre_cargo";
            cmbCargo.SelectedIndex = -1;
        }

        private void ActivarTextos(bool bEstado)
        {
            txtNombre.Enabled = bEstado;
            txtDireccion.Enabled = bEstado;
            txtSalario.Enabled = bEstado;
            txtTelefono.Enabled = bEstado;
            cmbDepartamento.Enabled = bEstado;
            cmbCargo.Enabled = bEstado;
            dtpFechaNacimiento.Enabled = bEstado;

            txtBuscar.Enabled = !bEstado;
            dgvLista.Enabled = !bEstado;
        }

        private void ActivarBotones(bool bEstado)
        {  
            btnNuevo.Enabled = bEstado;
            btnActualizar.Enabled = bEstado;
            btnEliminar.Enabled = bEstado;
            btnReporte.Enabled = bEstado;
            

            btnGuardar.Visible = !bEstado;
            btnCancelar.Visible = !bEstado;
        }

        public void SeleccionarEmpleado()
        {
            iCodigoEmpleado = Convert.ToInt32(dgvLista.CurrentRow.Cells["ID"].Value);

            txtNombre.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Nombre"].Value);
            txtDireccion.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Dirección"].Value);
            txtTelefono.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Teléfono"].Value);
            txtSalario.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Salario"].Value);
            cmbDepartamento.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Departamento"].Value);
            cmbCargo.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Cargo"].Value);
            dtpFechaNacimiento.Value = Convert.ToDateTime(dgvLista.CurrentRow.Cells["Fecha Nacimiento"].Value);
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtSalario.Clear();
            txtBuscar.Clear();

            cmbCargo.SelectedIndex = -1;
            cmbDepartamento.SelectedIndex = -1;

            dtpFechaNacimiento.Value = DateTime.Now;

            iCodigoEmpleado = 0;
        }


        private void GuardarEmpleados()
        {
            E_Empleado Empleado = new E_Empleado();

            Empleado.Nombre_Empleado = txtNombre.Text;
            Empleado.Direccion_Empleado = txtDireccion.Text;
            Empleado.Telefono_Empleado = txtTelefono.Text;
            Empleado.Salario_Empleado = Convert.ToDecimal(txtSalario.Text);
            Empleado.Fecha_Nacimiento = dtpFechaNacimiento.Value;
            Empleado.ID_Departamento = Convert.ToInt32(cmbDepartamento.SelectedValue);
            Empleado.ID_Cargo = Convert.ToInt32(cmbCargo.SelectedValue);

            D_Empleados Datos = new D_Empleados();
            string respuesta = Datos.Guardar_Empleado(Empleado);

            if(respuesta == "OK")
            {
                CargarEmpleados("%");
                Limpiar();
                ActivarTextos(false);
                ActivarBotones(true);

                MessageBox.Show("Datos Guardados Correctamente!", "Sistema Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(respuesta, "Sistema Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ActualizarEmpleados()
        {
            E_Empleado Empleado = new E_Empleado();

            Empleado.ID_Empleado = iCodigoEmpleado;
            Empleado.Nombre_Empleado = txtNombre.Text;
            Empleado.Direccion_Empleado = txtDireccion.Text;
            Empleado.Telefono_Empleado = txtTelefono.Text;
            Empleado.Salario_Empleado = Convert.ToDecimal(txtSalario.Text);
            Empleado.Fecha_Nacimiento = dtpFechaNacimiento.Value;
            Empleado.ID_Departamento = Convert.ToInt32(cmbDepartamento.SelectedValue);
            Empleado.ID_Cargo = Convert.ToInt32(cmbCargo.SelectedValue);

            D_Empleados Datos = new D_Empleados();
            string respuesta = Datos.Actualizar_Empleado(Empleado);

            if (respuesta == "OK")
            {
                CargarEmpleados("%");
                Limpiar();
                ActivarTextos(false);
                ActivarBotones(true);

                MessageBox.Show("Datos Actualizados Correctamente!", "Sistema Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(respuesta, "Sistema Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DesactivarEmpleados(int iCodigoEmpleado)
        {
          
            D_Empleados Datos = new D_Empleados();
            string respuesta = Datos.Desactivar_Empleado(iCodigoEmpleado);

            if (respuesta == "OK")
            {
                CargarEmpleados("%");
                Limpiar();
                ActivarTextos(false);
                ActivarBotones(true);

                MessageBox.Show("Registro Eliminado Correctamente!", "Sistema Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(respuesta, "Sistema Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private bool validarTextos()
        {
            bool hayTextosVacios = false;

            if (string.IsNullOrEmpty(txtNombre.Text)) hayTextosVacios = true;
            if (string.IsNullOrEmpty(txtTelefono.Text)) hayTextosVacios = true;
            if (string.IsNullOrEmpty(txtSalario.Text)) hayTextosVacios = true;

            return hayTextosVacios;
        }

        private void ConfigureButtonAsLabel()
        {
            // Cambiar el estilo del botón para que se vea como un Label
            btnRecuperarPersona.FlatStyle = FlatStyle.Popup;    // Eliminar bordes y darle un estilo plano
            btnRecuperarPersona.BackColor = this.BackColor;    // Fondo igual al del formulario (transparente)
            btnRecuperarPersona.FlatAppearance.BorderSize = 0; // Eliminar el borde del botón
            btnRecuperarPersona.ForeColor = System.Drawing.Color.Blue;  // Color de texto similar al de un Label
            btnRecuperarPersona.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Regular); // Fuente como un label
            btnRecuperarPersona.Text = "Recuperar Persona"; // El texto del botón
        }
        #endregion



        public void frmEmpleados_Load(object sender, EventArgs e)
        {
            CargarEmpleados("%");
            dgvLista.ForeColor = Color.Black;
            CargarDepartamentos();
            CargarCargos();

            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarEmpleados(txtBuscar.Text);
        }

      

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bEstadoGuardar = true;

            ActivarTextos(true);
            ActivarBotones(false);

            txtNombre.Select();
            Limpiar();


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bEstadoGuardar=true;

            ActivarTextos(false);
            ActivarBotones(true);

            Limpiar();
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarEmpleado();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(iCodigoEmpleado == 0)
            {
                MessageBox.Show("Selecciona un registro", "Sistema de Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bEstadoGuardar=false;

                ActivarTextos(true);
                ActivarBotones(false);

                txtNombre.Select();
            }
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarTextos())
            {
                MessageBox.Show("Hay campos vacios, debes llenar todos los campos (*) obligatorios", "Sistema de Gestión de Empleados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (bEstadoGuardar)
                {
                    GuardarEmpleados();
                }
                else
                {
                    ActualizarEmpleados();
                }
                
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iCodigoEmpleado == 0)
            {
                MessageBox.Show("Selecciona un registro", "Sistema de Gestión de empleados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult resultado = MessageBox.Show("¿Estas seguro de eliminar este registro?", "Sistema de Gestión de empleados", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                {
                    DesactivarEmpleados(iCodigoEmpleado);
                }
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            frmReportesEmpleados formReporteEmpleados = new frmReportesEmpleados();

            formReporteEmpleados.txtFiltrar.Text = txtBuscar.Text;
            formReporteEmpleados.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRecuperarEmpleado formRecuperarEmpleado = new frmRecuperarEmpleado(this);

            formRecuperarEmpleado.Show();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnBuscar_Click(sender, e);
            
            }
        }
    }
}
