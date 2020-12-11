using System;
using System.Globalization;

namespace GestionEmpresaTransporte.Core
{
    public class Empresa
    {
        //-------------------------------------------CONSTRUCTORES------------------------------------------------------

        /// <summary>
        ///     El constructor de garaje vacio
        ///     Inicializa todas las listas vacias
        /// </summary>
        public Empresa()
        {
            ColeccionClientes = new GestorDeClientes();
            ColeccionVehiculos = new ColeccionVehiculos();
            ColeccionTransportes = new ColeccionTransportes();
        }


        public GestorDeClientes ColeccionClientes { get; set; }
        public ColeccionVehiculos ColeccionVehiculos { get; set; }
        public ColeccionTransportes ColeccionTransportes { get; set; }

        //-------------------------------------------CARGAR Y GUARDAR------------------------------------------------------

        /// <summary>
        ///     Carga el XML en las listas
        /// </summary>
        public void CargarXML()
        {
            ColeccionClientes = GestorDeClientes.CargarXML();
            ColeccionVehiculos = ColeccionVehiculos.CargarXml();
            ColeccionTransportes = ColeccionTransportes.CargarXML(ColeccionClientes, ColeccionVehiculos);
        }

        public void GuardaXML()
        {
            ColeccionClientes.GuardaXml();
            ColeccionVehiculos.GuardaXml();
            ColeccionTransportes.GuardaXml();
        }

        //------------------------------------------ADD-----------------------------------------------------------------


        /// <summary>
        ///     Añadir vehiculo a la lista correspondiente
        /// </summary>
        /// <param name="automovil"> Vehiculo a insertar </param>
        /// <returns>
        ///     true= El vehiculo se añadió correctamente
        ///     false= Existia otro vehiculo con la misma matricula
        /// </returns>
        public void AddVehiculo(Vehiculo automovil)
        {
            ColeccionVehiculos.Add(automovil);
        }


        /// <summary>
        ///     Añadir cliente a la lista correspondiente
        /// </summary>
        /// <param name="cliente"> Cliente a insertar </param>
        /// <returns>
        ///     true= El cliente se añadió correctamente
        ///     false= Existia otro cliente con el mismo dni
        /// </returns>
        public void AddCliente(Cliente cliente)
        {
            ColeccionClientes.Add(cliente);
        }


        /// <summary>
        ///     Añadir transporte a la lista correspondiente
        /// </summary>
        /// <param name="mover"> Transporte a añadir </param>
        /// <returns>
        ///     true= Se añadió correctamente el transporte
        ///     false= Existia otro transporte con el mismo id
        /// </returns>
        public void AddTransporte(Transporte transporte)
        {
            ColeccionTransportes.Add(transporte);
        }


        //------------------------------------------GETTERS-------------------------------------------------------------


        /// <summary>
        ///     Devuelve la lista de Vehiculo
        /// </summary>
        /// <returns> La colección de vehiculos </returns>
        public ColeccionVehiculos GetVehiculos()
        {
            return ColeccionVehiculos;
        }


        /// <summary>
        ///     Devuelve la lista de Clientes
        /// </summary>
        /// <returns> La colección de clientes </returns>
        public GestorDeClientes GetClientes()
        {
            return ColeccionClientes;
        }


        /// <summary>
        ///     Devuelve la lista de Transportes
        /// </summary>
        /// <returns> La colección de transportes </returns>
        public ColeccionTransportes GetTransportes()
        {
            return ColeccionTransportes;
        }


        //---------------------------------------------TO STRING--------------------------------------------------------


        /// <summary>
        ///     Devuelve cadena con toda la información almacenada
        /// </summary>
        /// <returns> Cadena </returns>
        public override string ToString()
        {
            var resultado = "";

            resultado += "\n\nVEHICULOS\n\n------------------------------------\n";

            foreach (var v in ColeccionVehiculos) resultado += v + "\n-------------------------------------\n";

            resultado += "\n\nCLIENTES\n\n------------------------------------\n";

            foreach (var c in ColeccionClientes) resultado += c + "\n-------------------------------------\n";

            resultado += "\n\nTRANSPORTES\n\n------------------------------------\n";

            foreach (var t in ColeccionTransportes) resultado += t + "\n-------------------------------------\n";

            return resultado;
        }


