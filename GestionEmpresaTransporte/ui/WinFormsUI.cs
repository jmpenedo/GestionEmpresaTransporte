using System.Windows.Forms;

namespace GestionEmpresaTransporte.ui
{
    internal class WinFormsUI
    {
        public static void MainLoop(string[] args)
        {
            var f = new MainWindowCtrl();
            Application.Run(f.View);
        }
    }
}