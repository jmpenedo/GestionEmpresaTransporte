using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using GestionEmpresaTransporte.Core.Vehiculos;
using System.Collections.ObjectModel;

namespace GestionEmpresaTransporte.Core
{
    public class ColeccionVehiculos : ICollection<Vehiculo>
    {
        protected List<Vehiculo> listaVehiculos = new List<Vehiculo>();
        
        public const string ArchivoXml = "../../Samples/vehiculos.xml";
        public const string EtqVehiculos = "vehiculos";
        public const string EtqVehiculo = "vehiculo";
        public const string EtqMatricula = "matricula"; 
        public const string EtqTipo = "tipo";
        public const string EtqMarca = "marca"; 
        public const string EtqModelo = "modelo"; 
        public const string EtqConsumo = "consumoKm"; 
        public const string EtqFechaAdquisicion= "fechaAdquisicion";
        public const string EtqFechaFabricacion = "fechaFabricacion"; 
        public const string EtqPeso = "pesoMaximo"; 
        public const string EtqComodidades = "comodidades";
        public const string EtqComodidad = "comodidad";
        
        public IEnumerator<Vehiculo> GetEnumerator()
        {
            return ((IEnumerable<Vehiculo>) this.listaVehiculos).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return listaVehiculos.GetEnumerator();
        }

        public void Add(Vehiculo item)
        {
            if (item != null & !Contains(item))
            {
                listaVehiculos.Add(item);
            }
        }

        public void Clear()
        {
            listaVehiculos.Clear();
        }

        public bool Contains(Vehiculo t)
        {
            var toret = false;
            foreach (var x in listaVehiculos.Where(x => t != null && x.Matricula.Equals(t.Matricula)))
            {
                toret = true;
            }

            return toret;
        }

        public void CopyTo(Vehiculo[] array, int arrayIndex)
        {
            listaVehiculos.CopyTo(array,arrayIndex);
        }

        public bool Remove(Vehiculo item)
        {
            return item != null && listaVehiculos.Remove(item);
        }
        
        public bool Remove(String matricula)
        {
            Vehiculo toremove = null;
            foreach (var item in listaVehiculos)
            {
                if (item.Matricula.Equals(matricula))
                    toremove = item;
            }

            return Remove(toremove);
        }

        public int Count
        {
            get { return listaVehiculos.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public override string ToString()
        {
            string toret = "";
            foreach (var v in listaVehiculos)
            {
                toret += v + "\n";
            }

            return toret;
        }

        public Vehiculo listaVehiculosIndex(int i)
        {
            return this.listaVehiculos[i];
        }
        

        public Vehiculo RecuperarVehiculo(String matricula)
        {
            return this.listaVehiculos.Select(vehiculo => vehiculo).Where(vehiculo => vehiculo.Matricula.Equals(matricula)).First<Vehiculo>();
        }

        public List<string> ListaMatriculas()
        {
                var toret = new List<string>();
                foreach (Vehiculo Vehiculo in this.listaVehiculos)
                {
                    toret.Add(Vehiculo.Matricula);
                }
                return toret;
        }

        public void GuardaXml()
        {
            this.GuardaXml( ArchivoXml );
        }

        public void GuardaXml(string nf)
        {
            var doc = new XDocument();
            var root = new XElement(EtqVehiculos);

            foreach (Vehiculo vehiculo in this.listaVehiculos)
            {
                root.Add(
                    new XElement(EtqVehiculo,
                        new XElement(EtqMatricula, vehiculo.Matricula),
                        new XElement(EtqTipo, vehiculo.Tipo),
                        new XElement(EtqMarca, vehiculo.Marca),
                        new XElement(EtqModelo, vehiculo.Modelo),
                        new XElement(EtqConsumo, vehiculo.Consumo),
                        new XElement(EtqFechaAdquisicion, vehiculo.FechaAdquisicion),
                        new XElement(EtqFechaFabricacion, vehiculo.FechaFabricacion),
                        new XElement(EtqPeso, vehiculo.Peso),
                        new XElement(EtqComodidades, vehiculo.Comodidades.Select(i => new XElement(EtqComodidad, i)))));
            }

            doc.Add(root);
            doc.Save(nf);
        }
        
        public static ColeccionVehiculos CargarXml(string f)
        {
            var toret = new ColeccionVehiculos();
            
            try
            {
                var doc = XDocument.Load(f);
                
                if (doc.Root != null
                    && doc.Root.Name == EtqVehiculos)
                {
                    var vehiculos = doc.Root.Elements(EtqVehiculo);
                    
                    foreach(XElement vehiculoXml in vehiculos)
                    {
                        var comodidades = doc.Root.Elements(EtqComodidad);
                        List<String> c =new List<string>();
                        foreach (XElement comodidad in comodidades)
                        {
                            c.Add((string)comodidad);
                        }
                        
                        if (vehiculoXml.Element(EtqTipo).Value=="Camion") 
                        {
                            toret.listaVehiculos.Add(new Camion((string)vehiculoXml.Element(EtqMatricula),
                                (DateTime) vehiculoXml.Element(EtqFechaFabricacion),
                                (DateTime) vehiculoXml.Element(EtqFechaAdquisicion),
                                (double) vehiculoXml.Element(EtqConsumo),
                                (string) vehiculoXml.Element(EtqMarca),
                                (string) vehiculoXml.Element(EtqModelo),
                                c));
                        }
                        else if (vehiculoXml.Element(EtqTipo).Value=="CamionArticulado") 
                        {
                            toret.listaVehiculos.Add(new Camion((string)vehiculoXml.Element(EtqMatricula),
                                (DateTime) vehiculoXml.Element(EtqFechaFabricacion),
                                (DateTime) vehiculoXml.Element(EtqFechaAdquisicion),
                                (double) vehiculoXml.Element(EtqConsumo),
                                (string) vehiculoXml.Element(EtqMarca),
                                (string) vehiculoXml.Element(EtqModelo),
                                c));
                        }
                        else if (vehiculoXml.Element(EtqTipo).Value=="Furgoneta") 
                        {
                            toret.listaVehiculos.Add(new Camion((string)vehiculoXml.Element(EtqMatricula),
                                (DateTime) vehiculoXml.Element(EtqFechaFabricacion),
                                (DateTime) vehiculoXml.Element(EtqFechaAdquisicion),
                                (double) vehiculoXml.Element(EtqConsumo),
                                (string) vehiculoXml.Element(EtqMarca),
                                (string) vehiculoXml.Element(EtqModelo),
                                c));
                        }
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

        public static ColeccionVehiculos CargarXml()
        {
            return CargarXml( ArchivoXml );
        }
    }
}