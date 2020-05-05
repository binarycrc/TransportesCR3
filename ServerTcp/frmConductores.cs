using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TransportesCRLib;

namespace ServerTcp
{
    public partial class frmConductores : Form
    {
        DataLayer datalayer = new DataLayer();
        public frmConductores()
        {
            InitializeComponent();
        }

        private void btnConsultarConductor_Click(object sender, EventArgs e)
        {
            try
            {
                gvConductores.DataSource = datalayer.ConsultarConductores();
                gvConductores.Update();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnValidarConductor_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedIdentificacion;
                if (gvConductores.SelectedCells.Count > 0)
                {
                    int selectedrowindex = gvConductores.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = gvConductores.Rows[selectedrowindex];
                    selectedIdentificacion = Convert.ToString(selectedRow.Cells["Identificacion"].Value);

                    datalayer.ValidarConductor(selectedIdentificacion, "CONDUCTOR");
                    btnConsultarConductor_Click(sender, e);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnDenegar_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedIdentificacion;
                if (gvConductores.SelectedCells.Count > 0)
                {
                    int selectedrowindex = gvConductores.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = gvConductores.Rows[selectedrowindex];
                    selectedIdentificacion = Convert.ToString(selectedRow.Cells["Identificacion"].Value);

                    datalayer.ValidarConductor(selectedIdentificacion, "DENEGADO");
                    btnConsultarConductor_Click(sender, e);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
