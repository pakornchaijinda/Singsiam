using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SingSiamOffice.Pages.EmployeeManagement
{
    public partial class ViewEmployee
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        private string role { get; set; } = "admin";
        public string employeeId { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; } = true;

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

        private string[] positions = {
    "แอดมิน",
    "เจ้าหน้าที่"
};
        private string branchs;
        private string[] branchlists =
        {
        "1001 | สาขาเชียงใหม่", "1002 | สาขาลำพูน", "1003 | สาขาดอนเมือง", "1004 | สาขาตลาดไทย",


    };

        private async Task<IEnumerable<string>> SearchBranch(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return branchlists;
            return branchlists.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
         
            }
        }


        private void goBack()
        {
            navigationManager.NavigateTo("/employee-list");
        }
   
    }
}
