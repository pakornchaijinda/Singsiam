using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using MudBlazor;
using SingSiamOffice.Manage;
using SingSiamOffice.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace SingSiamOffice.Pages.CustomerManagement.Payment
{
    public partial class PaymentList
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
        Manage.Managements managements { get; set; }

        [Inject]
        Manage.GlobalData globalData { get; set; }
        [Inject]
        Manage.PromiseManagement promiseManagement { get; set; }
        public bool paid_status { get; set; } = true;


      

        //payment method
        //1 เงินสด 2 เงินโอน 3 อื่นๆ 
        // 2,3 มีหลักฐานการชำระ

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
            var folderName = _promise.Promiseno;


            // Create the folder inside the wwwroot/Uploads directory
            var folderPath = Path.Combine("UploadsSlip", folderName);


            var folderDirectory = Path.Combine(_env.WebRootPath, folderPath);
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }

            folderName = folderName + '_' + _promise.Customer.NatId + ReceiptNo;
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
               // PromiseInfo.UploadImg = parts[1];
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


        public int payment_method { get; set; } = 1;

        private string role { get; set; } = "employee";
        public bool pay_check { get; set; }

        public decimal deposotamount { get; set; }

        bool _expanded = true;
        private decimal totalFee { get; set; }
        private void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
        }
        private int? current_periods { get; set; }
        private List<Receiptdesc> lst_receiptdescs = new List<Receiptdesc>();
        private Receiptdesc _receiptdesc_toAdd { get; set; }
        private List<Periodtran> _periodtran = new List<Periodtran>();
        private Promise _promise = new Promise();
        private Periodtran _promise_pay = new Periodtran();
        private string ReceiptNo { get; set; } = "";
        int activeIndex = 0;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }
        protected override async void OnInitialized()
        {
            _periodtran =await managements.GetPeriodtransbyPromiseId(promise_id);
            _promise = await managements.GetPromisebyPromiseId(promise_id);
            _promise_pay.OverPayQty = _periodtran.Where(s=>s.OverPayQty >0).Count();
          
            _promise_pay.total_deposit = _periodtran.Sum(p => p.total_deposit);
            _promise_pay.total_fee = _periodtran.Sum(p => p.total_fee);
            totalFee = _promise_pay.total_fee;
            _promise_pay.total_charge_follow = _periodtran.Sum(p => p.total_charge_follow);

            if (_promise_pay.OverPayQty > 0)
            {
                _promise_pay.total_deptAmount = _periodtran.Sum(p => p.amount_remain);
                _promise_pay.pending_amount = _promise_pay.total_deptAmount - _promise_pay.total_deposit + _promise_pay.total_fee + _promise_pay.total_charge_follow;
            }
            else 
            {
                _promise_pay.total_deptAmount = 0;
                _promise_pay.pending_amount = 0;
            }
            totalFee = _promise_pay.total_fee;
            var receipt = await managements.Get_Receipt_No(branch_id, "receipt");
            ReceiptNo = receipt.ToString();
        }
        private void ValueChanged(ChangeEventArgs args)
        {
           var total_fee_change  = Convert.ToInt32((String)args.Value);
            totalFee = total_fee_change;
            _promise_pay.total_deptAmount = _periodtran.Sum(p => p.amount_remain) + total_fee_change;
            
        }
        void SetTab(int index)
        {
            activeIndex = index;

            if (activeIndex == 0)
            {
                paidprincipleAmount = 0;
                paidinterestAmount = 0;
                _promise_pay.OverPayQty = _periodtran.Where(s => s.OverPayQty > 0).Count();

                _promise_pay.total_deposit = _periodtran.Sum(p => p.total_deposit);
                _promise_pay.total_fee = _periodtran.Sum(p => p.total_fee);
                _promise_pay.total_charge_follow = _periodtran.Sum(p => p.total_charge_follow);

                if (_promise_pay.OverPayQty > 0)
                {
                    _promise_pay.total_deptAmount = _periodtran.Sum(p => p.amount_remain);
                    _promise_pay.pending_amount = _promise_pay.total_deptAmount - _promise_pay.total_deposit + _promise_pay.total_fee + _promise_pay.total_charge_follow;
                }
                else
                {
                    _promise_pay.total_deptAmount = 0;
                    _promise_pay.pending_amount = 0;
                }
            }
            else if (activeIndex == 1)
            {
                _promise_pay.OverPayQty = _periodtran.Where(s => s.Ispaid == false).Count();

                _promise_pay.total_deposit = _periodtran.Sum(p => p.total_deposit);
                _promise_pay.total_fee = _periodtran.Sum(p => p.total_fee);
                _promise_pay.total_charge_follow = _periodtran.Sum(p => p.total_charge_follow);

              
                _promise_pay.total_deptAmount = _periodtran.Sum(p => (decimal)p.Amount);
                _promise_pay.pending_amount = _promise_pay.total_deptAmount - _promise_pay.total_deposit + _promise_pay.total_fee + _promise_pay.total_charge_follow;
                paidprincipleAmount = _periodtran.Sum(p => (decimal)p.Capital);
                paidinterestAmount = _periodtran.Sum(p => (decimal)p.Interest);
            }
            StateHasChanged();
        }

  
        private void CalculateStandardDimensions(ChangeEventArgs args)
        {
            var amount = args.Value.ToString();
            if (amount == "")
            {
                amount = "0";
            }
           totalFee = Convert.ToDecimal(amount);
            _promise_pay.total_deptAmount = _periodtran.Sum(p => p.amount_remain) + totalFee;
            _promise_pay.pending_amount = _promise_pay.total_deptAmount - _promise_pay.total_deposit;
        }
        private async Task delete()
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmdelete");
            if (confirm)
            {
                await JSRuntime.InvokeVoidAsync("deletesuccess");
                await Task.Delay(100);

                navigationManager.NavigateTo("/paymentlist");

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("delete_error");
            }
        }

        private async Task submitpaymentlist()
        {

            if (customerPayAmount != null)
            {
                var confirm = await JSRuntime.InvokeAsync<bool>("confirmPayment");
                if (confirm)
                {



                    if ((decimal)customerPayAmount >= _promise_pay.pending_amount)
                    {

                        decimal cal_period_pay = ((decimal)customerPayAmount - _promise_pay.total_fee) / (decimal)_promise.Amount;
                        var period_pay_qty = (int)Math.Ceiling(cal_period_pay);
                        int remainingInstallments = (int)_periodtran.Where(s => s.Ispaid == false).Count() - (int)Math.Floor(cal_period_pay);

                        Receipttran _receipttran_toAdd = new Receipttran();

                        _receipttran_toAdd.PromiseId = _promise.Id;
                        _receipttran_toAdd.BranchId = _promise.BranchId;
                        _receipttran_toAdd.CustomerId = _promise.CustomerId;
                        _receipttran_toAdd.Receiptno = ReceiptNo;

                        if (payment_method == 1 || payment_method == 2 || payment_method == 3)
                        {
                            _receipttran_toAdd.Amount = (decimal)customerPayAmount;
                            _receipttran_toAdd.Receiptdesc = "ชำระค่างวด";
                        }
                        if (payment_method == 4)
                        {
                            _receipttran_toAdd.Amount = (decimal)customerPayAmount;
                            _receipttran_toAdd.Receiptdesc = "รับฝากเงินล่วงหน้า";
                        }

                        _receipttran_toAdd.Tdate = DateTime.Now.AddYears(543).ToString("dd/MM/yyyy");
                        _receipttran_toAdd.Tdateformat = DateTime.Now.ToString("yyyyMMdd");
                        _receipttran_toAdd.Tdatecal = DateTime.Now.AddYears(543).ToString("dd/MM/yyyy");
                        _receipttran_toAdd.Tdatecalformat = DateTime.Now.ToString("yyyyMMdd");
                        _receipttran_toAdd.peroidtrans_info = _periodtran.Where(s => s.Ispaid == false).Take(period_pay_qty).ToList();


                        if (payment_method != 4)
                        {
                            _receipttran_toAdd.Arbalance = _periodtran.Where(s => s.Ispaid == false).FirstOrDefault().Amount;
                            _receipttran_toAdd.Cappaid = _periodtran.Where(s => s.Ispaid == false).FirstOrDefault().Capital;

                            var calpay_remain = (decimal)customerPayAmount - _receipttran_toAdd.Arbalance;
                            //      _receipttran_toAdd.Inspaid = _periodtran.Where(s => s.Ispaid == false).FirstOrDefault().Interest + calpay_remain;
                            _receipttran_toAdd.Intpaid = _periodtran.Where(s => s.Ispaid == false).FirstOrDefault().Interest + calpay_remain;
                            _receipttran_toAdd.Capremain = _promise.Capital - _receipttran_toAdd.Cappaid;
                            var sum_interate = _periodtran.Where(s => s.Ispaid == false).Sum(s => s.Interest);
                            _receipttran_toAdd.Intremain = sum_interate - _receipttran_toAdd.Inspaid;
                            _receipttran_toAdd.Charge1amt = totalFee;
                            _receipttran_toAdd.Charge2amt = _promise_pay.total_charge_follow;
                            var count_periodtran = _periodtran.Where(s => s.Ispaid == false).Count();
                            if (count_periodtran == _promise.Periods)
                            {
                                _receipttran_toAdd.Periodremain = _promise.Periods - 1;

                            }
                            else
                            {
                                _receipttran_toAdd.Periodremain = count_periodtran;
                            }
                        }
                        else 
                        {
                            current_periods = await managements.GetCurrentPeriod(promise_id);
                            _receipttran_toAdd.Periodchg = current_periods;
                            _receipttran_toAdd.Periodremain = _promise.Periods - 1;
                        }




                        _receipttran_toAdd.Usercode = globalData.fullname;

                        if (payment_method == 1)
                        {
                            _receipttran_toAdd.Cashpaid = 1;
                            _receipttran_toAdd.PaidBy = 1;
                        }
                        if (payment_method == 2)
                        {
                            _receipttran_toAdd.Transferdate = "1";
                            _receipttran_toAdd.PaidBy = 2;
                        }
                        if (payment_method == 3)
                        {
                            _receipttran_toAdd.Otherpaid = 1;
                            _receipttran_toAdd.PaidBy = 3;
                        }
                        if (payment_method == 4)
                        {
                            _receipttran_toAdd.Otherpaid = 1;
                            _receipttran_toAdd.PaidBy = 4;
                        }

                        _receipttran_toAdd.Currentperiod = _periodtran.Where(s => s.Ispaid == false).Select(s => s.Period).FirstOrDefault();
                      //  _receipttran_toAdd.RemainingPrincipal = ((decimal)customerPayAmount - totalFee) - _promise.Amount;


                        await promiseManagement.addReceipttran(_receipttran_toAdd);


                        //ยอดที่ชำระ
                        globalData.paymentAmount = (decimal)_receipttran_toAdd.Amount;

                        for (int i = 0; i < period_pay_qty; i++)
                        {
                            Receiptdesc _receiptdesc_toAdd = new Receiptdesc();
                            _receiptdesc_toAdd.PromiseId = _promise.Id;
                            _receiptdesc_toAdd.BranchId = _promise.BranchId;
                            _receiptdesc_toAdd.CustomerId = _promise.CustomerId;
                            _receiptdesc_toAdd.Receiptno = ReceiptNo;
                            _receiptdesc_toAdd.ReceipttranId = _receipttran_toAdd.Id;
                            _receiptdesc_toAdd.PeriodtranId = _receipttran_toAdd.peroidtrans_info[i].Id;
                            _receiptdesc_toAdd.Tdate = DateTime.Now.AddYears(543).ToString("dd/MM/yyyy");
                            _receiptdesc_toAdd.Tdateformat = DateTime.Now.ToString("yyyyMMdd");
                            _receiptdesc_toAdd.Tdatecal = DateTime.Now.AddYears(543).ToString("dd/MM/yyyy");
                            _receiptdesc_toAdd.Tdatecalformat = DateTime.Now.ToString("yyyyMMdd");

                            _receiptdesc_toAdd.Period = _receipttran_toAdd.peroidtrans_info[i].Period;
                            _receiptdesc_toAdd.Perioddate = _receipttran_toAdd.peroidtrans_info[i].Tdateformat;
                            _receipttran_toAdd.Charge1amt = _receipttran_toAdd.Charge1amt;
                            _receipttran_toAdd.Charge2amt = _receipttran_toAdd.Charge2amt;

                            decimal totalDue = (decimal)_receipttran_toAdd.peroidtrans_info[i].Capital + (decimal)_receipttran_toAdd.peroidtrans_info[i].Interest;

                            while (globalData.paymentAmount > 0)
                            {
                                if (payment_method != 4)
                                {
                                    if (globalData.paymentAmount >= totalDue)
                                    {
                                        _receiptdesc_toAdd.Cappaid = _receipttran_toAdd.peroidtrans_info[i].Capital * -1;
                                        _receiptdesc_toAdd.Intpaid = _receipttran_toAdd.peroidtrans_info[i].Interest * -1;
                                        var total = _receiptdesc_toAdd.Cappaid + _receiptdesc_toAdd.Intpaid;
                                        _receiptdesc_toAdd.Amount = total;
                                        globalData.RemainingPaid = globalData.paymentAmount - (decimal)_receiptdesc_toAdd.Amount;
                                        globalData.paymentAmount -= totalDue;
                                        break;
                                    }
                                    else
                                    {
                                        _receiptdesc_toAdd.pending_amount = globalData.RemainingPaid;
                                        if (_receiptdesc_toAdd.pending_amount >= _receipttran_toAdd.peroidtrans_info[i].Interest)
                                        {
                                            var amount_over_int = _receiptdesc_toAdd.pending_amount - _receipttran_toAdd.peroidtrans_info[i].Interest;
                                            _receiptdesc_toAdd.Intpaid = _receipttran_toAdd.peroidtrans_info[i].Interest * -1;
                                            _receiptdesc_toAdd.Cappaid = (_receipttran_toAdd.peroidtrans_info[i].Capital - amount_over_int) * -1;
                                            var total = _receiptdesc_toAdd.Cappaid + _receiptdesc_toAdd.Intpaid;
                                            _receiptdesc_toAdd.Amount = total * -1;
                                            globalData.RemainingPaid -= _receiptdesc_toAdd.pending_amount;
                                            break;
                                        }
                                        else
                                        {
                                            _receiptdesc_toAdd.Intpaid = _receiptdesc_toAdd.pending_amount * -1;
                                            _receiptdesc_toAdd.Amount = _receiptdesc_toAdd.pending_amount * -1;
                                            globalData.paymentAmount -= _receiptdesc_toAdd.pending_amount;
                                            break;
                                        }
                                    }
                                }
                                else 
                                {
                                    _receiptdesc_toAdd.Deposit = (decimal)_receipttran_toAdd.Amount;
                                    _receiptdesc_toAdd.Periodchg = current_periods;
                                    _receiptdesc_toAdd.payment_method = 4;
                                    break;
                                }

                            }

                            //  _receiptdesc_toAdd.pending_amount = globalData.RemainingPaid;
                            _receiptdesc_toAdd.Usercode = globalData.username;
                            _receiptdesc_toAdd.Chargeamt = _promise_pay.total_fee;

                            lst_receiptdescs.Add(_receiptdesc_toAdd);

                        }

                        await promiseManagement.addReceipdesc(lst_receiptdescs);

                        await JSRuntime.InvokeVoidAsync("paymentsuccess");
                        await Task.Delay(100);

                        //    navigationManager.NavigateTo($"/paymentlist/{branch_id}/{c_id}/{promise_id}");
                        navigationManager.NavigateTo($"/paymentlist/{branch_id}/{c_id}/{promise_id}", forceLoad: true);
                    }
                    else
                    {
                        await JSRuntime.InvokeVoidAsync("alert_error_pay_notenough");

                    }
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("paymenterror");
                }
            }
            else 
            {
                await JSRuntime.InvokeVoidAsync("paymenterror_amount");
            }
           
        }

        private void goback()
        {
            navigationManager.NavigateTo($"/customerlist/{branch_id}");
        }
        private int? ExpandedPeriodtransId;
        private void ToggleDetails(int periodtranId)
        {
            if (ExpandedPeriodtransId == periodtranId)
            {
                ExpandedPeriodtransId = null;
            }
            else
            {
                ExpandedPeriodtransId = periodtranId;
            }
        }
        private async Task receipt(int peroidtran_id)
        {
            string url = $"/paymentrecipe/{peroidtran_id}";
            await JSRuntime.InvokeAsync<object>("open", url, "_blank");
            //navigationManager.NavigateTo("/paymentrecipe");
        }

    }
}
