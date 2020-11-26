using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica2_Ejercicio1.Transporte
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    class ColeccionTransportes : ICollection<Transporte>
    {
        protected List<Transporte> ListaTransportes = new List<Transporte>();

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
    }
}
