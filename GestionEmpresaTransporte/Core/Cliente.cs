using System;
using System.Xml.Linq;

namespace GestionEmpresaTransporte.Core
{
    public class Cliente
    {
        /// <summary>
        ///     Crea un objeto de tipo cliente <see cref="Cliente" />
        /// </summary>
        /// <param name="nif">El identificador de la persona o empresa, puede ser un CIF,NIF, NIE</param>
        /// <param name="nombre">El nombre y apellidos de la persona o nombre de la empresa</param>
        /// <param name="telefono">Teléfono de contacto</param>
        /// <param name="email">Correo de contacto</param>
        /// <param name="dirección">Direccion completa</param>
        public Cliente(string nif, string nombre, string telefono, string email, string dirección)
        {
            if (utilidades.valida_NIFCIFNIE(nif))
            {
                Nif = nif;
            }
            else
            {
                var clientEx = new InvalidClientException("Valor de NIF no valido");
                throw clientEx;
            }
            ///TODO validar el resto de atributos

            Nombre = nombre;
            Telefono = telefono;
            Email = email;
            Dirección = dirección;
        }

        /// <summary>
        ///     Crea un cliente a partir de un XElement
        /// </summary>
        /// <param name="clienteXml"></param>
        public Cliente(XElement clienteXml) : this((string) clienteXml.Element("nif"),
            (string) clienteXml.Element("nombre"),
            (string) clienteXml.Element("telefono"),
            (string) clienteXml.Element("email"),
            (string) clienteXml.Element("direccion"))
        {
        }

        public string Nif { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Dirección { get; set; }

        /// <summary>
        ///     Devuelve el nif del cliente
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Nif;
        }

        /// <summary>
        ///     Devuelve los datos del cliente como un String
        /// </summary>
        /// <returns></returns>
        public string ImprimirDatos()
        {
            return string.Format("{0}, Nombre:{1}, Telefono:{2}, Email:{3}, Dirección:{4} ",
                Nif, Nombre, Telefono, Email, Dirección);
        }

        /// <summary>
        ///     Si dos clientes tienen el mismo NIF se consideran el mismo cliente
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var otroCliente = obj as Cliente;
            if (otroCliente == null) return false;

            return Nif.Equals(otroCliente.Nif);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        ///     Devuelve un XElement que contiene los datos del cliente
        /// </summary>
        /// <returns>devuelve <see cref="XElement" /></returns>
        public XElement ToXmlElement()
        {
            var raizToret = new XElement("cliente");
            raizToret.Add(new XElement("nif", Nif));
            raizToret.Add(new XElement("nombre", Nombre));
            raizToret.Add(new XElement("telefono", Telefono));
            raizToret.Add(new XElement("email", Email));
            raizToret.Add(new XElement("direccion", Dirección));
            return raizToret;
        }
    }

    public class InvalidClientException : Exception
    {
        public InvalidClientException()
        {
        }

        public InvalidClientException(string message) : base(message)
        {
        }

        public InvalidClientException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}