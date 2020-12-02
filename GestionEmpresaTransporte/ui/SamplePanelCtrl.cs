using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class SamplePanelCtrl
    {
        /// <summary>
        ///
        /// </summary>
        public SamplePanelCtrl()
        {
            View = new SamplePanelView();
            //Asignamos Handlers
            View.grdLista.SelectionChanged += (sender, args) => ActualizarEdInferior();
       }
        public SamplePanelView View { get; }
        public GestorDeClientes GestorClientes { get; set; }
        /// <summary>
        ///     Actuliza la caja de texto de dirección con la información
        ///     del cliente seleccionado
        /// </summary>
        private void ActualizarEdInferior()
        {
            foreach (WForms.DataGridViewRow row in View.grdLista.SelectedRows)
                View.EdInferior.Text = row.Cells[4].Value.ToString();
        }
    }
}