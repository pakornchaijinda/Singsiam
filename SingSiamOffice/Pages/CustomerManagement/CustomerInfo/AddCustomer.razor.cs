using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using MudBlazor.Extensions;
using SingSiamOffice.Models;

namespace SingSiamOffice.Pages.CustomerManagement.CustomerInfo
{
    public partial class AddCustomer
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        Services.ReadNatID_Service ReadNatID_Service { get; set; }
        [Inject]
        Manage.Managements Managements { get; set; }    
       
        private string role { get; set; } = "employee";
        private string[] name_titles = {
    "นาย",
    "นาง",
    "นางสาว",
    "พลตำรวจเอก",
    "พลตำรวจเอก หญิง",
    "พลตำรวจโท",
    "พลตำรวจโท หญิง",
    "พลตำรวจตรี",
    "พลตำรวจตรี หญิง",
    "พันตำรวจเอก",
    "พันตำรวจเอก หญิง",
    "พันตำรวจเอกพิเศษ",
    "พันตำรวจเอกพิเศษ หญิง",
    "พันตำรวจโท",
    "พันตำรวจโท หญิง",
    "พันตำรวจตรี",
    "พันตำรวจตรี หญิง",
    "ร้อยตำรวจเอก",
    "ร้อยตำรวจเอก หญิง",
    "ร้อยตำรวจโท",
    "ร้อยตำรวจโท หญิง",
    "ร้อยตำรวจตรี",
    "ร้อยตำรวจตรี หญิง",
    "นายดาบตำรวจ",
    "ดาบตำรวจหญิง",
    "สิบตำรวจเอก",
    "สิบตำรวจเอก หญิง",
    "สิบตำรวจโท",
    "สิบตำรวจโท หญิง",
    "สิบตำรวจตรี",
    "สิบตำรวจตรี หญิง",
    "จ่าสิบตำรวจ",
    "จ่าสิบตำรวจ หญิง",
    "พลตำรวจ",
    "พลตำรวจ หญิง",
    "พลเอก",
    "พลเอก หญิง",
    "พลโท",
    "พลโท หญิง",
    "พลตรี",
    "พลตรี หญิง",
    "พันเอก",
    "พันเอก หญิง",
    "พันเอกพิเศษ",
    "พันเอกพิเศษ หญิง",
    "พันโท",
    "พันโท หญิง",
    "พันตรี",
    "พันตรี หญิง",
    "ร้อยเอก",
    "ร้อยเอก หญิง",
    "ร้อยโท",
    "ร้อยโท หญิง",
    "ร้อยตรี",
    "ร้อยตรี หญิง",
    "สิบเอก",
    "สิบเอก หญิง",
    "สิบโท",
    "สิบโท หญิง",
    "สิบตรี",
    "สิบตรี หญิง",
    "จ่าสิบเอก",
    "จ่าสิบเอก หญิง",
    "จ่าสิบโท",
    "จ่าสิบโท หญิง",
    "จ่าสิบตรี",
    "จ่าสิบตรี หญิง",
    "พลทหารบก",
    "ว่าที่ พ.ต.",
    "ว่าที่ พ.ต. หญิง",
    "ว่าที่ ร.อ.",
    "ว่าที่ ร.อ. หญิง",
    "ว่าที่ ร.ท.",
    "ว่าที่ ร.ท. หญิง",
    "ว่าที่ ร.ต.",
    "ว่าที่ ร.ต. หญิง",
    "พลเรือเอก",
    "พลเรือเอก หญิง",
    "พลเรือโท",
    "พลเรือโท หญิง",
    "พลเรือตรี",
    "พลเรือตรี หญิง",
    "นาวาเอก",
    "นาวาเอก หญิง",
    "นาวาเอกพิเศษ",
    "นาวาเอกพิเศษ หญิง",
    "นาวาโท",
    "นาวาโท หญิง",
    "นาวาตรี",
    "นาวาตรี หญิง",
    "เรือเอก",
    "เรือเอก หญิง",
    "เรือโท",
    "เรือโท หญิง",
    "เรือตรี",
    "เรือตรี หญิง",
    "พันจ่าเอก",
    "พันจ่าเอก หญิง",
    "พันจ่าโท",
    "พันจ่าโท หญิง",
    "พันจ่าตรี",
    "พันจ่าตรี หญิง",
    "จ่าเอก",
    "จ่าเอก หญิง",
    "จ่าโท",
    "จ่าโท หญิง",
    "จ่าตรี",
    "จ่าตรี หญิง",
    "พลทหารอากาศ",
    "หม่อม",
    "หม่อมเจ้า",
    "หม่อมราชวงศ์",
    "หม่อมหลวง",
    "ดร.",
    "ศ.ดร.",
    "ศ.",
    "ผศ.ดร.",
    "ผศ.",
    "รศ.ดร.",
    "รศ.",
    "นพ.",
    "แพทย์หญิง",
    "สัตวแพทย์",
    "สพญ.",
    "ทพ.",
    "ทพญ.",
    "เภสัชกร",
    "ภกญ.",
    "พระ",
    "พระครู",
    "พระมหา",
    "พระสมุห์",
    "พระอธิการ",
    "สามเณร",
    "แม่ชี",
    "บาทหลวง",
    "MR",
    "MRS",
    "MS",
    "MISS",
    "Dr."
};
        public string idcard { get; set; }
        public string name { get; set; }
        public string religion { get; set; } = "พุทธ";
        public string address { get; set; }
        public string idcard_issue { get; set; }
        public string idcard_exp { get; set; }
        public string phone { get; set; }
        public string job { get; set; }
        public string workaddress { get; set; }
        public string googlemaplink { get; set; }
        public string refperson { get; set; }
        public string relation { get; set; }
        public string relation_phone { get; set; }
        public string other_dept { get; set; }
        public string idcardimg { get; set; }
        private bool ck_card { get; set; } = true;

