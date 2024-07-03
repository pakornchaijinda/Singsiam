using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace SingSiamOffice.Services
{
    public class ReadNatID_Service
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        public async Task<Models.ReadNatID_Info> CheckDriver()
        {
            HttpClient client = new HttpClient();
            Models.ReadNatID_Info info = new Models.ReadNatID_Info();
            bool resultBool = false;
            bool isUpdated = false;
            string version = "none";

            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:21998/info");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        info = JsonSerializer.Deserialize<Models.ReadNatID_Info>(responseBody);

                        if (info != null && info.result == "ok")
                        {
                            resultBool = true;
                            if (info.version >= 0.8)
                            {
                                isUpdated = true;
                            }
                            version = info.version.ToString();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: " + ex.Message);
            }

            return info;
        }

        public async Task<Models.ReadNatID_Info> ReadCard()
        {
       
                
            HttpClient client = new HttpClient();
            Models.ReadNatID_Info info = new Models.ReadNatID_Info();


          await JSRuntime.InvokeVoidAsync("Test");
            //try
            //{
            //    HttpResponseMessage response = await client.GetAsync("http://localhost:21998/readCard");

            //    if (response.IsSuccessStatusCode)
            //    {
            //        string responseBody = await response.Content.ReadAsStringAsync();

            //        if (!string.IsNullOrEmpty(responseBody))
            //        {
            //            info = JsonSerializer.Deserialize<Models.ReadNatID_Info>(responseBody);

            //            if (info != null && info.result == "ok")
            //            {
            //                info.fullname = info.fname_th + " " + info.sname_th;
            //                info.address = "บ้านเลขที่ " + info.address_no + " หมู่ที่ " + info.address_moo + " ซอย " + info.address_soi + " ตำบล " + info.address_tumbol + " อำเภอ " + info.address_amphor + " จังหวัด " + info.address_provinice;
            //                info.issue_date = Convert.ToDateTime(info.issue_date).AddYears(543).ToString("dd/MM/yyyy");
            //                info.issue_expire = Convert.ToDateTime(info.issue_expire).AddYears(543).ToString("dd/MM/yyyy");
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Request failed with status code: " + response.StatusCode);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception occurred: " + ex.Message);
            //}

            return info;
        }



    }
}
