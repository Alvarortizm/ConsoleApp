using System;
using System.IO;
using System.Threading;

namespace ConsoleApp
{
    /*
     * Prueba técnica aspirante a desarrollador Junior de Backend, LAP.
     * 
     * Autor: Alvaro Ortiz M.
     * 
     */

    public class Program
    {
        //Pass the file path and file name to the StreamWriter constructor(...\ConsoleApp\bin\Debug\net6.0)
        private static StreamWriter sw = new StreamWriter("Test.txt");
        // contador de resultados impresos
        private static int countResult = 0;

        /**
         * Punto de entrada de la aplicacion
         */
        static void Main(string[] args)
        {
            Thread hilo1 = new Thread(Mensaje);
            // hilo que contara las palabras que terminen en n.
            hilo1.Start(1);
            Thread hilo2 = new Thread(Mensaje);
            // hilo que contara el número de oraciones que contengan más de 15 palabras.
            hilo2.Start(2);
            Thread hilo3 = new Thread(Mensaje);
            // hilo que contara el número de parrafos
            hilo3.Start(3);
            // Contara el número de caracteres alfanuméricos distintos a n o N que contenga el archivo.
            Mensaje(4);
            Console.WriteLine("Presione enter para salir.....");
            Console.ReadLine();
        }

        /// <summary>
        /// Realiza la lectura del archivo de texto y haces los calculos segun el hilo correspondiente
        /// </summary>
        /// <param name="o">identifica cada hilo</param>
        static void Mensaje(object o)
        {
            switch (o)
            {
                case 1:
                    try
                    {
                        //Pass the file path and file name to the StreamReader constructor(...\ConsoleApp\bin\Debug\net6.0)
                        StreamReader sr = new StreamReader("Data.txt");
                        //Read the first line of text
                        String line = sr.ReadLine();
                        int acumulatorN = 0;
                        //Continue to read until you reach end of file
                        while (line != null)
                        {
                            //acumula la cantidad de 'n'  encontradas al final de las palabras  en cada linea del archivo
                            acumulatorN = acumulatorN + CountN(line); ;
                            //Read the next line
                            line = sr.ReadLine();
                        }
                        //close the file                      
                        sr.Close();
                        //ejecuta la rutina que imprime en el archivo el resultado calculado
                        PrintResults("EL TOTAL DE PALABRAS TERMINADAS EN 'n' ENCONTRADAS FUE: " + acumulatorN);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception 1:  " + e.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        //Pass the file path and file name to the StreamReader constructor(...\ConsoleApp\bin\Debug\net6.0)
                        StreamReader sr = new StreamReader("Data.txt");
                        //Read the first line of text
                        String line = sr.ReadLine();
                        int acumulatorWords = 0;
                        //Continue to read until you reach end of file
                        while (line != null)
                        {
                            acumulatorWords = acumulatorWords + CountWords(line); ;
                            //Read the next line
                            line = sr.ReadLine();
                        }
                        //close the file                      
                        sr.Close();
                        //ejecuta la rutina que imprime en el archivo el resultado calculado
                        PrintResults("EL TOTAL DE ORACIONES CON MAS DE 15 PALABRAS  FUE: " + acumulatorWords);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception 2: " + e.Message);
                    }
                    break;
                case 3:
                    try
                    {
                        //Pass the file path and file name to the StreamReader constructor(...\ConsoleApp\bin\Debug\net6.0)
                        StreamReader sr = new StreamReader("Data.txt");
                        //Read the first line of text
                        String line = sr.ReadLine();
                        String lastCharacter;
                        int counterParrafo = 0;
                        //Continue to read until you reach end of file
                        while (line != null)
                        {
                            int lenghtLine = line.Length;
                            if (lenghtLine > 0)
                            {
                                lastCharacter = line.Substring(lenghtLine - 1, 1);
                                if (lastCharacter.Equals("."))
                                {
                                    counterParrafo++;
                                }
                                //Read the next line
                            }
                            line = sr.ReadLine();

                        }
                        //close the file                      
                        sr.Close();
                        PrintResults("EL TOTAL DE PARRAFOS ENCONTRADOS FUE: " + counterParrafo);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception 3:  " + e.Message);
                    }
                    break;
                case 4:

                    try
                    {
                        String abecedario = "abcdefghijklmopqrstuvwxyz0123456789";
                        int countLetter = 0;
                        //Pass the file path and file name to the StreamReader constructor(...\ConsoleApp\bin\Debug\net6.0)
                        StreamReader sr = new StreamReader("Data.txt");
                        //Read the first line of text
                        String line = sr.ReadLine();
                        while (line != null)
                        {
                            int lenghtLine = line.Length;
                            Boolean foundLetter = false;
                            int indexLine = 0;
                            while (indexLine < line.Length)
                            {
                                int indexAbecedario = 0;
                                while (indexAbecedario < abecedario.Length && !foundLetter)
                                {
                                    String letraAbecedario = abecedario.Substring(indexAbecedario, 1);
                                    String letraLinea = line.Substring(indexLine, 1).ToLower();
                                    if (letraAbecedario.Equals(letraLinea))
                                    {
                                        foundLetter = true;
                                    }
                                    indexAbecedario++;
                                }
                                indexLine++;
                                if (foundLetter)
                                {
                                    countLetter++;
                                    foundLetter = false;
                                }

                            }
                            line = sr.ReadLine();
                        }
                        sr.Close();
                        PrintResults("EL TOTAL DE CARACTERES ALFANUMERICOS ([a-z][A-Z][0-9]) ENCONTRADOS FUE: " + countLetter);
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine("Exception 4:  " + e.Message);

                    }
                    break;
            }
            //sw.Close();
        }

