using System;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class SamplePanelView : WForms.Panel
    {
        public WForms.DataGridView grdLista;
        public WForms.Panel pnlPpal;
        public WForms.Panel pnlDetalle;
        public WForms.Panel pnlLista;
        public SamplePanelView()
        {
            Build();
        }
        public WForms.TextBox EdInferior { get; private set; }
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
            MinimumSize = new Draw.Size(800, 600);
            MaximumSize = MinimumSize;
            Text = "Gestión de ejemplo";
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
            EdInferior = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                Font = new Draw.Font(Draw.FontFamily.GenericMonospace, 10),
                ForeColor = Draw.Color.Navy,
                BackColor = Draw.Color.LightGray
            };
            pnlDetalle.Controls.Add(EdInferior);
            pnlDetalle.ResumeLayout(false);
            return pnlDetalle;
        }
        


    }
}