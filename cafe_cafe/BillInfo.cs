using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cafe_cafe
{
    public class BillInfo
    {
        private string nameFood;
        private int count;
        private float price;
        private float totalPrice;
        private int idFood;

        public BillInfo(int id, string name, int count, float price, float totalPrice = 0)
        {
            this.idFood = id;
            this.nameFood = name;
            this.count = count;
            this.price = price;
            this.totalPrice = totalPrice;
        }

        public BillInfo(DataRow row)
        {
            this.idFood = (int)row["id"];
            this.nameFood = row["name"].ToString();
            this.count = (int)row["count"];
            this.price = (float)Convert.ToDouble(row["price"].ToString());
            this.totalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }


        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public string NameFood { get => nameFood; set => nameFood = value; }
        public int IdFood { get => idFood; set => idFood = value; }



        //private int iD;
        //private int idBill;
        //private int idFood;
        //private int count;

        //public BillInfo(int id, int idBill, int idFood, int count)
        //{
        //    this.iD = id;
        //    this.idBill = idBill;
        //    this.idFood = idFood;
        //    this.count = count;
        //}

        //public BillInfo(DataRow row)
        //{
        //    this.iD = (int)row["id"];
        //    this.idBill = (int)row["idBill"];
        //    this.idFood = (int)row["idFood"];
        //    this.count = (int)row["count"];
        //}

        //public int IdBill { get => idBill; set => idBill = value; }
        //public int IdFood { get => idFood; set => idFood = value; }
        //public int Count { get => count; set => count = value; }
        //public int ID { get => iD; set => iD = value; }
    }
}
