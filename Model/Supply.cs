using System.ComponentModel.DataAnnotations.Schema;

namespace CarServiceApp.Model
{
    public class Supply
    {
        public int Id { get; set; }
        public int CountOfPart { get; set; }
        public string Date { get; set; }
        public int PartId { get; set; }
        public virtual Part Part { get; set; }
        [NotMapped]
        public Part SypplyPart
        {
            get
            {
                return DataWorker.GetPartById(PartId);
            }
        }
    }
}
