using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;
using SingSiamOffice.Helpers;
using SingSiamOffice.Manage;
using SingSiamOffice.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
using static SingSiamOffice.Manage.GetData;

namespace SingSiamOffice.Pages.Contracts
{
    public partial class AddContract
    {
        [Inject]
        IWebHostEnvironment _env { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Parameter]
        public int c_id { get; set; }
        [Parameter]
        public int branch_id { get; set; }
        [Inject]
        Manage.Managements Managements { get; set; }
        [Inject]
        Helpers.NumberToText Helpers { get; set; }
        [Inject]
        Manage.PromiseManagement promiseManagement { get; set; }
        [Inject]
        Manage.GlobalData globalData { get; set; }

        [Inject]
        Helpers.calamount c { get; set; }
        bool addGuarantor { get; set; } = false;

        Province _select_province = null;
        Province select_province
        {
            get { return _select_province; }
            set
            {
                if (value != null)
                {
                    _select_province = value;

                }
                else
                {

                }


            }
        }

        Collateral _select_collateral = null;
        Collateral select_collateral
        {
            get { return _select_collateral; }
            set
            {
                if (value != null)
                {
                    _select_collateral = value;
                    PromiseInfo.ProductId = _select_collateral.Id;
                    GetRefCode();
                    StateHasChanged();
                }
                else
                {

                }

            }

        }


        #region Parameter

        private string role { get; set; } = "employee";

        //ประเภทสัญญา
        public int contract_type { get; set; }
        //หลักประกัน
        public int collateral { get; set; }
        //ประเภทหลักประกัน
        public int collateral_type { get; set; }
        //คนค้ำ
        public int guarantor { get; set; }
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
        //เลช บปช
        public string guarantorANatId { get; set; }
        //ความสัมพันธ์คนค้ำที่1
        public string relationA { get; set; }
        //เบอร์โทรคนค้ำที่1
        public string phoneA { get; set; }
        //ที่อยู่คนค้ำที่1
        public string addressA { get; set; }
        //ชื่อคนค้ำที่2
        public string guarantorNameB { get; set; }
        //เลช บปช2
        public string guarantorBNatId { get; set; }
        //ความสัมพันธ์คนค้ำที่2
        public string relationB { get; set; }
        //เบอร์โทรคนค้ำที่2
        public string phoneB { get; set; }
        //ที่อยู่คนค้ำที่2
        public string addressB { get; set; }

        private string[] provincelist = {
            "กรุงเทพมหานคร",
            "กระบี่",
            "กาญจนบุรี",
            "กาฬสินธุ์",
            "กำแพงเพชร",
            "ขอนแก่น",
            "จันทบุรี",
            "ฉะเชิงเทรา",
            "ชัยนาท",
            "ชัยภูมิ",
            "ชุมพร",
            "ชลบุรี",
            "เชียงใหม่",
            "เชียงราย",
            "ตรัง",
            "ตราด",
            "ตาก",
            "นครนายก",
            "นครปฐม",
            "นครพนม",
            "นครราชสีมา",
            "นครศรีธรรมราช",
            "นครสวรรค์",
            "นราธิวาส",
            "น่าน",
            "นนทบุรี",
            "บึงกาฬ",
            "บุรีรัมย์",
            "ประจวบคีรีขันธ์",
            "ปทุมธานี",
            "ปราจีนบุรี",
            "ปัตตานี",
            "พะเยา",
            "พระนครศรีอยุธยา",
            "พังงา",
            "พิจิตร",
            "พิษณุโลก",
            "เพชรบุรี",
            "เพชรบูรณ์",
            "แพร่",
            "พัทลุง",
            "ภูเก็ต",
            "มหาสารคาม",
            "มุกดาหาร",
            "แม่ฮ่องสอน",
            "ยโสธร",
            "ยะลา",
            "ร้อยเอ็ด",
            "ระนอง",
            "ระยอง",
            "ราชบุรี",
            "ลพบุรี",
            "ลำปาง",
            "ลำพูน",
            "เลย",
            "ศรีสะเกษ",
            "สกลนคร",
            "สงขลา",
            "สมุทรสาคร",
            "สมุทรปราการ",
            "สมุทรสงคราม",
            "สระแก้ว",
            "สระบุรี",
            "สิงห์บุรี",
            "สุโขทัย",
            "สุพรรณบุรี",
            "สุราษฎร์ธานี",
            "สุรินทร์",
            "สตูล",
            "หนองคาย",
            "หนองบัวลำภู",
            "อำนาจเจริญ",
            "อุดรธานี",
            "อุตรดิตถ์",
            "อุทัยธานี",
            "อุบลราชธานี",
            "อ่างทอง",
            "อื่นๆ"

};

