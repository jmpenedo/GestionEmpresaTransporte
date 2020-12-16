using System.Diagnostics;
using GestionEmpresaTransporte.ui;

namespace GestionEmpresaTransporte
{
    internal class Program
    {
        [Conditional("DEBUG")]
        private static void CreateConsoleTracing()
        {
            Trace.Listeners.Add(new ConsoleTraceListener(true));
        }


        private static void Main(string[] args)
        {
            // Creando un listener para la consola
            CreateConsoleTracing();
            WinFormsUI.MainLoop(args);
        }
    }
}