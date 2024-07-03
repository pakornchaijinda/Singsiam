using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SingSiamOffice.Pages.CustomerManagement.Payment
{
    public partial class PaymentSystem
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        private string role { get; set; } = "employee";

        bool _expanded = true;

        private void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
        }

        //ประเภทการชำระค่างวด
        public int payment_type { get; set; }
        //วิธีการชำระค่างวด
        public int payment_method { get; set; }
        //วิธีการชำระ
        public string payment_method_description { get; set; }
        //ประเภทการปิดสัญญา
        public int close_type { get; set; }

        //เลขที่ใบเสร็จ
        public int receiptNo { get; set; }
        //ยอดที่ต้องชำระ
        public double pay_amount { get; set; }
        //ยอดรับฝากเงิน
        public double deposit_amount { get; set; }
        //ยอดที่ชำระ
        public double total_paid { get; set; }
        //ค่าปรับ
        public double fine { get; set; }
        //ค่าทวงถาม
        public double demand_fee { get; set; }

        //เงินต้นคงค้าง
        public double outstanding_amount { get; set; }
        //ดอกเบี้ยคงค้าง
        public double accruedInterest { get; set; }
        //ค่าปิดบัญชี
        public double closingCost { get; set; }
        //ดอกเบี้ยที่ลดได้
        public double interest_Reduce { get; set; }
        //จำนวนงวดที่คงเหลือ
        public double installments_remain { get; set; }
        //ดอกเบี้ยเพิ่ม
        public double additional_Interest { get; set; }
        //ส่วนลดดอกเบี้ย
        public double interest_discount { get; set; }
        //ส่วนต่าง
        public double diff { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }


        private void goBack()
        {
            navigationManager.NavigateTo("/paymentlist");
        }


        private async Task submit()
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmPayment");
            if (confirm)
            {

                await JSRuntime.InvokeVoidAsync("paymentsuccess");
                await Task.Delay(100);

                navigationManager.NavigateTo("/paymentrecipe");

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("paymenterror");
            }
        }
    }
}
