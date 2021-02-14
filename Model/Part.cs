using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServiceApp.Model
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal VendorCode { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public List<Supply> Supply { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } 
        [NotMapped]
        public Supplier PartSupplier
        {
            get
            {
                return DataWorker.GetSupplierById(SupplierId);
            }
        }        
    }
}
