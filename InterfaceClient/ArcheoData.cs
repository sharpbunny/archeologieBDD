using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceClient
{
    /// <summary>
    /// 
    /// </summary>
    public class ArcheoData
    {
        public ArcheoData(int line, string id, string no, string co, string dp, float lat, float lon, DateTime? ddeb, DateTime? dfin, string th)
        {
            LineNumber = line;
            IDLigne = id;
            NomSite = no;
            NomCommune = co;
            NomDepartement = dp;
            Latitude = lat;
            Longitude = lon;
            DateDebut = ddeb;
            DateFin = dfin;
            Theme = th;
        }

        public int LineNumber { get; }

        public string IDLigne { get; }

        public string NomSite { get; set; }

        public string NomCommune { get; set; }

        public string NomDepartement { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public DateTime? DateDebut { get; set; }

        public DateTime? DateFin { get; set; }

        public string Theme { get; set; }

    }
}
