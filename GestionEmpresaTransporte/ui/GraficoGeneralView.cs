using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using GestionEmpresaTransporte.Core;
namespace GestionEmpresaTransporte.ui
{
    public class GraficoGeneralView : Form
    {
        public const int ChartCanvasSize = 512;

        /// <summary>
        /// Initializes a new grafico de actividad general de la empresa de Transportes>.
        /// </summary>
        /// 

        public GraficoGeneralView()
        {
            this.Build();
        }

        void Build()
        {
            this.Chart = new Chart(width: ChartCanvasSize,
                                    height: ChartCanvasSize)
            {
                Dock = DockStyle.Fill,
            };

            this.Controls.Add(this.Chart);
            this.MinimumSize = new Size(ChartCanvasSize, ChartCanvasSize);
            this.Text = this.GetType().Name;
        }

        /// <summary>
        /// Gets the <see cref="Chart"/>.
        /// </summary>
        /// <value>The chart.</value>
        public Chart Chart
        {
            get; private set;
        }
    }
}
