/*********************************************************************
 * Copyright 2020 Pablo Ugalde
 * Universidad Estatal A Distancia
 * PRIMER CUATRI-2020 00830 PROGRAMACION AVANZADA
 * 
*********************************************************************/

using ServerTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TransportesCRLib;

namespace TransportesCR3
{
    public partial class DataInfo : Form
    {
        public DataInfo()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (
                    (string.IsNullOrEmpty(txtServerDataSource.Text))
                    || (string.IsNullOrWhiteSpace(txtServerDataSource.Text))
                    || (string.IsNullOrEmpty(txtServerInitialCatalog.Text))
                    || (string.IsNullOrWhiteSpace(txtServerInitialCatalog.Text))
                    )
                {
                    lblResultado.Text = "Valores en formato incorrecto o nulos";
                }
                else
                {
                    DataLayer datalayer = new DataLayer(txtServerDataSource.Text.Trim(), txtServerInitialCatalog.Text.Trim());
                    if (!datalayer.testData())
                    {
                        lblResultado.Text = datalayer._LatestError;
                    }
                    else
                    {
                        this.Hide();
                        frmServidor mainForm = new frmServidor();
                        mainForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = ex.Message;
                throw;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void txtServerDataSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter){btnEntrar_Click(this, new EventArgs());}
        }

        private void txtServerInitialCatalog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter){btnEntrar_Click(this, new EventArgs());}
        }
    }
}
