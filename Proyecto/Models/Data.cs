using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Data
    {
        private static Data instance;

        public static Data Instance
        {
            get
            {
                if (instance == null) return instance = new Data();

                return instance;
            }
        }

        public List<Pelicula> a1;

        public Data()
        {
            a1 = new List<Pelicula>();
        }


    }
}