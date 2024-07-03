using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SingSiamOffice.Pages.BranchManagement
{
    public partial class BranchManagementList
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        private string role { get; set; } = "admin";

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

        bool isOpen;

        bool _expanded = true;

        private void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
        }



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
        

        private void AddBranch()
        {
            navigationManager.NavigateTo("/add-branch");
        }
        private void EditBranch(int id)
        {
            navigationManager.NavigateTo("/edit-branch/"+ id.ToString());
        }
        private async Task delete(int id)
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmdelete");
            if (confirm)
            {
                try
                {
                    var to_remove = db.Branches.Where(s => s.Id == id).FirstOrDefault();
                    db.Branches.Remove(to_remove);
                    await db.SaveChangesAsync();
                    await JSRuntime.InvokeVoidAsync("deletesuccess");
                    await Task.Delay(100);
                    list_b = db.Branches.ToList();

                }
                catch
                {
                    await JSRuntime.InvokeVoidAsync("delete_error");
                }
                

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("delete_error");
            }
        }

        private void viewBranchInfo(int id)
        {
            navigationManager.NavigateTo("/branch-info/"+id.ToString());
        }

    }
}
