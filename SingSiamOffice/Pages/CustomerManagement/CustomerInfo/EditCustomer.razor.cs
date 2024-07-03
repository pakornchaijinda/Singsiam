using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SingSiamOffice.Models;
using System.Globalization;

namespace SingSiamOffice.Pages.CustomerManagement.CustomerInfo
{
    public partial class EditCustomer
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }


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
        public string religion { get; set; }
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


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
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
        async Task<bool> save()
        {
            try
            {

                db.Entry(edit_cuc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async Task submit()
        {
            
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmSaveData");
            if (confirm)
            {
                 edit_cuc.Bdate = dob.Value;
              
                if (await save())
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
            else
            {
                //await JSRuntime.InvokeVoidAsync("alert_error");
            }
        }

        private void goBack()
        {
            navigationManager.NavigateTo("/customerlist/"+b_id.ToString());
        }


    }
}
