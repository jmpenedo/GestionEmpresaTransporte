namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class MainWindowView : WForms.Form
    {
        public WForms.MenuItem mArchivo;
        public WForms.MenuItem mEditar;
        public WForms.MainMenu mPpal;
        public WForms.MenuItem opCargar;
        public WForms.MenuItem opGestionClientes;
        public WForms.MenuItem opGuardar;
        public WForms.MenuItem opPruebaClientes;
        public WForms.MenuItem opSalir;
        public WForms.Panel pnlCliente;
        public WForms.StatusBar sbStatus;

        public MainWindowView()
        {
            Build();
        }

        private void Build()
        {
            BuildMenuOpciones();
            BuildStatusBar();

            SuspendLayout();
            pnlCliente = new ClientePanelView()
            {
                Dock = WForms.DockStyle.Fill
            };
            pnlCliente.SuspendLayout();
            Controls.Add(pnlCliente);
            pnlCliente.ResumeLayout(false);
            MinimumSize = new Draw.Size(600, 400);
            Text = "Gestion de trasportes";
            pnlCliente.Visible = false;
            ResumeLayout(true);
        }

        private void BuildMenuOpciones()
        {
            mPpal = new WForms.MainMenu();
            mArchivo = new WForms.MenuItem("&Archivo");

            mEditar = new WForms.MenuItem("&Editar");
            opSalir = new WForms.MenuItem("&Salir") {Shortcut = WForms.Shortcut.CtrlQ};
            opGuardar = new WForms.MenuItem("&Guardar");
            opCargar = new WForms.MenuItem("&Cargar");
            opGestionClientes = new WForms.MenuItem("&Gestión de clientes")
            {
                Shortcut = WForms.Shortcut.CtrlC
            };
            opPruebaClientes = new WForms.MenuItem("&Prueba clientes")
            {
                Shortcut = WForms.Shortcut.CtrlC
            };
            mArchivo.MenuItems.Add(opCargar);
            mArchivo.MenuItems.Add(opGuardar);
            mArchivo.MenuItems.Add(opSalir);
            mEditar.MenuItems.Add(opGestionClientes);
            mEditar.MenuItems.Add(opPruebaClientes);
            mPpal.MenuItems.Add(mArchivo);
            mPpal.MenuItems.Add(mEditar);
            Menu = mPpal;
        }

        private void BuildStatusBar()
        {
            sbStatus = new WForms.StatusBar {Dock = WForms.DockStyle.Bottom};
            Controls.Add(sbStatus);
        }
    }
}