using CarServiceApp.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace CarServiceApp.Model
{
    public static class DataWorker
    {
        //получить всех поставщиков 
        public static List<Supplier> GetAllSuppliers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Suppliers.ToList();
                return result;
            }
        }
        //получить все детали 
        public static List<Part> GetAllParts()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Parts.ToList();
                return result;
            }
        }

        //получить все поставки
        public static List<Supply> GetAllSupplys()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Supplys.ToList();
                return result;
            }
        }

        //создать поставщика
        public static string CreateSupplier(string name, string adress, int phone)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем существует ли отдел
                bool chekIsExist = db.Suppliers.Any(el => el.Name == name && el.Adress == adress && el.Phone == phone);
                if (!chekIsExist)
                {
                    Supplier newSupplier = new Supplier 
                    {
                        Name = name, 
                        Adress = adress, 
                        Phone = phone
                    };
                    db.Suppliers.Add(newSupplier);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        //создать деталь

        public static string CreatePart(string name, decimal vendorCode, decimal price, string note, Supplier supplier)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем существует ли деталь
                bool chekIsExist = db.Parts.Any(el => el.Name == name && el.VendorCode == vendorCode && el.Price == price && el.Note == note);
                if (!chekIsExist)
                {
                    Part newPart = new Part
                    {
                        Name = name,
                        VendorCode = vendorCode,
                        Price = price,
                        Note = note,
                        SupplierId = supplier.Id
                    };
                    db.Parts.Add(newPart);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        //создать поставку

        public static string CreateSupply(int countOfPart, string date, Part part)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем существует поставка
                bool chekIsExist = db.Supplys.Any(el => el.CountOfPart == countOfPart && el.Date == date && el.Part == part);
                if (!chekIsExist)
                {
                    Supply newSupply = new Supply
                    {
                        CountOfPart = countOfPart,
                        Date = date,
                        PartId = part.Id
                    };
                    db.Supplys.Add(newSupply);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        //удалить поставщика
        public static string DeleteSupplier(Supplier supplier)
        {
            string result = "Такого поставщика не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
                result = $"Сделано! {supplier.Name} удален!";
            }
            return result;
        }

        //удалить деталь
        public static string DeletePart(Part part)
        {
            string result = "Такой детали не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Parts.Remove(part);
                db.SaveChanges();
                result = $"Сделано! {part.Name} удален!";
            }
            return result;
        }

        //удалить поставку
        public static string DeleteSupply(Supply supply)
        {
            string result = "Такой поставки не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Supplys.Remove(supply);
                db.SaveChanges();
                result = $"Сделано! Поставка удалена!";
            }
            return result;
        }

        //редактировать поставщика
        public static string EditeSupplier(Supplier OldSupplier, string newName, string newAdress, int newPhone)
        {
            string result = "Такого поставщика не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Supplier supplier = db.Suppliers.FirstOrDefault(newSupplier => newSupplier.Id == OldSupplier.Id );
                supplier.Name = newName;
                supplier.Adress = newAdress;
                supplier.Phone = newPhone;
                db.SaveChanges();
                result = $"Сделано! {supplier.Name} изменен!";
            }
            return result;
        }

        //редактировать деталь
        public static string EditePart(Part OldPart, string newName, decimal newVendorCode, decimal newPrice, string newNote, Supplier newSupplier)
        {
            string result = "Такой детали не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Part part = db.Parts.FirstOrDefault(newPart => newPart.Id == OldPart.Id);
                part.Name = newName;
                part.VendorCode = newVendorCode;
                part.Price = newPrice;
                part.Note = newNote;
                part.SupplierId = newSupplier.Id;
                db.SaveChanges();
                result = $"Сделано! {part.Name} изменена!";
            }
            return result;
        }

        //редактировать поставку
        public static string EditeSupply(Supply OldSupply, int newCountOfPart, string newDate, Part newPart)
        {
            string result = "Такой поставки не существует";
            using (ApplicationContext db = new ApplicationContext())
            {                
                Supply supply = db.Supplys.FirstOrDefault(newSupply => newSupply.Id == OldSupply.Id);                
                    supply.CountOfPart = newCountOfPart;
                    supply.Date = newDate;
                    supply.PartId = newPart.Id;
                    db.SaveChanges();
                    result = $"Сделано! Поставка изменена!";                               
            }
            return result;
        }

        //получение детали по id детали
        public static Part GetPartById (int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Part part = db.Parts.FirstOrDefault(p => p.Id == id);
                return part;
            }
        }

        //получение поставщика по id поставщика
        public static Supplier GetSupplierById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Supplier supplier = db.Suppliers.FirstOrDefault(p => p.Id == id);
                return supplier;
            }
        }
        //получение деталей по id поставщика
        public static List<Part> GetAllPartsBySupplierId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Part> parts = (from part in GetAllParts() where part.SupplierId == id select part).ToList();
                return parts;
            }

        }
    }
}
