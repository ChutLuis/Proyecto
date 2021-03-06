﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Proyecto.Models
{
    public class Pelicula
    {
        public string Tipo { get;set;}
        public string Nombre { get;set;}
        public int AñoLanzamiento { get; set;}
        public string Genero { get; set;}
        public string ToFixedSizeString()
        {
            return $"{string.Format("{0,-20}", Tipo)} |{string.Format("{0,-20}", Nombre)}" +
                   $"{AñoLanzamiento.ToString("00000000000,-00000000000")}|{string.Format("{0,-20}", Genero)}";                   
        }
        public int FixedSizeText
        {
           get { return FixedSize; }
        }
        public static int FixedSize { get { return 127; } }

        public class ByteGenerator
        {
            public static byte[] ConverToBytes(string Text)
            {
                return Encoding.UTF8.GetBytes(Text);
            }
            public static string ConvertToString(byte[] bytes)
            {
                return Encoding.UTF8.GetString(bytes);
            }

            public static byte[] ConvertToBytes(char[] text)
            {
                return Encoding.UTF8.GetBytes(text);
            }
        }
    }
}