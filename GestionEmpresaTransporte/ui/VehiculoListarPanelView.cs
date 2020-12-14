namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;
    public class VehiculoListarPanelView : WForms.Panel
    {
        public WForms.DataGridView grdLista;
        public VehiculoVerPanelView pnlVehiculo;
        
        public VehiculoListarPanelView(VehiculoVerPanelView vehiculoVerPanelView)
        {
            pnlVehiculo = vehiculoVerPanelView;
            Build();
        }
        
        private void Build()
        {
            var pnlTable = new WForms.TableLayoutPanel();

            pnlTable.SuspendLayout();
            pnlTable.Dock = WForms.DockStyle.Fill;
            pnlTable.Controls.Add(BuildPanelLista());
            pnlTable.Controls.Add(BuildVerVehiculo());
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
        
        private WForms.Control BuildVerVehiculo()
        {
            pnlVehiculo.Dock = WForms.DockStyle.Top;
            return pnlVehiculo;
        }
        
        public void AjustarColGrid()
        {
            if (grdLista.Columns.Count > 0)
            {
                grdLista.Columns[0].Width = (int)System.Math.Floor(grdLista.Width * .20);
                grdLista.Columns[1].Width = (int)System.Math.Floor(grdLista.Width * .20);
                grdLista.Columns[2].Width = (int)System.Math.Floor(grdLista.Width * .20);
                grdLista.Columns[3].Width = (int)System.Math.Floor(grdLista.Width * .10);
                grdLista.Columns[5].Width = (int)System.Math.Floor(grdLista.Width * .20);
                grdLista.Columns[6].Width = (int)System.Math.Floor(grdLista.Width * .10);

                grdLista.Columns[4].Visible = false;
                grdLista.Columns[7].Visible = false;
                
                
                grdLista.Columns[0].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleRight;
                grdLista.Columns[1].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleRight;
                grdLista.Columns[2].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleRight;
                grdLista.Columns[3].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleCenter;
                grdLista.Columns[5].DefaultCellStyle.Alignment = WForms.DataGridViewContentAlignment.MiddleRight;
                
                
            }
        }
        
        public void Actualizar()
        {
            grdLista.Update();
            grdLista.Refresh();
        }
    }
}