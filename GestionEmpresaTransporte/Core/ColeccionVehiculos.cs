using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GestionEmpresaTransporte.Core
{
    public class ColeccionVehiculos : ICollection<Vehiculo>
    {

        public ColeccionVehiculos()
        {
            Flota = new List<Vehiculo>();
        }

        public ColeccionVehiculos(IEnumerable<Vehiculo> vehiculos)
            :this()
        {
            this.Flota.AddRange( vehiculos );
        }

        public IEnumerator<Vehiculo> GetEnumerator()
        {
            return ((IEnumerable<Vehiculo>) this.Flota).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Flota.GetEnumerator();
        }

        public void Add(Vehiculo item)
        {
            if (item != null & !Contains(item))
            {
                Flota.Add(item);
            }
        }

        public void Clear()
        {
            Flota.Clear();
        }

        public bool Contains(Vehiculo t)
        {
            var toret = false;
            foreach (var x in Flota.Where(x => t != null && x.Matricula.Equals(t.Matricula)))
            {
                toret = true;
            }

            return toret;
        }

        public void CopyTo(Vehiculo[] array, int arrayIndex)
        {
            Flota.CopyTo(array,arrayIndex);
        }

        public bool Remove(Vehiculo item)
        {
            return item != null && Flota.Remove(item);
        }
        
        public bool Remove(String matricula)
        {
            Vehiculo toremove = null;
            foreach (var item in Flota)
            {
                if (item.Matricula.Equals(matricula))
                    toremove = item;
            }

            return Remove(toremove);
        }

        public int Count
        {
            get { return Flota.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public override string ToString()
        {
            string toret = "";
            foreach (var v in Flota)
            {
                toret += v + "\n";
            }

            return toret;
        }

        public Vehiculo flotaIndex(int i)
        {
            return this.Flota[i];
        }

        public List<Vehiculo> Flota { get; set; }

        public Vehiculo RecuperarVehiculo(String matricula)
        {
            return this.Flota.Select(vehiculo => vehiculo).Where(vehiculo => vehiculo.Matricula.Equals(matricula)).First<Vehiculo>();
        }


    }
}