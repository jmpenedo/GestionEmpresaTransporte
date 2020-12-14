using System;
using System.Diagnostics;

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
        public WForms.MenuItem opGestionTransportes;
        public WForms.MenuItem opGestionVehiculos;
        public WForms.MenuItem opGestionGraficos;
        public WForms.MenuItem opGuardar;
        public WForms.MenuItem opSalir;
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
            MinimumSize = new Draw.Size(800, 700);
            MaximumSize = MinimumSize;
            Text = "Gestion de trasportes";
            BuildIcono();
        }

        private void BuildIcono()
        {
            try
            {
                Icon = new Draw.Icon("../../Res/camion.ico");
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al cargar el icono: " + e);
            }
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
            opGestionVehiculos = new WForms.MenuItem("&Gestión de vehículos")
            {
                Shortcut = WForms.Shortcut.CtrlY
            };
            opGestionTransportes = new WForms.MenuItem("&Gestión de transportes")
            {
                Shortcut = WForms.Shortcut.CtrlT
            };
            opGestionGraficos = new WForms.MenuItem("&Gestión de gráficos")
            {
                Shortcut = WForms.Shortcut.CtrlShift0
            };
            mArchivo.MenuItems.Add(opCargar);
            mArchivo.MenuItems.Add(opGuardar);
            mArchivo.MenuItems.Add(opSalir);
            mEditar.MenuItems.Add(opGestionClientes);
            mEditar.MenuItems.Add(opGestionVehiculos);
            mEditar.MenuItems.Add(opGestionTransportes);
            mEditar.MenuItems.Add(opGestionGraficos);
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