namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;
    public class VehiculoVerPanelCtrl
    {
        public VehiculoVerPanelCtrl()
        {
            View = new VehiculoVerPanelView();
            View.ModoConsulta();
        }

        public VehiculoVerPanelView View { get; }
    }
}