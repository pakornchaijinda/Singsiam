using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SingSiamOffice.Models;
namespace SingSiamOffice.Pages.BranchManagement
{
    public partial class AddBranch
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        
        SingsiamdbContext db = new SingsiamdbContext();
        private string role { get; set; } = "admin";

        public string branch_name { get; set; }
        public string branch_code { get; set; }
        public string branch_account { get; set; }
        public string branch_bank { get; set; }
        public string branch_tel { get; set; }
        public string branch_description { get; set; }
        public string branch_map { get; set; }
        public string branch_address { get; set; }

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
        

        private string value1;
        private string[] states =
        {
        "Alabama", "Alaska", "American Samoa", "Arizona",
        "Arkansas", "California", "Colorado", "Connecticut",

    };

        private async Task<IEnumerable<string>> SearchValue(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return states;
            return states.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
              
            }
        }

        public async Task<bool> Save(Branch b_add)
        {

            try
            {
                b_add.CreateAt = DateTime.Now;
                db.Branches.Add(b_add);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        private async Task submit()
        {
         
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmSaveData");
            if (confirm)
            {
                if (await Save(new_b))
                {
                    await JSRuntime.InvokeVoidAsync("confirm");
                    await Task.Delay(100);

                    navigationManager.NavigateTo("/branchManagement-list");
                }
               

            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alert_error");
            }
        }

        private void goback()
        {
            navigationManager.NavigateTo("/branchManagement-list");
        }
    }
}
