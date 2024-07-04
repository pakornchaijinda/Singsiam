using System.ComponentModel.DataAnnotations.Schema;

namespace SingSiamOffice.Models
{
    public partial class Promise
    {
        [NotMapped]
        public decimal Interest { get; set; }
        [NotMapped]
        public decimal CapitalCal { get; set; }
        [NotMapped]
        public decimal Interest_Service { get; set; }
        [NotMapped]
        public DateTime FirstDatePay { get; set; }
    }
    public partial class Periodtran
    {
        //วันที่ล่าช้า
        [NotMapped]
        public int latedate { get; set; }
        [NotMapped]
        public DateTime currentdate { get; set; }

        //วันที่จ่ายแต่ละงวด
        [NotMapped]
        public DateTime tdate_pay { get; set; }
        [NotMapped]
        public string style_color { get; set; }

        //จำนวนคงเหลือ
        [NotMapped]
        public decimal amount_remain { get; set; }

        //จำนวนงวดที่ค้างชำระ
        [NotMapped]
        public int OverPayQty { get; set; }

        //จำนวนยอดรับฝากเงิน
        [NotMapped]
        public decimal total_deposit { get; set;}

        //ค่าปรับ
        [NotMapped]
        public decimal total_fee { get; set;}

        //จำนวนที่ต้องชำระ
        [NotMapped]
        public decimal total_deptAmount { get; set; }

        //ค่าทวงถามค่าติดตาม
        [NotMapped]
        public decimal total_charge_follow { get; set; }

        //เช็คงวดที่เกิดวันกำหนดชำระ
        [NotMapped]
        public bool check_overpay { get; set; }
        //ค้างชำระยกไป
        [NotMapped]
        public decimal pending_amount { get; set; }
        //รวมเงินต้นทั้งหมด
        [NotMapped]
        public decimal total_capital { get; set; }

        //รวมดอกเบี้ยทั้งหมด
        [NotMapped]
        public decimal total_interest { get; set; }

        //รวมงวดที่ต้องชำระที่เหลือทั้งหมด
        [NotMapped]
        public int total_periods_remain { get; set; }
        //รับฝากเงิน
        [NotMapped]
        public bool ck_deposit { get; set;}
    }
    public partial class Receipttran 
    {
        [NotMapped]
        public List<Periodtran> peroidtrans_info { get; set; }
    }
    public partial class Receiptdesc
    {
        [NotMapped]
        public decimal pending_amount { get; set; }
        [NotMapped]
        public int payment_method { get; set; }
    }
}
