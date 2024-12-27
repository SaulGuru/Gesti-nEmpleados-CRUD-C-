using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjGestionEmpleados.Presentacion.Reportes
{
    public partial class frmReportesEmpleados : Form
    {
        public frmReportesEmpleados()
        {
            InitializeComponent();
        }

        private void frmReportesEmpleados_Load(object sender, EventArgs e)
        {

            this.sP_LISTAR_EMPLEADOSTableAdapter.Fill(this.dataSet.SP_LISTAR_EMPLEADOS, cBusqueda: txtFiltrar.Text);
            this.reportViewer1.RefreshReport();

        }
    }
}