        private string username { get;set; }

        #endregion


        #region UploadIMG
        private List<string> fileNames = new List<string>();
        private const int MaxImageUploadSizeMB = 1;
        private const int MaxImageUploadSize = MaxImageUploadSizeMB * 1000000; //in bytes
        private ImageUploadFormModel _formModel = new ImageUploadFormModel();
        public class ImageUploadFormModel
        {
            public IBrowserFile ImageFile { get; set; }
            public string PreviewUrl { get; set; } = null;

            [Required]
            public byte[] ImageFileData { get; set; }
        }

        public IBrowserFile ImageFile { get; set; }
        public byte[] ImageFileData { get; set; }
        public string filePath;
        private async Task UploadFiles(IBrowserFile file)
        {

            fileNames.Add(file.Name);


            if (file is null)
                return;

            if (file.Size > MaxImageUploadSize)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
                Snackbar.Add($"please don't exceed {MaxImageUploadSize / 1000000} MB", Severity.Error);
                return;
            }

            _formModel.ImageFile = await file.RequestImageFileAsync("image/jpeg", 400, 400);
            if (_formModel.ImageFile is null)
                return;

            await using var imageStream = _formModel.ImageFile.OpenReadStream();
            _formModel.ImageFileData = new byte[_formModel.ImageFile.Size];
            await imageStream.ReadAsync(_formModel.ImageFileData);

            _formModel.PreviewUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(_formModel.ImageFileData)}";

            // Create a unique folder name based on current date and time
            var folderName = PromiseInfo.Promiseno;


            // Create the folder inside the wwwroot/Uploads directory
            var folderPath = Path.Combine("UploadsCollateral", folderName);


