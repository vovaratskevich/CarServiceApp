using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServiceApp.Model
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int Phone { get; set; }        
        public List<Part> Part { get; set; }
        [NotMapped]
        
        public List<Part> SupplierPart
        {
            get
            {
                return DataWorker.GetAllPartsBySupplierId(Id);
            }
        }       
    }
}