        /// <summary>
        ///     Devuelve una cadena con la información filtrada
        /// </summary>
        /// <param name="filtro">
        ///     Char que sirve para seleccionar que se quiere ver
        ///     (v/V) -> Vehiculos
        ///     (c/C) -> Clientes
        ///     (default) -> Transportes
        /// </param>
        /// <returns> Cadena con la información solicitada </returns>
        public string ToString(char filtro)
        {
            var resultado = "";
            switch (filtro)
            {
                case 'V':
                case 'v':
                    resultado += "\n\nVEHICULOS\n";

                    foreach (var v in ColeccionVehiculos) resultado += v + "\n-------------------------------------\n";
                    break;
                case 'C':
                case 'c':
                    resultado += "\n\nCLIENTES\n";

                    foreach (var c in ColeccionClientes) resultado += c + "\n-------------------------------------\n";
                    break;
                default:
                    resultado += "\n\nTRANSPORTES\n";

                    foreach (var t in ColeccionTransportes)
                        resultado += t + "\n-------------------------------------\n";
                    break;
            }

            return resultado;
        }


        //--------------------------------PENDIENTE (VIAJES EN LOS PRÓXIMOS 5 DÍAS)------------------------------------


        /// <summary>
        ///     Saca por pantalla todos los transportes que se van a realizar en los próximos 5 días
        /// </summary>
        /// <returns> Lista con los transportes a realizar en los próximos 5 días </returns>
        public ColeccionTransportes Pendiente()
        {
            var actual = DateTime.Now;
            var limite = actual.AddDays(5.0);
            var resultado = new ColeccionTransportes();

            Console.WriteLine("\n\nTRANSPORTES PARA LOS PRÓXIMOS 5 DÍAS\n");

            foreach (var t in ColeccionTransportes)
            {
                var fechaTransporte = t.FechaSalida;
                if (fechaTransporte.CompareTo(limite) < 0 &&
                    fechaTransporte.CompareTo(actual) > 0)
                {
                    Console.WriteLine("\n" + t + "\n-------------------------------------\n");
                    resultado.Add(t);
                }
            }

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return resultado;
        }


        /// <summary>
        ///     Filtra los transportes para los próximos 5 días de un vehículo en concreto y los saca por pantalla
        /// </summary>
        /// <param name="matricula"> Cadena que representa la matricula de un vehiculo </param>
        /// <returns>
        ///     Lista de transportes para los próximos 5 días del vehiculo con la matricula enviada
        ///     por parámetros
        /// </returns>
        public ColeccionTransportes Pendiente(string matricula)
        {
            var actual = DateTime.Now;
            var limite = actual.AddDays(5.0);
            var resultado = new ColeccionTransportes();

            Console.WriteLine("\n\nTRANSPORTES PARA LOS PRÓXIMOS 5 DÍAS DE VEHÍCULO CON MATRÍCULA:" +
                              matricula + "\n");

            foreach (var t in ColeccionTransportes)
            {
                var fechaTransporte = t.FechaSalida;
                if (fechaTransporte.CompareTo(limite) < 0 &&
                    fechaTransporte.CompareTo(actual) > 0 &&
                    t.Camion.Matricula.Equals(matricula))
                {
                    Console.WriteLine("\n" + t + "\n-------------------------------------\n");
                    resultado.Add(t);
                }
            }

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return resultado;
        }


        /// <summary>
        ///     Filtra los transportes para los próximos 5 días de un vehículo en concreto y los saca por pantalla
        ///     Sirve para llamar a <see cref="this.Pendiente(matricula)" />
        /// </summary>
        /// <param name="vehiculo"> Vehiculo del que interesa saber los viajes </param>
        /// <returns> Lista con los transportes para los próximos 5 días de <see cref="vehiculo" /> </returns>
        public ColeccionTransportes Pendiente(Vehiculo vehiculo)
        {
            return Pendiente(vehiculo.Matricula);
        }


        //-----------------------DISPONIBILIDAD (VEHICULOS SIN TRANSPORTES EN EL FUTURO)--------------------------------


        /// <summary>
        ///     Muestra los vehiculos disponibles y los saca por pantalla
        /// </summary>
        /// <returns> Lista de Flota con los vehiculos disponibles </returns>
        public ColeccionVehiculos Disponibilidad()
        {
            var actual = DateTime.Now;
            Console.WriteLine("\n\nVEHICULOS DISPONIBLES\n");

            var vehiculosDisponible = new ColeccionVehiculos();
            foreach (var v in ColeccionVehiculos) vehiculosDisponible.Add(v);

            foreach (var t in ColeccionTransportes)
            {
                var fechaTransporte = t.FechaSalida;
                if (DateTime.Compare(actual, fechaTransporte) < 0) vehiculosDisponible.Remove(t.Camion);
            }

            foreach (var v in vehiculosDisponible) Console.WriteLine(v + "\n-------------------------------------\n");

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return vehiculosDisponible;
        }