            var folderDirectory = Path.Combine(_env.WebRootPath, folderPath);
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }

            folderName = folderName + '_' + _customer.NatId;
            // Generate a unique file name for the uploaded image
            var fileName = $"{folderName}{Path.GetExtension(file.Name)}";

            // Combine the folder path and file name to get the full path of the image
            var file_Path = Path.Combine(folderDirectory, fileName);

            using (var stream = new FileStream(file_Path, FileMode.Create))
            {
                await file.OpenReadStream().CopyToAsync(stream);
            }

            int index = file_Path.IndexOf("wwwroot");

            if (index != -1)
            {
                // Split the string based on the search text
                string[] parts = file_Path.Split(new[] { "wwwroot" }, StringSplitOptions.None);

                filePath = parts[1];
                PromiseInfo.UploadImg = parts[1];
            }


            // Save the uploaded image to the specified path

            StateHasChanged();
        }
        //private async void OnInputFileChanged(InputFileChangeEventArgs e)
        //{

        //    if (e?.File is null)
        //        return;

        //    ImageFile = await e.File.RequestImageFileAsync("image/jpeg", 400, 400);
        //    if (ImageFile is null)
        //        return;

        //    await using var imageStream = ImageFile.OpenReadStream();
        //    ImageFileData = new byte[ImageFile.Size];
        //    await imageStream.ReadAsync(ImageFileData);



        //    // Create a unique folder name based on current date and time
        //    var folderName = PromiseInfo.Promiseno;

        //    var sub_folderName = _customer.NatId + "_" + folderName;
        //    // Create the folder inside the wwwroot/Uploads directory
        //    var folderPath = Path.Combine("UploadsCollateral", folderName);


        //    var folderDirectory = Path.Combine(_env.WebRootPath, folderPath);
        //    if (!Directory.Exists(folderDirectory))
        //    {
        //        Directory.CreateDirectory(folderDirectory);
        //    }

        //    // Generate a unique file name for the uploaded image
        //    var fileName = sub_folderName + Path.GetExtension(e.File.Name);


        //    // Combine the folder path and file name to get the full path of the image
        //    var file_Path = Path.Combine(folderDirectory, fileName);


        //    using (var stream = new FileStream(file_Path, FileMode.Create))
        //    {
        //        await e.File.OpenReadStream().CopyToAsync(stream);
        //    }

        //    int index = file_Path.IndexOf("wwwroot");

        //    if (index != -1)
        //    {
        //        // Split the string based on the search text

        //        string[] parts = file_Path.Split(new[] { "wwwroot" }, StringSplitOptions.None);


        //        //filePath = parts[2];
        //        PromiseInfo.UploadImg = parts[1];
        //    }
        //    // Save the uploaded image to the specified path


        //    StateHasChanged();
        //}
     


        #endregion

        private List<Province> List_Provinces {  get; set; }
        private List<Collateral> List_Collaterals { get; set; }

        private List<Guarantor> List_Guarantors = new List<Guarantor>();

        private Promise PromiseInfo = new Promise();
        private Customer _customer { get; set; }
        private Branch branch = new Branch();
        List<Customer> list_customer = new List<Customer>();

        Collateral1 collateral1 = new Collateral1();
        Collateral2 collateral2 = new Collateral2();
        Collateral3 collateral3 = new Collateral3();

     
        private async Task<IEnumerable<Province>> SearchProvince(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return List_Provinces;
            return List_Provinces.Where(x => x.ProvinceName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
        private async Task<IEnumerable<Collateral>> SearchCollateral(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return List_Collaterals;
            return List_Collaterals.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }


        #region Method Action
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }
        protected override async void OnInitialized()
        {
            list_customer = await Managements.GetCustomerbyBranch(branch_id);
            list_customer = list_customer.Where(s => s.CustomerId != c_id).ToList();
            List_Provinces = await Managements.GetProvince();
            List_Collaterals = await Managements.GetCollaterals();
            _customer = await Managements.GetCustomerInfo(c_id);
            var promiseNo = await Managements.Get_Promise_No(branch_id, "promiseno");
            PromiseInfo.Promiseno = promiseNo.ToString();
            branch = await Managements.GetBranches(branch_id);

            username = globalData.username;
          
        }
        string date = DateTime.Now.AddYears(543).ToString("dd/MM/yyyy");

        public async Task AddGuanrantor() 
        {
            addGuarantor = true;
            StateHasChanged();
        }
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

        private async void TextChanged() 
        {
             c = new calamount();
            double paycapital = 0;
            if (interest.ToString().Length >= 3)
            {
                if (contract_type == 1)
                {
                    paycapital = paymentContract;
                }
                else
                {
                    paycapital = principle;
                }
                c = await Helpers.cal_amonut(Convert.ToDecimal(paycapital), Convert.ToInt32(installmentsTerm), Convert.ToDecimal(interest), select_collateral.Id);
            }
        }
        private async void InterateTextChanged()
        {
            if (serviceCharge.ToString().Length > 0)
            {
               var total = c.total_amount + Convert.ToDecimal(serviceCharge);
                c.total_amount = total;
                c.total_interate_service = c.interate + Convert.ToDecimal(serviceCharge);
            }
        }
  
        private async void GetRefCode() 
        {
            var RefCode = await Managements.Get_Ref_Code(branch_id, "refcode", branch.BranchCode, select_collateral.Id);
            PromiseInfo.Refcode = RefCode;
        }
        private async Task submit()
        {

            CultureInfo thaiCulture = new CultureInfo("th-TH");
            thaiCulture.DateTimeFormat.Calendar = new ThaiBuddhistCalendar();
            if (select_collateral.Id == 1) 
            {
                PromiseInfo.Prddesc = vehicle_brand + "/" + vehicle_color + "/" + chassisNo + "/" + vehicle_ver + "/" + plate + "/" + machineNo + "/" + vehicle_yr + "/" + tax_issue;
                collateral1.vehicle_brand = vehicle_brand;
                collateral1.vehicle_color = vehicle_color;
                collateral1.chassisNo = chassisNo;
                collateral1.vehicle_ver = vehicle_ver;
                collateral1.plate = plate;  
                collateral1.machineNo = machineNo;
                collateral1.vehicle_yr = vehicle_yr;
                collateral1.tax_issue = tax_issue;
                string json_data = Newtonsoft.Json.JsonConvert.SerializeObject(collateral1);
                PromiseInfo.JsonPrddesc = json_data;
            }
            if (select_collateral.Id == 2)
            {
                PromiseInfo.Prddesc = vehicle_brand + "/" + vehicle_color + "/" + chassisNo + "/" + vehicle_ver + "/" + plate + "/" + machineNo + "/" + vehicle_yr + "/" + tax_issue;
                collateral1.vehicle_brand = vehicle_brand;
                collateral1.vehicle_color = vehicle_color;
                collateral1.chassisNo = chassisNo;
                collateral1.vehicle_ver = vehicle_ver;
                collateral1.plate = plate;
                collateral1.machineNo = machineNo;
                collateral1.vehicle_yr = vehicle_yr;
                collateral1.tax_issue = tax_issue;
                string json_data = Newtonsoft.Json.JsonConvert.SerializeObject(collateral1);
                PromiseInfo.JsonPrddesc = json_data;
            }
            if (select_collateral.Id == 3)
            {
                PromiseInfo.Prddesc = deedType + "/" + deedNo + "/" + landType + "/" + mapsheet + "/" + parcelNo + "/" + volumn + "/" + page + "/" + pageInspect + "/"+ landAmount;
                collateral2.deedType = deedType;
                collateral2.deedNo = deedNo;    
                collateral2.landType = landType;
                collateral2.mapsheet = mapsheet;
                collateral2.parcelNo = parcelNo;
                collateral2.volumn = volumn;    
                collateral2.page = page;
                collateral2.pageInspect = pageInspect;
                collateral2.landAmount = landAmount;
                string json_data2 = Newtonsoft.Json.JsonConvert.SerializeObject(collateral2);
                PromiseInfo.JsonPrddesc = json_data2;
            }
            if (select_collateral.Id == 4)
            {
                PromiseInfo.Prddesc = vehicle_brand + "/" + vehicle_color + "/" + chassisNo + "/" + vehicle_ver + "/" + plate + "/" + machineNo + "/" + vehicle_yr + "/" + tax_issue;
                collateral1.vehicle_brand = vehicle_brand;
                collateral1.vehicle_color = vehicle_color;
                collateral1.chassisNo = chassisNo;
                collateral1.vehicle_ver = vehicle_ver;
                collateral1.plate = plate;
                collateral1.machineNo = machineNo;
                collateral1.vehicle_yr = vehicle_yr;
                collateral1.tax_issue = tax_issue;
                string json_data = Newtonsoft.Json.JsonConvert.SerializeObject(collateral1);
                PromiseInfo.JsonPrddesc = json_data;
            }
            if (select_collateral.Id == 5)
            {
                PromiseInfo.Prddesc = bookNo + "/" + farmerCode + "/" + fregisDate + "/" + docRight + "/" + fHolding + "/" + fActivity + "/" + fArea + "/" + fProduct +"/" + fLocate;
                collateral3.bookNo = bookNo;
                collateral3.farmerCode = farmerCode;
                collateral3.fregisDate = fregisDate;
                collateral3.docRight = docRight;
                collateral3.fHolding = fHolding;
                collateral3.fActivity = fActivity;
                collateral3.fArea = fArea;
                collateral3.fProduct = fProduct;    
                collateral3.fLocate = fLocate;
                string json_data3 = Newtonsoft.Json.JsonConvert.SerializeObject(collateral3);
                PromiseInfo.JsonPrddesc = json_data3;
            }
            try 
            {
                PromiseInfo.ProvinceId = select_province.Id;
            }
            catch(Exception ex){  }
          
            PromiseInfo.Daypaid = paymentTerm.Value.Date.ToString();
            PromiseInfo.Firstdate = paymentTerm.Value.ToString("yyyy-MM-dd");
            PromiseInfo.Tdatetime = DateTime.Now;
            PromiseInfo.Tdate = DateTime.Now.ToString("yyyy/MM/dd",thaiCulture);
            PromiseInfo.Tdateformat = DateTime.Now.ToString("yyyyMMdd");
            PromiseInfo.CustomerId = c_id;
            PromiseInfo.Ptype = contract_type;
            PromiseInfo.Chargeamt = c.chargement;

          
            PromiseInfo.Intrate =Convert.ToDecimal(interest);
            PromiseInfo.Service =Convert.ToDecimal(serviceCharge);
            PromiseInfo.Periods = Convert.ToInt32(installmentsTerm);
            if (contract_type == 1)
            {
                PromiseInfo.Amount = c.total_amount;
                PromiseInfo.Capital = Convert.ToDecimal(paymentContract);
            }
            if (contract_type == 2)
            {
                PromiseInfo.Amount = c.amount_Ptype2;
                PromiseInfo.Capital = Convert.ToDecimal(principle);
            }
            PromiseInfo.Daypaid = paymentTerm.Value.Day.ToString();
            PromiseInfo.Firstdate = paymentTerm.Value.ToString("dd/MM/yyyy",thaiCulture);
            PromiseInfo.FirstDatePay = paymentTerm.Value;
           
            PromiseInfo.Status = 0;
            PromiseInfo.Insurance1 = guarantorNameA;
            PromiseInfo.Insurance2 = guarantorNameB;
            PromiseInfo.Insurance1relation  = relationA + "/" +phoneA + "/"+addressA;
            PromiseInfo.Insurance2relation = relationB + "/" +phoneB + "/"+addressB;
            PromiseInfo.BranchId = branch_id;
            PromiseInfo.Usercode = username;
            PromiseInfo.Person1 = username;
            PromiseInfo.Interest_Service = c.total_interate_service;
            PromiseInfo.CapitalCal = c.capital;
            PromiseInfo.ContractType = contract_type;
            if (guarantor == 1)
            {
                PromiseInfo.Guarantor = true;
            }
            if(guarantor == 2) 
            {
                PromiseInfo.Guarantor = false;
            }

            var confirm = await JSRuntime.InvokeAsync<bool>("confirmSaveData");
            if (confirm)
            {

                var b = await promiseManagement.addPromise(PromiseInfo);
                if(guarantor == 1) 
                {
                    Models.Guarantor g = new Guarantor();
                    g.GuarantorName = guarantorNameA;
                    g.GuarantorNatId = guarantorANatId;
                    g.GuarantorRelation = relationA == null ? "-" : relationA;
                    g.Phone = phoneA == null ? "-" : phoneA; ;
                    g.Address = addressA == null ? "-" : addressA;
                    g.PromiseId = b.Id;
                    List_Guarantors.Add(g);

                    if (addGuarantor == true)
                    {
                        Guarantor gg = new Guarantor();

                        gg.GuarantorName = guarantorNameB;
                        gg.GuarantorNatId = guarantorBNatId;
                        gg.GuarantorRelation = relationB == null ? "-" : relationB;
                        gg.Phone = phoneB == null ? "-" : phoneB; ;
                        gg.Address = addressB == null ? "-" : addressB;
                        gg.PromiseId = b.Id;
                        List_Guarantors.Add(gg);

                    }
                    await promiseManagement.addGuarantor(List_Guarantors);
                }
                var periodtran = await Managements.Add_Periodtrans(b, contract_type);
                var ck_save = await promiseManagement.addPeriodtran(periodtran);

                if (ck_save)
                {
                    await JSRuntime.InvokeVoidAsync("confirm");

                    await Task.Delay(100);

                    navigationManager.NavigateTo($"/customerlist/{branch_id}");
                }

              

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("alert_error");
            }
        }
        private void goBack()
        {
            navigationManager.NavigateTo("/customerlist/" + branch_id.ToString());
        }

        Models.Customer selectCustomer = null;
        Models.Customer _selectCustomer
        {
            get { return _selectCustomer; }
            set
            {
                if (value != null)
                {
                    _selectCustomer = value;

                    guarantorNameA = _selectCustomer.FullName;
                    guarantorANatId = _selectCustomer.NatId;
                    phoneA = _selectCustomer.Phone;
                    addressA = _selectCustomer.Address;
                    StateHasChanged();
                }
                else
                {

                }
            }
            
        }
        private async Task<IEnumerable<Customer>> SearchValue(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return list_customer;
            return list_customer.Where(x => x.NatId.Contains(value, StringComparison.InvariantCultureIgnoreCase) || x.FullName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
        #endregion
    }
}
