using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SingSiamOffice.Models;
using System.Linq;

namespace SingSiamOffice.Manage
{
    public class PromiseManagement
    {
        SingsiamdbContext db = new SingsiamdbContext();
        public async Task<Promise> addPromise(Promise promise) 
        {
       
            try
            {
                var obj = JsonConvert.SerializeObject(promise);
                db.Promises.Add(promise);
                await db.SaveChangesAsync();



                return promise;
            }
            catch (Exception ex)
            {
                var tt = ex.InnerException.Message;
                return null;
            }
        }
        public async Task addGuarantor(List<Guarantor> guarantor)
        {
            try
            {
                foreach (Guarantor guarantor1 in guarantor)
                {
                    db.Guarantors.Add(guarantor1);
                    await db.SaveChangesAsync();

                }

              
            }
            catch (Exception ex) {}
        }
        public async Task<bool> addPeriodtran(List<Periodtran> periodtrans)
        {
            try
            {
                foreach (Periodtran periodtran in periodtrans) 
                {
                    db.Periodtrans.Add(periodtran);
                    await db.SaveChangesAsync();

                }

                return true;
            }
            catch (Exception ex) { return false; }
        }
        public async Task UpdateRunningNo(int branchId,string type)
        {
            var toEdit = db.RunningNos.Where(s => s.BranchId == branchId && s.Type == type).FirstOrDefault();
            if (toEdit != null) 
            {
                toEdit.CurrentNo = toEdit.NextNo;
                toEdit.NextNo = toEdit.NextNo + 1;
                db.Entry(toEdit).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }


        public async Task<Receipttran> addReceipttran(Receipttran receipttrans)
        {
            try
            {
               
                    db.Receipttrans.Add(receipttrans);
                    await db.SaveChangesAsync();

                return receipttrans;


            }
            catch (Exception ex) 
            {
                return null;
            }

   
        }
        public async Task addReceipdesc(List<Receiptdesc> receiptdesc)
        {
            try
            {
                foreach(var items in receiptdesc) 
                {
                    db.Receiptdescs.Add(items);
                    await db.SaveChangesAsync();


                    var to_edit = db.Periodtrans.Where(s => s.Id == items.PeriodtranId).FirstOrDefault();
                    if (to_edit != null)
                    {
                        if (items.payment_method != 4)
                        {
                            if (to_edit.Amount == (items.Amount * -1))
                            {
                                to_edit.Ispaid = true;
                                to_edit.Status = 1;
                            }
                            else
                            {
                                to_edit.Ispaid = false;
                            }

                            to_edit.Cappaid = Math.Abs((decimal)items.Cappaid);
                            to_edit.Intpaid = Math.Abs((decimal)items.Intpaid);
                            to_edit.Paidamount = Math.Abs((decimal)items.Amount);
                            to_edit.Paidremain = items.pending_amount;
                        }
                        else 
                        {
                            to_edit.Deposit = to_edit.Deposit + items.Deposit;
                        }
                     
                     
                        db.Entry(to_edit).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                    }
                }
               

            }
            catch (Exception ex)
            {
               
            }


        }


    }
}
