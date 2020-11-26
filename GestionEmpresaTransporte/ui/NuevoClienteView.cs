namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class NuevoClienteView : WForms.Panel
    {
        public NuevoClienteView()
        {
            Build();
        }

        /*Datos del cliente*/
        public WForms.TextBox EdNif { get; private set; }
        public WForms.TextBox EdNombre { get; private set; }
        public WForms.TextBox EdTelefono { get; private set; }
        public WForms.TextBox EdCorreo { get; private set; }
        public WForms.TextBox EdDireccion { get; private set; }
        public WForms.Button BtAceptar { get; private set; }
        public WForms.Button BtCancelar { get; private set; }

        private void Build()
        {
            var pnlCliente = new WForms.TableLayoutPanel
            {
                Dock = WForms.DockStyle.Fill
            };
            pnlCliente.SuspendLayout();
            Controls.Add(pnlCliente);
            pnlCliente.Controls.Add(BuildNif());
            pnlCliente.Controls.Add(BuildNombre());
            pnlCliente.Controls.Add(BuildCorreo());
            pnlCliente.Controls.Add(BuildTelefono());
            pnlCliente.Controls.Add(BuildDireccion());
            var botones = BuildPanelBotones();
            pnlCliente.Controls.Add(botones);
            pnlCliente.ResumeLayout(true);
            Text = "Alta de cliente";
            MaximumSize = new Draw.Size(400, pnlCliente.Height + botones.Height);
            MinimumSize = MaximumSize;
            /*MinimizeBox = false;
            MaximizeBox = false;
            StartPosition = WForms.FormStartPosition.CenterParent;*/
        }

        private WForms.Panel BuildNif()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Nif"
            });
            EdNif = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.90),
                TextAlign = WForms.HorizontalAlignment.Right
            };
            toret.Controls.Add(EdNif);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdNif.Height);
            return toret;
        }

        private WForms.Panel BuildNombre()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Nombre"
            });
            EdNombre = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.90),
                TextAlign = WForms.HorizontalAlignment.Right
            };

            toret.Controls.Add(EdNombre);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdNombre.Height);
            return toret;
        }

        private WForms.Panel BuildTelefono()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Telefono"
            });

            EdTelefono = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.90),
                TextAlign = WForms.HorizontalAlignment.Right
            };

            toret.Controls.Add(EdTelefono);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdTelefono.Height);
            return toret;
        }

        private WForms.Panel BuildCorreo()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Correo"
            });

            EdCorreo = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.90),
                TextAlign = WForms.HorizontalAlignment.Right
            };

            toret.Controls.Add(EdCorreo);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdCorreo.Height);
            return toret;
        }

        private WForms.Panel BuildDireccion()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Direccion"
            });

            EdDireccion = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.90),
                TextAlign = WForms.HorizontalAlignment.Left,
                Multiline = true,
                ScrollBars = WForms.ScrollBars.Vertical
            };

            toret.Controls.Add(EdDireccion);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdDireccion.Height);

            return toret;
        }

        public WForms.Panel BuildPanelBotones()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            BtAceptar = new WForms.Button
            {
                Dock = WForms.DockStyle.Left,
                DialogResult = WForms.DialogResult.OK,
                Text = "&Aceptar"
            };
            toret.Controls.Add(BtAceptar);

            BtCancelar = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                DialogResult = WForms.DialogResult.Cancel,
                Text = "&Cancelar"
            };
            toret.Controls.Add(BtCancelar);
            toret.Dock = WForms.DockStyle.Top;
            toret.MaximumSize = new Draw.Size(int.MaxValue, 30);
            return toret;
        }

        public void ModoConsulta()
        {
            EdNif.Enabled = false;
            EdNombre.Enabled = false;
            EdTelefono.Enabled = false;
            EdCorreo.Enabled = false;
            EdDireccion.Enabled = false;



        }
    }
}