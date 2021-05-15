using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cafe_cafe
{
    public class Bill
    {
        private int iD;
        private DateTime? DateCheckIn;
        private DateTime? DateCheckOut;
        private int status;

        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status)
        {
            this.iD = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.status = status;
        }

        public Bill(DataRow row)
        {
            this.iD = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckIn"];

            var dateCheckOutTmp = row["dateCheckOut"];
            if(dateCheckOutTmp.ToString() != "")
                this.DateCheckOut = (DateTime?)dateCheckOutTmp;

            this.status = (int)row["status"];
        }

        public int ID { get => iD; set => iD = value; }
        public DateTime? DateCheckIn1 { get => DateCheckIn; set => DateCheckIn = value; }
        public DateTime? DateCheckOut1 { get => DateCheckOut; set => DateCheckOut = value; }
        public int Status { get => status; set => status = value; }
    }
}
