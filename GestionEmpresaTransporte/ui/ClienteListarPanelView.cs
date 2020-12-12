using System;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClienteListarPanelView : WForms.Panel
    {
        public WForms.DataGridView grdLista;
        public ClienteVerPanelView pnlCliente;

        public ClienteListarPanelView()
        {
            Build();
        }

        private void Build()
        {
            var pnlTable = new WForms.TableLayoutPanel();
            pnlTable.SuspendLayout();
            pnlTable.Dock = WForms.DockStyle.Fill;
            pnlTable.Controls.Add(BuildPanelLista());
            pnlTable.Controls.Add(BuildVerCliente());
            pnlTable.ResumeLayout(false);
            Controls.Add(pnlTable);
            MinimumSize = new Draw.Size(780, 600);
            MaximumSize = MinimumSize;
            grdLista.Height = (int) (Height * 0.69);
        }

        private WForms.Control BuildPanelLista()
        {
            // Crear gridview
            grdLista = new WForms.DataGridView
            {
                Dock = WForms.DockStyle.Top,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                ReadOnly = true,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                EnableHeadersVisualStyles = false,
                SelectionMode = WForms.DataGridViewSelectionMode.FullRowSelect
            };
            grdLista.ColumnHeadersDefaultCellStyle.ForeColor = Draw.Color.Black;
            grdLista.ColumnHeadersDefaultCellStyle.BackColor = Draw.Color.LightGray;
            grdLista.AutoSizeColumnsMode = WForms.DataGridViewAutoSizeColumnsMode.Fill;
            return grdLista;
        }

        private WForms.Control BuildVerCliente()
        {
            pnlCliente = new ClienteVerPanelView();
            pnlCliente.Dock = WForms.DockStyle.Top;
            return pnlCliente;
        }

        public void AjustarColGrid()
        {
            // Tomar las nuevas medidas
            var width = ClientRectangle.Width;

            // Redimensionar la tabla
            grdLista.Width = width;
            grdLista.Columns[0].Width =
                (int) Math.Floor(width * .10); // NIF
            grdLista.Columns[1].Width =
                (int) Math.Floor(width * .40); // Nombre
            grdLista.Columns[2].Width =
                (int) Math.Floor(width * .10); // TLF
            grdLista.Columns[4].Visible = false; //Direccion
        }

        public void Actualizar()
        {
            grdLista.Update();
            grdLista.Refresh();
            if (grdLista.Rows.Count > 0 && grdLista.SelectedRows.Count == 0)
                grdLista.Rows[grdLista.Rows.Count - 1].Selected = true;
        }
    }
}