using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace SingSiamOffice.Pages.Report
{
    public partial class ContractReport
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        private string role { get; set; } = "admin";



        string date = DateTime.Now.AddYears(543).ToString("dd/MM/yyyy");
        DateTime? filter_date { get; set; }


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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
                await JSRuntime.InvokeVoidAsync("barchart");
            }
        }



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
    }
}