        private JsToDotNetBridge jsToDotNetBridge = new JsToDotNetBridge();
        private DotNetObjectReference<JsToDotNetBridge> jsToDotNetBridgeReference;
        private Models.ReadNatID_Info readcard_info { get; set; }

       private bool check_cus_blacklist { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }

        }

        protected override async Task<Task> OnInitializedAsync()
        {
          
            jsToDotNetBridgeReference = DotNetObjectReference.Create(jsToDotNetBridge);
            jsToDotNetBridge.setaddCustomer(this);
            JSRuntime.InvokeVoidAsync("jsToDotNetSamples.setDotNetReference", jsToDotNetBridgeReference);

            return base.OnInitializedAsync();
        }
        string date = DateTime.Now.AddYears(543).ToString("dd/MM/yyyy");
        DateTime? dob { get; set; }


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

    

        private void goBack() {
            navigationManager.NavigateTo("/customerlist/" + b_id.ToString());
        }

        public class JsToDotNetBridge 
        {
            private AddCustomer addCustomer;
            public void setaddCustomer(AddCustomer that) 
            {
                addCustomer = that;
            }
            [JSInvokable]
            public async Task ReadCardNatID(Models.ReadNatID_Info value)
            {
                await addCustomer.ReadCard(value);
            }
        }
       


        public async Task ReadNatID() 
        {
            ck_card = false;
            StateHasChanged();
            await JSRuntime.InvokeVoidAsync("readCardNatID");

        }
        public async Task ReadCard(Models.ReadNatID_Info _Info) 
        {
           
            readcard_info = _Info;
            readcard_info.fullname = readcard_info.title_th+ " "+ readcard_info.fname_th + " " + readcard_info.sname_th;
            readcard_info.address = "บ้านเลขที่ " + readcard_info.address_no + " หมู่ที่ " + readcard_info.address_moo + " ซอย " + readcard_info.address_soi + " ตำบล " + readcard_info.address_tumbol + " อำเภอ " + readcard_info.address_amphor + " จังหวัด " + readcard_info.address_provinice;
            readcard_info.issue_date = Convert.ToDateTime(readcard_info.issue_date).AddYears(543).ToString("dd/MM/yyyy");
            readcard_info.issue_expire = Convert.ToDateTime(readcard_info.issue_expire).AddYears(543).ToString("dd/MM/yyyy");
            idcardimg = "data:image/png;base64," + readcard_info.photo;


            if (readcard_info != null)
            {
                if (readcard_info.result == "read initiate failed")
                {
                    await JSRuntime.InvokeVoidAsync("idcard_notfound");
                    ck_card = true;
                }
                else
                {
                    dob = Convert.ToDateTime(readcard_info.birthdate);
                    ck_card = true;
                }


                new_cus.FullName = readcard_info.fullname;
                new_cus.Bdate = Convert.ToDateTime(readcard_info.birthdate);
                new_cus.Religion = readcard_info.national;
                new_cus.Address = readcard_info.address;
                new_cus.CardCreate = readcard_info.issue_date;
                new_cus.CardExprite = readcard_info.issue_expire;
                new_cus.NatId = readcard_info.nat_id;
                new_cus.CusImg = idcardimg;
                StateHasChanged();
            }

        }
        [Parameter]
        public int b_id { get; set; }


        SingSiamOffice.Models.SingsiamdbContext db = new SingSiamOffice.Models.SingsiamdbContext();

        Branch b_info = new Branch();

        Customer new_cus = new Customer();


        private async Task submit()
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmSaveData");
            if (confirm)
            {
                check_cus_blacklist =await Managements.Check_Customer_Backlist(new_cus.NatId);
                if (check_cus_blacklist)
                {
                    await JSRuntime.InvokeVoidAsync("alert_customer_blacklist");
                }
                else 
                {
                    if (await Save(new_cus))
                    {
                        await JSRuntime.InvokeVoidAsync("confirm");
                        await Task.Delay(100);

                        navigationManager.NavigateTo("/customerlist/" + b_id.ToString());
                    }
                    else
                    {
                        await JSRuntime.InvokeVoidAsync("alert_error");
                    }
                }
            


            }
            else
            {

            }
        }

        public async Task<bool> Save(Customer b_add)
        {

            try
            {
                b_add.BranchId = b_id;
                db.Customers.Add(b_add);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }




    }
}
