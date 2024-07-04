using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SingSiamOffice.Manage;
using SingSiamOffice.Models;

namespace SingSiamOffice.Pages.CustomerManagement.Payment
{
    public partial class PaymentRecipe
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Parameter]
        public int peroidtran_id { get; set; }
        [Inject]
        Manage.Managements Manage { get; set; }
        [Inject]
        Helpers.NumberToText GetNumberToText { get; set; }
        [Inject]
        Manage.GlobalData globalData { get; set; }

        private List<Receiptdesc> lst_receiptdescs = new List<Receiptdesc>();
        private Receipttran receipttran = new Receipttran();
        Manage.Receipt receipt = new Receipt();
        private bool ck_deposit { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("printReceipt");
            }
        }
        protected override async void OnInitialized()
        {
            lst_receiptdescs = await Manage.GetReceipttran(peroidtran_id);
            receipttran = lst_receiptdescs.Select(s => s.Receipttran).FirstOrDefault();
            receipt.daypaid = receipttran.Tdate;
            receipt.receipt_no = receipttran.Receiptno;
            receipt.promise_no = lst_receiptdescs.FirstOrDefault().Promise.Refcode;
            receipt.fullname = lst_receiptdescs.FirstOrDefault().Customer.FullName;
            receipt.address = lst_receiptdescs.FirstOrDefault().Customer.Address;
            receipt.product = lst_receiptdescs.FirstOrDefault().Promise.Product.Name;
            receipt.paid_by = await GetNumberToText.Paid_By((int)receipttran.PaidBy);
            receipt.total_amount = receipttran.Amount.ToString();
            receipt.amount_text = await GetNumberToText.ConvertNumberToThaiWords(Convert.ToInt32(receipttran.Amount));
            receipt.peroid_remain = receipttran.Periodremain.ToString();
            receipt.total_fee = receipttran.Charge1amt.ToString();
            receipt.receive_by = globalData.fullname;
            receipt.deposit = receipttran.Deposit.ToString();
            if (receipttran.Receiptdesc == "รับฝากเงินล่วงหน้า")
            {
                ck_deposit = true; 
            }
            else 
            {
                ck_deposit = false;
            }
            if (receipttran.Charge1amt > 0)
            {
                receipt.ck_total_fee = true;
            }
        }








    }
}
