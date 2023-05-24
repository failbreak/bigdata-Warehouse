using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class Feature
    {
        public int Featureid { get; set; }

        //public Geometry Geometry { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public List<Properties> Properties { get; set; }
    }
}
