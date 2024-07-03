using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using SingSiamOffice.Manage;
using SingSiamOffice.Models;
using System.Globalization;
using static MudBlazor.Icons.Custom;

namespace SingSiamOffice.Pages.Contracts
{
    public partial class ViewContract
    {
        [Inject]
        IWebHostEnvironment _env { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Parameter]
        public int branch_id { get; set; }
        [Parameter]
        public int c_id { get; set; }
        [Parameter]
        public int promise_id { get; set; }

        [Inject]
        Manage.GlobalData globalData { get; set; }
        [Inject]
        Manage.Managements Managements { get; set; }
        private string role { get; set; } = "employee";

        //ประเภทสัญญา
        public int contract_type { get; set; } = 1;
        //หลักประกัน
        public int collateral { get; set; } = 1;
        //ประเภทหลักประกัน
        public int collateral_type { get; set; } = 1;
        //คนค้ำ
        public int guarantor { get; set; } = 1;
        //คิดภาษี
        public int vat { get; set; }


        //ยี่ห้อรถ
        public string vehicle_brand { get; set; }
        //สีรถ
        public string vehicle_color { get; set; }
        //เลขตัวถัง
        public string chassisNo { get; set; }
        //รุ่น
        public string vehicle_ver { get; set; }
        //เลขทะเบียน
        public string plate { get; set; }
        //เลขเครื่อง
        public string machineNo { get; set; }
        //รถปี
        public string vehicle_yr { get; set; }
        //จังหวัด
        public string vehicle_province { get; set; }
        //วันเสียภาษี
        public string tax_issue { get; set; }

        //ประเภทโฉนด
        public string deedType { get; set; }
        //เลขที่โฉนด
        public string deedNo { get; set; }
        //ประเภทที่ดิน
        public string landType { get; set; }
        //ระวาง
        public string mapsheet { get; set; }
        //เลขที่ดิน
        public string parcelNo { get; set; }
        //เล่ม
        public string volumn { get; set; }
        //หน้า
        public string page { get; set; }
        //หน้าสำรวจ
        public string pageInspect { get; set; }
        //จำนวนที่ดิน
        public string landAmount { get; set; }

        //เลขที่สมุด
        public string bookNo { get; set; }
        //รหัสเกษตรกร
        public string farmerCode { get; set; }
        //วันที่ขึ้นทะเบียน
        public string fregisDate { get; set; }
        //เอกสารสิทธิ์
        public string docRight { get; set; }
        //การถือครอง
        public string fHolding { get; set; }
        //กิจกรรม
        public string fActivity { get; set; }
        //เนื้อที่
        public string fArea { get; set; }
        //ผลผลิต
        public string fProduct { get; set; }
        //ที่ตั้ง X:Y:Z
        public string fLocate { get; set; }

        //เงินดาวน์
        public double downPayment { get; set; }
        //ค้างเงินดาวน์
        public double outstandingDownPm { get; set; }
        //ชำระทุกวันที่
        DateTime? paymentTerm { get; set; }
        //เงินทำสัญญา
        public double paymentContract { get; set; }
        //จำนวนงวด
        public double installmentsTerm { get; set; }
        //ดอกเบี้ย
        public double interest { get; set; }
        //งวดละ
        public double installments { get; set; }
        //ค่าบริการ
        public double serviceCharge { get; set; }
        //ค่าประกันหนี้
        public string deptInsurant { get; set; }
        //ค่านำพา
        public string carryingCost { get; set; }
        //เงินต้น
        public double principle { get; set; }
        //ค่าธรรมเนียม
        public double fee { get; set; }

        //ชื่อคนค้ำที่1
        public string guarantorNameA { get; set; }
        //ความสัมพันธ์คนค้ำที่1
        public string relationA { get; set; }
        //เบอร์โทรคนค้ำที่1
        public string phoneA { get; set; }
        //ที่อยู่คนค้ำที่1
        public string addressA { get; set; }
        //ชื่อคนค้ำที่2
        public string guarantorNameB { get; set; }
        //ความสัมพันธ์คนค้ำที่2
        public string relationB { get; set; }
        //เบอร์โทรคนค้ำที่2
        public string phoneB { get; set; }
        //ที่อยู่คนค้ำที่2
        public string addressB { get; set; }

        DateTime? date = DateTime.Now;
        private Customer _customer { get; set; }
        private Promise _promises { get; set; }

        private string filePath { get; set; }
        private string imgbase64 { get; set; }
        private List<Guarantor> _guarantor {  get; set; }
        Collateral1 collateral1 = new Collateral1();
        Collateral2 collateral2 = new Collateral2();
        Collateral3 collateral3 = new Collateral3();
        public CultureInfo GetThaiCulture()
        {
            var culture = new CultureInfo("th-TH");
            DateTimeFormatInfo formatInfo = culture.DateTimeFormat;
            formatInfo.AbbreviatedDayNames = new[] { "อา", "จ", "อ", "พ", "พฤ", "ศ", "ส" };
            formatInfo.DayNames = new[] { "วันอาทิตย์", "วันจันทร์", "วันอังคาร", "วันพุธ", "วันพฤหัสบดี", "วันศุกร์", "วันเสาร์" };
            var monthNames = new[]
            {
        "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม",""
    };
            formatInfo.AbbreviatedMonthNames = formatInfo.MonthNames = formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
            formatInfo.AMDesignator = "ก่อนเที่ยง";
            formatInfo.PMDesignator = "หลังเที่ยง";
            formatInfo.ShortDatePattern = "dd/MM/yyyy";
            formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
            formatInfo.FirstDayOfWeek = DayOfWeek.Sunday;
            return culture;
        }
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
                
        //        await JSRuntime.InvokeVoidAsync("sideBar");
        //    }
        //}
        protected override async void OnInitialized()
        {
           
            _customer = await Managements.GetCustomerInfo(c_id);
            _promises = await Managements.GetPromisebyPromiseId(promise_id);
            try
            {
                string[] subpath = _promises.UploadImg.Split('\\');
                string imagePath = Path.Combine(_env.WebRootPath, subpath[1], subpath[2], subpath[3]);
                imgbase64 = ConvertImageToBase64(imagePath);

            }
            catch (Exception ex) { }
         
            _promises.Interest = (decimal)_promises.Periodtrans.Select(s => s.Interest).FirstOrDefault();
            _guarantor = await Managements.GetGurantorbyPromiseId(promise_id);
            List<int> product_group = new List<int> { 1, 2, 4 };
            if (product_group.Contains(_promises.ProductId))
            { 
              collateral1  = JsonConvert.DeserializeObject<Collateral1>(_promises.JsonPrddesc);
            }
            if (_promises.ProductId == 3)
            { 
                collateral2 = JsonConvert.DeserializeObject<Collateral2>(_promises.JsonPrddesc);
            }
            if (_promises.ProductId == 5)
            {
                collateral3 = JsonConvert.DeserializeObject<Collateral3>(_promises.JsonPrddesc);
            }
        }
        public static string ConvertImageToBase64(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException("Image file not found", imagePath);
            }
            // Read image file into a byte array
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Convert byte array to Base64 string
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }
        private void goBack(int branch_id)
        {
            navigationManager.NavigateTo($"/customerlist/{branch_id}");
        }
    }
}
