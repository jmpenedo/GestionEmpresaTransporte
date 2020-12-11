using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace GestionEmpresaTransporte.Core
{
    public class utilidades
    {
        /// <summary>
        ///     Autor: jmpenedo@esei.uvigo.es
        ///     REcibe una cadena y devuelve una cadena con los
        ///     caracteres que son digitos
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static string stringToTelString(string cadena)
        {
            var toret = new StringBuilder();
            foreach (var caracter in cadena)
                if (char.IsDigit(caracter))
                    toret.Append(caracter);

            return toret.ToString();
        }


        /// <summary>
        ///     Autor validaciones de NIF: http://touzas.blogspot.com/2016/03/validacion-nif-nie-cif.html
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool valida_NIFCIFNIE(string data)
        {
            if (string.IsNullOrEmpty(data) || data.Length < 8)
                return false;

            var initialLetter = data.Substring(0, 1).ToUpper();
            if (char.IsLetter(data, 0))
                switch (initialLetter)
                {
                    case "X":
                        data = "0" + data.Substring(1, data.Length - 1);
                        return validarNIF(data);
                    case "Y":
                        data = "1" + data.Substring(1, data.Length - 1);
                        return validarNIF(data);
                    case "Z":
                        data = "2" + data.Substring(1, data.Length - 1);
                        return validarNIF(data);
                    default:
                        if (new Regex("[A-Za-z][0-9]{7}[A-Za-z0-9]{1}$").Match(data).Success)
                            return validadCIF(data);
                        break;
                }
            else if (char.IsLetter(data, data.Length - 1))
                if (new Regex("[0-9]{8}[A-Za-z]").Match(data).Success ||
                    new Regex("[0-9]{7}[A-Za-z]").Match(data).Success)
                    return validarNIF(data);

            return false;
        }

        private static string getLetra(int id)
        {
            var letras = new Dictionary<int, string>();
            letras.Add(0, "T");
            letras.Add(1, "R");
            letras.Add(2, "W");
            letras.Add(3, "A");
            letras.Add(4, "G");
            letras.Add(5, "M");
            letras.Add(6, "Y");
            letras.Add(7, "F");
            letras.Add(8, "P");
            letras.Add(9, "D");
            letras.Add(10, "X");
            letras.Add(11, "B");
            letras.Add(12, "N");
            letras.Add(13, "J");
            letras.Add(14, "Z");
            letras.Add(15, "S");
            letras.Add(16, "Q");
            letras.Add(17, "V");
            letras.Add(18, "H");
            letras.Add(19, "L");
            letras.Add(20, "C");
            letras.Add(21, "K");
            letras.Add(22, "E");
            return letras[id];
        }

        private static bool validarNIF(string data)
        {
            if (data == string.Empty)
                return false;
            try
            {
                string letra;
                letra = data.Substring(data.Length - 1, 1);
                data = data.Substring(0, data.Length - 1);
                var nifNum = int.Parse(data);
                var resto = nifNum % 23;
                var tmp = getLetra(resto);
                if (tmp.ToLower() != letra.ToLower())
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        private static bool validadCIF(string data)
        {
            try
            {
                var pares = 0;
                var impares = 0;
                int suma;
                string ultima;
                int unumero;
                var uletra = new[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "0"};
                var fletra = new[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};
                var fletra1 = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
                string xxx;

                /*
                * T      P      P      N  N  N  N  N  C
                Siendo:
                T: Letra de tipo de Organización, una de las siguientes: A,B,C,D,E,F,G,H,K,L,M,N,P,Q,S.
                P: Código provincial.
                N: Númeración secuenial dentro de la provincia.
                C: Dígito de control, un número ó letra: Aó1,Bó2,Có3,Dó4,Eó5,Fó6,Gó7,Hó8,Ió9,Jó0.
                *
                *
                A.    Sociedades anónimas.
                B.    Sociedades de responsabilidad limitada.
                C.    Sociedades colectivas.
                D.    Sociedades comanditarias.
                E.    Comunidades de bienes y herencias yacentes.
                F.    Sociedades cooperativas.
                G.    Asociaciones.
                H.    Comunidades de propietarios en régimen de propiedad horizontal.
                I.    Sociedades civiles, con o sin personalidad jurídica.
                J.    Corporaciones Locales.
                K.    Organismos públicos.
                L.    Congregaciones e instituciones religiosas.
                M.    Órganos de la Administración del Estado y de las Comunidades Autónomas.
                N.    Uniones Temporales de Empresas.
                O.    Otros tipos no definidos en el resto de claves.

                */
                data = data.ToUpper();

                ultima = data.Substring(8, 1);

                var cont = 1;
                for (cont = 1; cont < 7; cont++)
                {
                    xxx = 2 * int.Parse(data.Substring(cont++, 1)) + "0";
                    impares += int.Parse(xxx.Substring(0, 1)) + int.Parse(xxx.Substring(1, 1));
                    pares += int.Parse(data.Substring(cont, 1));
                }

                xxx = 2 * int.Parse(data.Substring(cont, 1)) + "0";
                impares += int.Parse(xxx.Substring(0, 1)) + int.Parse(xxx.Substring(1, 1));

                suma = pares + impares;
                unumero = int.Parse(suma.ToString().Substring(suma.ToString().Length - 1, 1));
                unumero = 10 - unumero;
                if (unumero == 10) unumero = 0;

                if (ultima == unumero.ToString() || ultima == uletra[unumero - 1])
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        ///  Verifica que una matrícula es correcta en formato EU
        /// </summary>
        /// <param name="matricula"></param>
        /// <returns></returns>
        public static bool ValidarMatricula(string matricula)
        {
           
            Regex rx = new Regex(@"[0-9]{4}[A-Z]{3}");
            return rx.IsMatch(matricula.ToUpper()) & matricula.Length == 7;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
#pragma warning disable 168
            catch (RegexMatchTimeoutException e)
#pragma warning restore 168
            {
                return false;
            }
#pragma warning disable 168
            catch (ArgumentException e)
#pragma warning restore 168
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidFechaSalida(DateTime fechaContratacion, DateTime fechaSalida)
        {
            if ((fechaSalida - fechaContratacion).TotalDays < 0)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidFechaEntrega(DateTime fechaSalida, DateTime fechaEntrega)
        {
            if ((fechaEntrega - fechaSalida).TotalDays < 0)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidFechaContratacion(DateTime fechaContratacion, DateTime fechaSalida, DateTime fechaEntrega)
        {
            if ((fechaEntrega - fechaContratacion).TotalDays < 0
                || (fechaSalida - fechaContratacion).TotalDays < 0)
            {
                return false;
            }

            return true;
        }
    }
}