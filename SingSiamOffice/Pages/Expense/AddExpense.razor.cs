using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using Microsoft.EntityFrameworkCore;
using SingSiamOffice.Models;
using System;
using System.Diagnostics;

namespace SingSiamOffice.Pages.Expense
{
    public partial class AddExpense
    {

        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
 



        public int accountNo { get; set; }
        public int deducted { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public string description { get; set; }

        //วิธีการชำระค่างวด
        public int payment_method { get; set; }

        //วิธีการชำระ
        public string payment_method_description { get; set; }



     
  

        

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }

        private async Task submit(int id)
        {
            if (subject_Id == 0 && amount == 0)
            {
                Snackbar.Add("โปรดเลือกรายการและใส่จำนวน", Severity.Error);
                return;
            }
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmSaveData");
            if (confirm)
            {
                try
                {
                    var Add_expren = new TransactionHistory();
                    if (subject_Id == 4) //โอนเงินไปสาขาอื่น
                    {
                        description = description1 + " โอนเงินให้กับสาขา: " + selectBranch.BranchCode + " | " + selectBranch.BranchName;
                        Add_expren = new TransactionHistory()
                        {
                            BranchId = id,
                            TransectionRef = name,
                            SubjectId = subject_Id,
                            SlipUrl = filePath,
                            Price = amount,
                            CreateAt = DateTime.Now,
                            LoginId = userLogin.Id,
                            Detial = description,
                            PaymentMethod = 2
                        };
                        var add_amout_to_brach = new TransactionHistory()
                        {
                            BranchId = selectBranch.Id,
                            TransectionRef = name,
                            SubjectId = 21,
                            SlipUrl = filePath,
                            Price = amount,
                            CreateAt = DateTime.Now,
                            LoginId = userLogin.Id,
                            Detial = description1+ " ได้รับเงินโอนเงินจากสาขา: " + my_b.BranchCode + " | " + my_b.BranchName,
                            PaymentMethod = 2
                        };
                        db.TransactionHistories.Add(add_amout_to_brach);
                        await db.SaveChangesAsync();
                    }else if (subject_Id == 9) //ถอนเงินตั้งสำรอง
                    {
                        Add_expren = new TransactionHistory()
                        {
                            BranchId = id,
                            TransectionRef = name,
                            SubjectId = subject_Id,
                            SlipUrl = filePath,
                            Price = amount,
                            CreateAt = DateTime.Now,
                            LoginId = userLogin.Id,
                            Detial = description,
                            PaymentMethod = 1
                        };
                    }
                    else if (subject_Id == 10)//ฝากเงินธนาคาร
                    {
                        Add_expren = new TransactionHistory()
                        {
                            BranchId = id,
                            TransectionRef = name,
                            SubjectId = subject_Id,
                            SlipUrl = filePath,
                            Price = amount,
                            CreateAt = DateTime.Now,
                            LoginId = userLogin.Id,
                            Detial = description,
                            PaymentMethod = 2
                        };
                    }
                    else
                    {
                         Add_expren = new TransactionHistory()
                        {
                            BranchId = id,
                            TransectionRef = name,
                            SubjectId = subject_Id,
                            SlipUrl = filePath,
                            Price = amount,
                             LoginId = userLogin.Id,
                             CreateAt = DateTime.Now,
                            Detial = description,
                            PaymentMethod = Paytype
                        };
                    }
                    db.TransactionHistories.Add(Add_expren);
                    await db.SaveChangesAsync();

                    await JSRuntime.InvokeVoidAsync("confirm");
                    await Task.Delay(100);

                    navigationManager.NavigateTo("/expense-list/"+ id.ToString());
                }
                catch
                {
                    await JSRuntime.InvokeVoidAsync("alert_error");
                }
               

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("alert_error");
            }
        }

        private void goback()
        {
            navigationManager.NavigateTo("/expense-list");
        }

        [Inject]
        IWebHostEnvironment _env { get; set; }
        public IBrowserFile ImageFile { get; set; }
        public byte[] ImageFileData { get; set; }
        public string filePath;
        private async void OnInputFileChanged(InputFileChangeEventArgs e)
        {

            
      
        
            if (e?.File is null)
                return; 

        

           ImageFile = await e.File.RequestImageFileAsync("image/jpeg", 400, 400);
            if (ImageFile is null)
                return;

            await using var imageStream = ImageFile.OpenReadStream();
            ImageFileData = new byte[ImageFile.Size];
            await imageStream.ReadAsync(ImageFileData);

            // Create a unique folder name based on current date and time
            var folderName = b_id;

        
            // Create the folder inside the wwwroot/Uploads directory
            var folderPath = Path.Combine("Uploads", folderName.ToString());


            var folderDirectory = Path.Combine(_env.WebRootPath, folderPath);
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }



            // Generate a unique file name for the uploaded image
            var fileName = name+ Path.GetExtension(e.File.Name);

            // Combine the folder path and file name to get the full path of the image
            var file_Path = Path.Combine(folderDirectory, fileName);


            using (var stream = new FileStream(file_Path, FileMode.Create))
            {
                await e.File.OpenReadStream().CopyToAsync(stream);
            }

            int index = file_Path.IndexOf("wwwroot");

            if (index != -1)
            {
                // Split the string based on the search text

                string[] parts = file_Path.Split(new[] { "wwwroot" }, StringSplitOptions.None);


                //filePath = parts[2];
                filePath = parts[1];
            }
            // Save the uploaded image to the specified path



            StateHasChanged();

        }
    }
}
