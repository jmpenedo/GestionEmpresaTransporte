namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClienteVerPanelCtrl
    {
        /// <summary>
        /// </summary>
        /// <param name="gestorDeClientes"></param>
        public ClienteVerPanelCtrl()
        {
            View = new ClienteVerPanelView();
            View.ModoConsulta();
        }

        public ClienteVerPanelView View { get; }
    }
}