        /// <summary>
        ///     Muestra los vehiculos disponibles de un tipo determinado y los saca por pantalla
        /// </summary>
        /// <param name="filtro"> char que indica el tipo a filtrar </param>
        /// <returns> Lista de Flota con los vehículos disponibles del tipo indicado por <see cref="filtro" /> </returns>
        public ColeccionVehiculos Disponibilidad(char filtro)
        {
            string tipo;
            switch (filtro)
            {
                case 'C':
                case 'c':
                    tipo = "Camion";
                    break;
                case 'F':
                case 'f':
                    tipo = "Furgoneta";
                    break;
                default:
                    tipo = "Camion Articulado";
                    break;
            }

            var actual = DateTime.Now;
            Console.WriteLine("\n\nFLOTA DISPONIBLE DEL TIPO: " + tipo + "\n");

            var vehiculoDisponible = new ColeccionVehiculos();
            foreach (var v in vehiculoDisponible) vehiculoDisponible.Add(v);

            foreach (var t in ColeccionTransportes)
            {
                var fechaTransporte = t.FechaSalida;
                if (DateTime.Compare(actual, fechaTransporte) < 0 || !t.Camion.Tipo.Equals(tipo))
                    vehiculoDisponible.Remove(t.Camion);
            }

            foreach (var v in vehiculoDisponible) Console.WriteLine(v + "\n-------------------------------------\n");

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return vehiculoDisponible;
        }


        //-------------------------------RESERVAS CLIENTES(TRANSPORTES DE UN CLIENTE)-----------------------------------


        /// <summary>
        ///     Saca por pantalla las reservas para un cliente
        ///     Lo hace llamando al método <see cref="ReservasCliente(int dni, char letra)" />
        /// </summary>
        /// <param name="c"> Cliente que interesa encontrar sus reservas </param>
        /// <returns> Lista de transportes para <see cref="c" /> </returns>
        public ColeccionTransportes ReservasCliente(Cliente c)
        {
            return ReservasCliente(c.Nif);
        }


        /// <summary>
        ///     Saca por pantalla las reservas para un cliente filtradas por año
        ///     Lo hace llamando al método <see cref="ReservasCliente(int dni, char letra, int year)" />
        /// </summary>
        /// <param name="c"> Cliente que interesa encontrar sus reservas </param>
        /// <param name="year"> Año por el que se va a filtrar las reservas </param>
        /// <returns> Lista de transportes para <see cref="c" /> en el año <see cref="year" /> </returns>
        public ColeccionTransportes ReservasCliente(Cliente c, int year)
        {
            return ReservasCliente(c.Nif, year);
        }


        /// <summary>
        ///     Saca por pantalla las reservas para un cliente
        /// </summary>
        /// <param name="dni"> Dni completo de un cliente </param>
        /// <returns> Lista con los transportes para el cliente con Dni = <see cref="dni" /> </returns>
        public ColeccionTransportes ReservasCliente(string nif)
        {
            var resultado = new ColeccionTransportes();
            var aux = string.Format("\nLISTADO DE RESERVAS PARA EL CLIENTE: " + nif + "\n");
            Console.WriteLine(aux);

            foreach (var t in ColeccionTransportes)
                if (t.Cliente.Nif == nif)
                {
                    Console.WriteLine(t + "\n-------------------------------------\n");
                    resultado.Add(t);
                }

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return resultado;
        }


        /// <summary>
        ///     Saca por pantalla las reservas para un cliente en un año
        /// </summary>
        /// <param name="dni"> Dni completo de un cliente </param>
        /// <param name="year"> Año para filtrar las reservas </param>
        /// <returns>
        ///     Lista con los transportes para el cliente con Dni = <see cref="dni" /> para el año <see cref="year" />
        /// </returns>
        public ColeccionTransportes ReservasCliente(string nif, int year)
        {
            var resultado = new ColeccionTransportes();
            var aux = string.Format("\nLISTADO DE RESERVAS PARA EL CLIENTE: " + nif + " PARA EL AÑO: " + year + "\n");
            Console.WriteLine(aux);

            foreach (var t in ColeccionTransportes)
            {
                var fechaTransporte = t.FechaSalida;
                if (t.Cliente.Nif == nif && fechaTransporte.Year == year)
                {
                    Console.WriteLine(t + "\n-------------------------------------\n");
                    resultado.Add(t);
                }
            }

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return resultado;
        }


        //------------------RESERVAS(MOSTRAR TRANSPORTES)[Filtro:Vehiculos, Año, Ambas, Ninguna]------------------------


        /// <summary>
        ///     Muestra todos los transportes para toda la flota
        /// </summary>
        /// <returns>
        ///     <see cref="GetTransportes()" />
        /// </returns>
        public ColeccionTransportes ReservasCamion()
        {
            Console.WriteLine(ToString('t'));
            return ColeccionTransportes;
        }


