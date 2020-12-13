using System;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class TransporteListarPanelView : WForms.Panel
    {
        public WForms.DataGridView grdLista;
        public TransporteVerPanelView pnlTransporte;

        public TransporteListarPanelView(TransporteVerPanelView transporteVerPanelView)
        {
            pnlTransporte = transporteVerPanelView;
            Build();
        }

        public WForms.TextBox EdInferior { get; private set; }

        private void Build()
        {
            var pnlTable = new WForms.TableLayoutPanel();

            pnlTable.SuspendLayout();
            pnlTable.Dock = WForms.DockStyle.Fill;
            pnlTable.Controls.Add(BuildPanelLista());
            pnlTable.Controls.Add(BuildVerTransporte());
            pnlTable.ResumeLayout(false);
            Controls.Add(pnlTable);
            MinimumSize = new Draw.Size(785, 600);
            MaximumSize = MinimumSize;
            grdLista.Height = (int) (Height * 0.40);
        }

        private WForms.Control BuildPanelLista()
        {
            // Crear gridview
            grdLista = new WForms.DataGridView
            {
                Dock = WForms.DockStyle.Top,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
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

        private WForms.Control BuildVerTransporte()
        {
            pnlTransporte.Dock = WForms.DockStyle.Top;
            return pnlTransporte;
        }

        public void AjustarColGrid()
        {
            if (grdLista.Columns.Count > 0)
            {
                grdLista.Columns[0].Width = (int) Math.Floor(grdLista.Width * .17);
                grdLista.Columns[1].Visible = false;
                grdLista.Columns[2].Width = (int) Math.Floor(grdLista.Width * .16);
                grdLista.Columns[3].Width = (int) Math.Floor(grdLista.Width * .17);
                grdLista.Columns[4].Width = (int) Math.Floor(grdLista.Width * .16);
                grdLista.Columns[5].Width = (int) Math.Floor(grdLista.Width * .17);
                grdLista.Columns[6].Visible = false;
                grdLista.Columns[7].Visible = false;
                grdLista.Columns[8].Visible = false;
                grdLista.Columns[9].Visible = false;
                grdLista.Columns[10].Visible = false;
                grdLista.Columns[11].Visible = false;
                grdLista.Columns[12].Width = (int) Math.Floor(grdLista.Width * .17);

                grdLista.Columns[0].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleCenter;
                grdLista.Columns[2].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleCenter;
                grdLista.Columns[3].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleCenter;
                grdLista.Columns[4].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleCenter;
                grdLista.Columns[5].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleCenter;
                grdLista.Columns[12].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}