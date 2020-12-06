using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEmpresaTransporte.Core
{
    using System.Xml.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Xml;

    class ColeccionTransportes : ICollection<Transporte>
    {
        public List<Transporte> ListaTransportes = new List<Transporte>();
        
        public const string ArchivoXml = "../../Samples/transportes.xml"; 
        public const string EtqTransportes = "transportes"; 
        public const string EtqID = "ID"; 
        public const string EtqTipo = "tipo"; 
        public const string EtqTransporte = "transporte";
        public const string EtqVehiculo = "matriculaVehiculo";
        public const string EtqCliente = "nifCliente";
        public const string EtqPrecio = "precioTotal";
        public const string EtqFechaContratacion = "fechaContratacion"; 
        public const string EtqKmRecorridos = "kmRecorridos"; 
        public const string EtqFechaSalida = "fechaSalida";
        public const string EtqFechaEntrega = "fechaEntrega"; 
        public const string EtqImporteDia = "importeDia";
        public const string EtqPrecioLitro = "precioLitro";
        public const string EtqIva = "iva";
        public const string EtqGas = "gasConsumido";

        public int Count
        {
            get { return ListaTransportes.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(Transporte transporte)
        {
            ListaTransportes.Add(transporte);
        }

        public void Clear()
        {
            ListaTransportes.Clear();
        }

        public bool Contains(Transporte transporte)
        {
            return ListaTransportes.Contains(transporte);
        }

        public bool Remove(Transporte transporte)
        {
            return ListaTransportes.Remove(transporte);
        }

        public void CopyTo(Transporte[] transporteList, int pos)
        {
            ListaTransportes.CopyTo(transporteList, pos);
        }

        IEnumerator<Transporte> IEnumerable<Transporte>.GetEnumerator()
        {
            foreach (var Transporte in ListaTransportes)
            {
                yield return Transporte;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var Transporte in ListaTransportes)
            {
                yield return Transporte;
            }
        }

        public Transporte this[int i]
        {
            get { return ListaTransportes[i]; }
            set { ListaTransportes[i] = value; }
        }

        public ReadOnlyCollection<Transporte> AsReadOnly()
        {
            return ListaTransportes.AsReadOnly();
        }

        public Boolean ExisteTransporte(String idTransporte)
        {
            Boolean toret = false;
            var count = this.ListaTransportes.Select(transp => transp).Where(transp => transp.IdTransporte.Equals(idTransporte)).Count();
            if (count != 0)
            {
                toret = true;
            }

            return toret;
        }

        public Transporte RecuperarTransporte(String idTransporte)
        {
            return this.ListaTransportes.Select(transp => transp).Where(transp => transp.IdTransporte.Equals(idTransporte)).First<Transporte>();
        }
        
        public void GuardaXml()
        {
            this.GuardaXml( ArchivoXml );
        }
        
        public void GuardaXml(string nf)
        {
            var doc = new XDocument();
            var root = new XElement(EtqTransportes);

            foreach (Transporte transporte in this.ListaTransportes)
            {
                root.Add(
                    new XElement(EtqTransporte,
                        new XElement(EtqID, transporte.IdTransporte),
                        new XElement(EtqTipo, transporte.TipoTransporte),
                        new XElement(EtqVehiculo, transporte.Camion),
                        new XElement(EtqCliente, transporte.Cliente),
                        new XElement(EtqFechaContratacion, transporte.FechaContratacion),
                        new XElement(EtqKmRecorridos, transporte.KmRecorridos),
                        new XElement(EtqFechaSalida, transporte.FechaSalida),
                        new XElement(EtqFechaEntrega, transporte.FechaEntrega),
                        new XElement(EtqImporteDia, transporte.ImportePorDia),
                        new XElement(EtqIva, transporte.IVA),
                        new XElement(EtqPrecioLitro, transporte.PrecioLitro),
                        new XElement(EtqGas, transporte.GasConsumido),
                        new XElement(EtqPrecio, transporte.PrecioTotal)));
            }
            doc.Add( root );
            doc.Save( nf );
        }
        
        public static ColeccionTransportes CargarXML(string f, GestorDeClientes c, ColeccionVehiculos v)
        {
            var toret = new ColeccionTransportes();
            
            try
            {
                var doc = XDocument.Load(f); 
                if (doc.Root != null
                    && doc.Root.Name == EtqTransportes)
                {
                    var transportes = doc.Root.Elements(EtqTransporte);
                    
                    foreach(XElement transporteXml in transportes)
                    {
                        Cliente cliente = c.getClientebyNif((string)transporteXml.Element(EtqCliente));
                        Vehiculo vehiculo = v.RecuperarVehiculo((string) transporteXml.Element(EtqVehiculo));
                        toret.ListaTransportes.Add( new Transporte(vehiculo,
                            cliente,
                            (string) transporteXml.Element(EtqFechaContratacion),
                            (int) transporteXml.Element(EtqKmRecorridos),
                            (string) transporteXml.Element(EtqFechaSalida),
                            (string) transporteXml.Element(EtqFechaEntrega),
                            (double) transporteXml.Element(EtqImporteDia),
                            (double) transporteXml.Element(EtqIva),
                            (double) transporteXml.Element(EtqPrecioLitro),
                            (double) transporteXml.Element(EtqGas),
                            (double) transporteXml.Element(EtqPrecio)));
                    }
                }
                
            }catch(XmlException)
            {
                
                toret.Clear();
            }
            catch(IOException)
            {
                toret.Clear();
            }

            return toret;
        }
        
        public static ColeccionTransportes CargarXML(GestorDeClientes c, ColeccionVehiculos v)
        {
            return CargarXML( ArchivoXml,c,v );
        }
    }
}
