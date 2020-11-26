namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClientePanelView : WForms.Panel
    {
        public WForms.DataGridView grdLista;
        public WForms.Panel pnlPpal;
        public WForms.Panel pnlDetalle;
        public WForms.Panel pnlLista;
        public ClientePanelView()
        {
            Build();
        }
        public WForms.TextBox EdDireccion { get; private set; }
        private void Build()
        {
            SuspendLayout();
            pnlPpal = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            pnlPpal.SuspendLayout();
            Controls.Add(pnlPpal);
            pnlPpal.Controls.Add(BuildPanelLista());
            pnlPpal.Controls.Add(BuildTexto());
            pnlPpal.ResumeLayout(false);
            
            Text = "Gestión de Clientes";
            ResumeLayout(true);
        }
        private WForms.Panel BuildPanelLista()
        {
            pnlLista = new WForms.Panel();
            pnlLista.SuspendLayout();
            pnlLista.Dock = WForms.DockStyle.Fill;

            // Crear gridview
            grdLista = new WForms.DataGridView
            {
                Dock = WForms.DockStyle.Fill,
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
            pnlLista.Controls.Add(grdLista);
            pnlLista.ResumeLayout(false);
            return pnlLista;
        }
        private WForms.Panel BuildTexto()
        {
            pnlDetalle = new WForms.Panel {Dock = WForms.DockStyle.Bottom};
            pnlDetalle.SuspendLayout();
            EdDireccion = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                Font = new Draw.Font(Draw.FontFamily.GenericMonospace, 10),
                ForeColor = Draw.Color.Navy,
                BackColor = Draw.Color.LightGray
            };
            pnlDetalle.Controls.Add(EdDireccion);
            pnlDetalle.ResumeLayout(false);
            return pnlDetalle;
        }
        


    }
}