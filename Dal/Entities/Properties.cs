
using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class Properties
    {
        
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime Created { get; set; }
        public DateTime Observed { get; set; }
        public string ParameterId { get; set; }
        public string StationId { get; set; }

    }

}