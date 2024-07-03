using SingSiamOffice.Models;

namespace SingSiamOffice.Helpers
{
    public class NumberToText
    {
        public async Task<string> CustomerStatusText(int status_number)
        {
            switch (status_number)
            {
                case -2:
                    return "Blacklist";
                case -1:
                    return "ยกเลิก";
                case 0:
                    return "รอยืนยันการสมัคร";
                case 1:
                    return "ปกติ";
                default:
                    return null;
            }
        }
        public async Task<string> Paid_By(int paidby)
        {
            switch (paidby)
            {

                case 1:
                    return "เงินสด";
                case 2:
                    return "เงินโอน";
                case 3:
                    return "อื่นๆ";
                case 4:
                    return "รับฝากเงินล่วงหน้า";
                default:
                    return null;
            }
        }
        public async Task<string> PromiseStatusText(int status_number)
        {
            switch (status_number)
            {
                case -2:
                    return "Blacklist";
                case -1:
                    return "ยกเลิก";
                case 0:
                    return "ปกติ";
                case 1:
                    return "ปิดสัญญา";
                case 2:
                    return "ปิดสัญญาก่อนกำหนด";
                default:
                    return null;
            }
        }
        public async Task<string> ReceiptStatusText(int status_number)
        {
            switch (status_number)
            {

                case 0:
                    return "ยังไม่ได้ชำระ";
                case 1:
                    return "ชำระค่างวด";
                case 2:
                    return "ปิดสัญญาก่อนกำหนด";
                case 3:
                    return "ปิดสัญญาแบบมีส่วนต่าง";
                case 4:
                    return "รับฝากเงินล่วงหน้า";
                case 5:
                    return "คืนเงินต้น";
                default:
                    return null;
            }
        }
        private decimal RoundToNearest(decimal value)
        {
            // ปัดเศษทศนิยม 1 ตำแหน่งและใช้ MidpointRounding.AwayFromZero
            return Math.Round(value, 0, MidpointRounding.AwayFromZero);
        }
        public async Task<calamount> cal_amonut(decimal capital, int periods_qty, decimal intrate, int product_id)
        {
            SingsiamdbContext db = new SingsiamdbContext();
            calamount c = new calamount();
            var loanrate = db.Collaterals.Where(s => s.Id == product_id).FirstOrDefault().Loanrate / 100;
            var intrate_qty = intrate / 100;
            int amonut_interate = (int)(capital * intrate_qty);
            var amount = capital / periods_qty;
            var chargeamt = capital * loanrate;
            c.amount_Ptype1 = amount;

            c.amount_Ptype2 =RoundToNearest(amonut_interate);
            c.chargement = chargeamt;
            c.interate = RoundToNearest(amonut_interate);
            c.total_amount = RoundToNearest((amount + amonut_interate));
            c.capital = RoundToNearest((capital / periods_qty));

            return c;
        }




        public async Task<string> ConvertNumberToThaiWords(int number)
        {
            // กำหนดคำศัพท์สำหรับตัวเลขและหน่วยในภาษาไทย
            string[] thaiDigits = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า" };
            string[] thaiUnits = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };

            // ตรวจสอบว่าตัวเลขเป็น 0 หรือไม่ ถ้าใช่ คืนค่า "ศูนย์บาท"
            if (number == 0)
                return thaiDigits[0] + "บาท";

            string result = ""; // ตัวแปรสำหรับเก็บผลลัพธ์
            int unitPosition = 0; // ตำแหน่งของหน่วย (หลักสิบ, ร้อย, พัน, ฯลฯ)
            bool isFirstDigit = true; // ตัวแปรตรวจสอบว่าตัวเลขตัวแรก (ขวาสุด) หรือไม่

            // ลูปเพื่อแปลงตัวเลขแต่ละหลักไปเป็นคำในภาษาไทย
            while (number > 0)
            {
                int digit = number % 10; // ดึงค่าของหลักปัจจุบัน

                if (digit != 0) // ถ้าหลักปัจจุบันไม่ใช่ 0
                {
                    string digitWord = thaiDigits[digit]; // คำที่แทนค่าตัวเลข
                    string unitWord = thaiUnits[unitPosition]; // คำที่แทนหน่วย

                    if (unitPosition == 1) // กรณีเป็นหลักสิบ
                    {
                        if (digit == 1)
                        {
                            digitWord = isFirstDigit ? "หนึ่ง" : ""; // ถ้าเป็นตัวแรก ให้ใช้ "หนึ่ง" มิเช่นนั้นไม่ต้องแสดงคำ
                        }
                        else if (digit == 2)
                        {
                            digitWord = "ยี่"; // ถ้าเป็นเลข 2 ในหลักสิบ ใช้ "ยี่"
                        }
                    }

                    result = digitWord + unitWord + result; // ประกอบคำกลับเข้าไปในผลลัพธ์
                }

                number /= 10; // ตัดหลักขวาสุดออก
                unitPosition++; // เลื่อนไปหลักถัดไป
                isFirstDigit = false; // หลังจากการวนลูปครั้งแรก ไม่ใช่ตัวแรกแล้ว
            }

            result += "บาท"; // เพิ่มคำว่า "บาท" ท้ายสุด
            return result; // คืนค่าผลลัพธ์
        }


    }
    public class calamount
    {
        public decimal capital { get; set; }
        public decimal? chargement { get; set; }
        public decimal amount_Ptype1 { get; set; }
        public decimal amount_Ptype2 { get; set; }
        public decimal service { get; set; }
        public decimal interate { get; set; }
        public decimal total_amount { get; set; }
        public decimal total_interate_service { get; set;}
       
    }
}
