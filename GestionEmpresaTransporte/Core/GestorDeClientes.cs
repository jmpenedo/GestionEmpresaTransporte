using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace GestionEmpresaTransporte.Core
{
    /// <summary>
    ///     Clase contenedora de clientes y permite su gestión
    /// </summary>
    public class GestorDeClientes : ICollection<Cliente>
    {
        //Asigna una variable a la ruta del fichero XML y a las etiquetas

        public const string ArchivoXml = "../../Samples/clientes.xml";
        public const string EtqClientes = "clientes";
        public const string EtqCliente = "cliente";
        public const string EtqNIF = "nif";
        public const string EtqNombre = "nombre";
        public const string EtqTelefono = "telefono";
        public const string EtqEmail = "email";
        public const string EtqDireccionPostal = "DireccionPostal";

        public List<Cliente> Clientes = new List<Cliente>();

        public IEnumerator<Cliente> GetEnumerator()
        {
            return Clientes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Clientes).GetEnumerator();
        }


        /// <summary>
        ///     Si no existe un cliente con el mismo NIF se añade al sistema
        ///     si ya existe un cliente con el mismo NIF se actualiza
        /// </summary>
        /// <param name="unCliente"></param>
        public void Add(Cliente unCliente)
        {
            if (unCliente != null)
            {
                if (Clientes.Contains(unCliente))
                    Clientes.Remove(unCliente);
                Clientes.Add(unCliente);
            }
        }

        /// <summary>
        ///     Borra todos los clientes
        /// </summary>
        public void Clear()
        {
            Clientes.Clear();
        }

        /// <summary>
        ///     Revisa si el cliente está en la lista de clientes
        /// </summary>
        /// <param name="unCliente">
        ///     <see cref="Cliente" />
        /// </param>
        /// <returns></returns>
        public bool Contains(Cliente unCliente)
        {
            return Clientes.Contains(unCliente);
        }

        public void CopyTo(Cliente[] array, int arrayIndex)
        {
            Clientes.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Elimina el cliente pasado como parámetro
        /// </summary>
        /// <param name="unCliente"></param>
        /// <returns></returns>
        public bool Remove(Cliente unCliente)
        {
            return Clientes.Remove(unCliente);
        }

        public int Count => Clientes.Count;

        public bool IsReadOnly => ((ICollection<Cliente>) Clientes).IsReadOnly;

        public List<string> ListaNifs()
        {
            var toret = new List<string>();
            foreach (var Cliente in Clientes) toret.Add(Cliente.Nif);
            return toret;
        }

        /// <summary>
        ///     Busca en el listado de clientes el NIF pasado como
        ///     parámetro
        /// </summary>
        /// <param name="nif"></param>
        /// <returns>Devuelve un objeto <see cref="Cliente" /> si lo encuentra o null si no lo encuentra.</returns>
        public Cliente getClientebyNif(string nif)
        {
            Cliente toret = null;
            if (!string.IsNullOrEmpty(nif))
            {
                var allClients =
                    Clientes.Where(m => m.Nif.Equals(nif)).ToList();
                toret = allClients.FirstOrDefault();
            }

            return toret;
        }

        /// <summary>
        ///     Devuelve un string con el listado de los clientes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var toret = new StringBuilder();
            toret.AppendLine("Listado de Clientes");
            foreach (var cliente in Clientes) toret.AppendLine(cliente.ToString());
            return toret.ToString();
        }


        public int PosCliente(Cliente aBuscar)
        {
            return Clientes.FindIndex(cliente => cliente.Nif == aBuscar.Nif);
        }


        /// <summary>
        ///     Devuelve el listado de clientes como  XElement
        /// </summary>
        /// <returns>devuelve <see cref="XElement" /></returns>
        public XElement ToXmlElement()
        {
            var raiz = new XElement("clientes");
            foreach (var cliente in Clientes) raiz.Add(cliente.ToXmlElement());
            return raiz;
        }


        public void GuardaXml()
        {
            GuardaXml(ArchivoXml);
        }

        /// <summary>
        ///     Guarda el listado en formato XML en disco con el nombre pasado
        ///     por parametro
        /// </summary>
        /// <param name="fn">
        ///     <see cref="string" />
        /// </param>
        public void GuardaXml(string nf)
        {
            var doc = new XDocument();
            var root = new XElement(EtqClientes);

            foreach (var cliente in Clientes)
                root.Add(
                    new XElement(EtqCliente,
                        new XElement(EtqNIF, cliente.Nif),
                        new XElement(EtqNombre, cliente.Nombre),
                        new XElement(EtqTelefono, cliente.Telefono),
                        new XElement(EtqEmail, cliente.Email),
                        new XElement(EtqDireccionPostal, cliente.Dirección)));
            doc.Add(root);
            doc.Save(nf);
        }

        public static GestorDeClientes CargarXML(string f)
        {
            var toret = new GestorDeClientes();

            try
            {
                var doc = XDocument.Load(f);
                if (doc.Root != null
                    && doc.Root.Name == EtqClientes)
                {
                    var clientes = doc.Root.Elements(EtqCliente);

                    foreach (var clienteXml in clientes)
                        toret.Clientes.Add(new Cliente((string) clienteXml.Element(EtqNIF),
                            (string) clienteXml.Element(EtqNombre),
                            (string) clienteXml.Element(EtqTelefono),
                            (string) clienteXml.Element(EtqEmail),
                            (string) clienteXml.Element(EtqDireccionPostal)));
                }
            }
            catch (XmlException)
            {
                toret.Clear();
            }
            catch (IOException)
            {
                toret.Clear();
            }

            return toret;
        }

        public static GestorDeClientes CargarXML()
        {
            return CargarXML(ArchivoXml);
        }
    }
}