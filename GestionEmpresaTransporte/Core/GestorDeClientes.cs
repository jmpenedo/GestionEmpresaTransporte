using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GestionEmpresaTransporte.Core
{
    /// <summary>
    ///     Clase contenedora de clientes y permite su gestión
    /// </summary>
    public class GestorDeClientes : ICollection<Cliente>
    {
        /// <summary>
        ///     Crea un gestor de clientes con una lista vacía de clientes
        /// </summary>
        public GestorDeClientes()
        {
            Clientes = new List<Cliente>();
        }

        //Crea un gestor de clientes apartir de un fichero XML pasado por parametro
        public GestorDeClientes(string ficheroXml) : this()
        {
            CargarXML(ficheroXml);
        }

        public List<Cliente> Clientes { get; set; }

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

        /// <summary>
        ///     Guarda el listado en formato XML en disco con el nombre pasado
        ///     por parametro
        /// </summary>
        /// <param name="fn">
        ///     <see cref="string" />
        /// </param>
        public void GuardarXML(string fn)
        {
            try
            {
                ToXmlElement().Save(fn);
            }
            catch (SystemException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        ///     Carga clientes desde un XML del que se pasa el nombre como
        ///     parámertro
        /// </summary>
        /// <param name="fn">
        ///     <see cref="string" />
        /// </param>
        public void CargarXML(string fn)
        {
            try
            {
                var docXml = XElement.Load(fn);
                foreach (var clienteXml in docXml.Elements("cliente")) Clientes.Add(new Cliente(clienteXml));
            }
            catch (SystemException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}