        /// <summary>
        ///     Muestra todos los transportes asociados a un vehiculo
        /// </summary>
        /// <param name="f"> Vehiculo del que interesa saber sus transportes </param>
        /// <returns> Lista para todos los transportes del vehiculo <see cref="f" /> </returns>
        public ColeccionTransportes ReservasCamion(Vehiculo v)
        {
            return ReservasCamion(v.Matricula);
        }


        /// <summary>
        ///     Muestra todos los transportes para un vehiculo dentro de un año
        /// </summary>
        /// <param name="f"> Vehiculo del que interesa conocer sus transportes </param>
        /// <param name="year"> Año del que interesa buscar los transportes </param>
        /// <returns>
        ///     Lista para todos los transportes del vehiculo <see cref="f" /> para el año <see cref="year" />
        /// </returns>
        public ColeccionTransportes ReservasCamion(Vehiculo v, int year)
        {
            return ReservasCamion(v.Matricula, year);
        }


        /// <summary>
        ///     Muestra todos los transportes asociados a un vehiculo
        /// </summary>
        /// <param name="f"> Matricula del vehiculo que interesa conocer sus transportes </param>
        /// <returns> Lista para todos los transportes del vehiculo con matricula <see cref="f" /> </returns>
        public ColeccionTransportes ReservasCamion(string matricula)
        {
            Console.WriteLine("\nTRANSPORTES PARA EL VEHICULO: " + matricula + "\n");
            var resultado = new ColeccionTransportes();

            foreach (var t in ColeccionTransportes)
                if (t.Camion.Matricula.Equals(matricula))
                {
                    Console.WriteLine(t + "\n-----------------------------\n");
                    resultado.Add(t);
                }

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return resultado;
        }


        /// <summary>
        ///     Muestra todos los transportes pertenecientes a un año
        /// </summary>
        /// <param name="year"> Año del que interesa conocer todos los transportes </param>
        /// <returns> Lista con todos los transportes para el año <see cref="year" /> </returns>
        public ColeccionTransportes ReservasCamion(int year)
        {
            var resultado = new ColeccionTransportes();
            Console.WriteLine("\nTRANSPORTES DENTRO DEL AÑO: " + year + "\n");

            foreach (var t in ColeccionTransportes)
            {
                var fechaTransporte = t.FechaSalida;
                if (fechaTransporte.Year == year)
                {
                    Console.WriteLine(t + "\n-----------------------------\n");
                    resultado.Add(t);
                }
            }

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return resultado;
        }


        /// <summary>
        ///     Muestra todos los transportes para un vehiculo dentro de un año
        /// </summary>
        /// <param name="f"> Vehiculo del que interesa conocer sus transportes </param>
        /// <param name="year"> Año del que interesa buscar los transportes </param>
        /// <returns>
        ///     Lista con todos los transportes del vehiculo con matricula <see cref="f" /> para el año <see cref="year" />
        /// </returns>
        public ColeccionTransportes ReservasCamion(string matricula, int year)
        {
            var resultado = new ColeccionTransportes();
            Console.WriteLine("\nTRANSPORTES DENTRO DEL AÑO: " + year + " PARA EL VEHICULO: " + matricula + "\n");

            foreach (var t in ColeccionTransportes)
            {
                var fechaTransporte = t.FechaSalida;
                if (fechaTransporte.Year == year && t.Camion.Matricula.Equals(matricula))
                {
                    Console.WriteLine(t + "\n-----------------------------\n");
                    resultado.Add(t);
                }
            }

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return resultado;
        }


        //-----------------------------------RESERVAS POR DÍA[Filtra por fecha]-----------------------------------------


        /// <summary>
        ///     Muestra los transportes para un día
        /// </summary>
        /// <param name="fecha"> Dia del que interesa saber los transportes </param>
        /// <returns> Lista con todos los transportes para el día <see cref="fecha" /> </returns>
        public ColeccionTransportes ReservasDia(DateTime fecha)
        {
            var resultado = new ColeccionTransportes();
            Console.WriteLine("\nTRANSPORTES PARA EL DIA: " + fecha.ToString("dd/MM/yyyy") + "\n");

            foreach (var t in ColeccionTransportes)
            {
                var fechaTransporte = t.FechaSalida;
                if (fecha.ToString("ddMMyyyy").Equals(fechaTransporte.ToString("ddMMyyyy")))
                {
                    Console.WriteLine(t + "\n-----------------------------\n");
                    resultado.Add(t);
                }
            }

            Console.WriteLine("\nFINALIZADO EL LISTADO");
            return resultado;
        }
    }
}