        /// <summary>
        /// Cuenta palabrasde la linea terminan en 'n'
        /// </summary>
        /// <param name="value">Linea de texto tomada del archivo de entrada</param>
        /// <returns>Numero de palabras terminadas en'n' </returns>
        public static int CountN(String value)
        {
            int counterN = 0;
            int startIndex = 1;
            int lengthValue = value.Length;
            bool foundN = false;
            while (startIndex < lengthValue)
            {
                String caracter = value.Substring(startIndex, 1);
                String caracterAnterior = value.Substring(startIndex - 1, 1);
                if (foundN)
                {
                    counterN++;
                }
                if (caracter.Equals(" ") || caracter.Equals(".") || caracter.Equals(",") || caracter.Equals(";"))
                {
                    if (caracterAnterior.Equals("n")) foundN = true;
                }
                else
                {
                    foundN = false;

                }
                startIndex++;
            }
            if (foundN)
            {
                counterN++;
            }
            return counterN;
        }

        /// <summary>
        /// Cuenta quincenas de palabras por oracion
        /// </summary>
        /// <param name="value">Linea leida del archivo de entrada</param>
        /// <returns>Numero de oraciones con mas de quince palabras</returns>
        public static int CountWords(String value)
        {
            // variable contadora del numero de palabras
            int counterWords = 0;
            // variable contadora de cada grupo de quince palabras
            int acumulatorWords = 0;
            // variable indice que recorre la linea leida
            int startIndex = 1;
            // variable que conserva el tamaño o numero de caracteres de la linea leida
            int lengthValue = value.Length;
            //ciclo que recorre toda la cadena linea en busqueda de oraciones con mas de 15 palabras
            while (startIndex < lengthValue)
            {
                String caracter = value.Substring(startIndex, 1);
                String caracterAnterior = value.Substring(startIndex - 1, 1);
                if (caracter.Equals(" ") || caracter.Equals(",") || caracter.Equals(";"))
                {
                    if (!(caracterAnterior.Equals(" ") || caracterAnterior.Equals(",") || caracterAnterior.Equals(";")))
                    {
                        counterWords++;
                    }
                }
                else
                {
                    if (caracter.Equals("."))
                    {
                        counterWords++;
                        if (counterWords > 15)
                        {
                            acumulatorWords++;
                            counterWords = 0;
                        }
                    }
                }
                startIndex++;
            }
            return acumulatorWords;
        }

        /// <summary>
        /// Imprime los resultados en el archivo
        /// </summary>
        /// <param name="result">Cadena que contiene el resultado o linea a imprimir en el archivo</param>
        public static void PrintResults(String result)
        {
            try
            {
                countResult++;
                //Write a line of text               
                sw.WriteLine(result);
                if (countResult == 4)
                {
                    //solo al completar los 4 resultados se cierra el archivo
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine(result);
            }
        }
    }
}

