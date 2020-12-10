namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClienteListarPanelView : WForms.Panel
    {
        public WForms.DataGridView grdLista;
        public ClienteVerPanelView pnlCliente;

        public ClienteListarPanelView(ClienteVerPanelView clienteVerPanelView)
        {
            pnlCliente = clienteVerPanelView;
            Build();
        }

        public WForms.TextBox EdInferior { get; private set; }

        private void Build()
        {
            var pnlTable = new WForms.TableLayoutPanel();

            pnlTable.SuspendLayout();
            pnlTable.Dock = WForms.DockStyle.Fill;
            pnlTable.Controls.Add(BuildPanelLista());
            pnlTable.Controls.Add(BuildVerCliente());
            pnlTable.ResumeLayout(false);
            Controls.Add(pnlTable);
            MinimumSize = new Draw.Size(785, 600);
            MaximumSize = MinimumSize;
            grdLista.Height = (int) (Height * 0.60);
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
            pnlCliente.Dock = WForms.DockStyle.Top;
            return pnlCliente;
        }

        public void AjustarColGrid()
        {
            if (grdLista.Columns.Count > 0)
            {
                grdLista.Columns[0].AutoSizeMode = WForms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                grdLista.Columns[1].AutoSizeMode = WForms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                grdLista.Columns[2].AutoSizeMode = WForms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                grdLista.Columns[4].Visible = false; //Ocultamos la columna 4 que contiene la direccion
            }
        }

        public void Actualizar()
        {
            grdLista.Update();
            grdLista.Refresh();
        }
    }
}