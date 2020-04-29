using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PFCWebPanel.Context;

namespace PFCWebPanel.Controllers
{
    public class VoipManagerController : Controller
    {
        public async System.Threading.Tasks.Task<ActionResult> SaleMemberList()
        {
            ApiExecuter Ex = new ApiExecuter();
            var extensionList = await Ex.GetExtensionListRestApi();
            ViewBag.ExtensionList = new SelectList(extensionList.Select(el => new
            {
                Idd = "شماره داخلی : " + el.Extension.ToString() + " - نام اوپراتور : " + el.Name,
                id = el.Extension.ToString() + ":" + el.Name
            }).ToList(), "id", "idd");
            return View();
        }
        [HttpPost]
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> SaleMemberList(IEnumerable<string> ExtensionList)
        {
            using (PFCSqlEntities db = new PFCSqlEntities())
            {


                foreach (var selectedExtension in ExtensionList)
                {
                    int selectedExtensionNumber =
                        Convert.ToInt32(selectedExtension.Substring(0, selectedExtension.IndexOf(":")));
                    string selectedExtensionName = selectedExtension.Substring((selectedExtension.IndexOf(":") + 1), (selectedExtension.Length - (selectedExtension.IndexOf(":") + 1)));

                    TblExtensions selectedExtensionDb = db.TblExtensions.Where(ex => ex.TblExtName == selectedExtensionName && ex.TblExtNumber == selectedExtensionNumber).FirstOrDefault();

                    if (selectedExtensionDb == null)
                    {
                        TblExtensions newExtensions = new TblExtensions();
                        newExtensions.TblExtNumber = selectedExtensionNumber;
                        newExtensions.TblExtName = selectedExtensionName;
                        db.TblExtensions.Add(newExtensions);
                        db.SaveChanges();

                        TblExtGroup newExtGroup = new TblExtGroup();
                        newExtGroup.TblExtGrExtID = newExtensions.TblExtID;
                        newExtGroup.TblExtGrGroupID = 1;
                        newExtGroup.TblExtGrRow = (db.TblExtGroup.Where(te => te.TblExtGrGroupID == 1).Select(teg => teg.TblExtGrRow).DefaultIfEmpty().Max() + 1);
                        db.TblExtGroup.Add(newExtGroup);
                        db.SaveChanges();

                    }

                }

                List<TblExtensions> extensionListDb = db.TblExtensions.ToList();

                foreach (var extensionDb in extensionListDb)
                {

                    Boolean result = false;
                    string selectedExtension = extensionDb.TblExtNumber.ToString() + ":" + extensionDb.TblExtName;

                    foreach (var item in ExtensionList)
                    {

                        if (item == selectedExtension)
                        {
                            result = true;
                        }

                    }

                    if (result == false)
                    {
                        db.TblExtensions.Remove(extensionDb);
                        db.SaveChanges();
                    }

                }

                // ApiExecuter Ex = new ApiExecuter();
                //string res = Ex.EditQueueOperatorsRestApi("5000", ExtensionList).Result;
                //if (res == "true")
                //{
                await ApplyAgents(1);
                return RedirectToAction("SaleMemberList");
                //}
                //var extensionList = await Ex.GetExtensionListRestApi();
                //ViewBag.ExtensionList = new SelectList(extensionList.Select(el => new
                //{
                //    Idd = "شماره داخلی : " + el.Extension.ToString() + " - نام اوپراتور : " + el.Name,
                //    id = el.Extension.ToString() + ":" + el.Name
                //}).ToList(), "id", "idd");
                //return View();
            }

        }



        public ActionResult GetSaleMemberList()
        {
            using (PFCSqlEntities db = new PFCSqlEntities())
            {
                var res = db.TblExtGroup.Where(eg => eg.TblExtGrGroupID == 1).Select(exg => exg.TblExtensions.TblExtNumber.ToString() + ":" + exg.TblExtensions.TblExtName).ToList();
                return Json(res, JsonRequestBehavior.AllowGet);
            }

        }








        public ActionResult SaleMemberOrder()
        {
            /* using */
            PFCSqlEntities db = new PFCSqlEntities();

            var list = db.TblExtGroup.Where(eg => eg.TblExtGrGroupID == 1).OrderBy(ex => ex.TblExtGrRow).ToList();
            return View(list);


        }

        public async System.Threading.Tasks.Task<string> SaleMemberOrderStepFirst(int id)
        {
            using (PFCSqlEntities db = new PFCSqlEntities())
            {
                TblExtGroup selectedExtGroup = db.TblExtGroup.Where(eg => eg.TblExtGrID == id).FirstOrDefault();
                if (selectedExtGroup != null)
                {
                    var list = db.TblExtGroup.Where(eg => eg.TblExtGrGroupID == 1).OrderBy(ex => ex.TblExtGrRow).ToList();
                    if (list.Count == 1 || selectedExtGroup.TblExtGrRow == list.Select(xe => xe.TblExtGrRow).Min())
                    {
                        return "false";
                    }

                    selectedExtGroup.TblExtGrRow = 1;
                    db.SaveChanges();
                    int counter = 2;
                    foreach (var item in list)
                    {
                        if (item != selectedExtGroup)
                        {
                            item.TblExtGrRow = counter;
                            db.SaveChanges();
                            counter += 1;
                        }

                    }
                    await ApplyAgents(1);
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }
        public async System.Threading.Tasks.Task<string> SaleMemberOrderStepUp(int id)
        {
            using (PFCSqlEntities db = new PFCSqlEntities())
            {
                TblExtGroup selectedExtGroup = db.TblExtGroup.Where(eg => eg.TblExtGrID == id).FirstOrDefault();
                if (selectedExtGroup != null)
                {
                    var list = db.TblExtGroup.Where(eg => eg.TblExtGrGroupID == 1).OrderBy(ex => ex.TblExtGrRow).ToList();
                    if (list.Count == 1 || selectedExtGroup.TblExtGrRow == list.Select(xe => xe.TblExtGrRow).Min())
                    {
                        return "false";
                    }

                    TblExtGroup prevExtGroup = db.TblExtGroup.Where(eg => eg.TblExtGrRow < selectedExtGroup.TblExtGrRow).OrderByDescending(ex => ex.TblExtGrRow).FirstOrDefault();

                    if (prevExtGroup == null)
                    {
                        return "false";
                    }

                    int prevRow = prevExtGroup.TblExtGrRow;
                    int selRow = selectedExtGroup.TblExtGrRow;
                    prevExtGroup.TblExtGrRow = selRow;
                    selectedExtGroup.TblExtGrRow = prevRow;
                    db.SaveChanges();
                    await ApplyAgents(1);
                    return "true";
                }
                else
                {
                    return "false";
                }
            }

        }
        public async System.Threading.Tasks.Task<string> SaleMemberOrderStepLast(int id)
        {
            using (PFCSqlEntities db = new PFCSqlEntities())
            {
                TblExtGroup selectedExtGroup = db.TblExtGroup.Where(eg => eg.TblExtGrID == id).FirstOrDefault();
                if (selectedExtGroup != null)
                {
                    var list = db.TblExtGroup.Where(eg => eg.TblExtGrGroupID == 1).OrderBy(ex => ex.TblExtGrRow).ToList();

                    if (list.Count == 1 || selectedExtGroup.TblExtGrRow == list.Select(xe => xe.TblExtGrRow).Max())
                    {
                        return "false";
                    }

                    selectedExtGroup.TblExtGrRow = list.Count;
                    db.SaveChanges();
                    int counter = 1;
                    foreach (var item in list)
                    {
                        if (item != selectedExtGroup)
                        {
                            item.TblExtGrRow = counter;
                            db.SaveChanges();
                            counter += 1;
                        }

                    }
                    await ApplyAgents(1);
                    return "true";
                }
                else
                {
                    return "false";
                }
            }

        }
        public async System.Threading.Tasks.Task<string> SaleMemberOrderStepDown(int id)
        {
            using (PFCSqlEntities db = new PFCSqlEntities())
            {
                TblExtGroup selectedExtGroup = db.TblExtGroup.Where(eg => eg.TblExtGrID == id).FirstOrDefault();
                if (selectedExtGroup != null)
                {
                    var list = db.TblExtGroup.Where(eg => eg.TblExtGrGroupID == 1).OrderBy(ex => ex.TblExtGrRow).ToList();
                    if (list.Count == 1 || selectedExtGroup.TblExtGrRow == list.Select(xe => xe.TblExtGrRow).Max())
                    {
                        return "false";
                    }

                    TblExtGroup nextExtGroup = db.TblExtGroup.Where(eg => eg.TblExtGrRow > selectedExtGroup.TblExtGrRow).OrderBy(ex => ex.TblExtGrRow).FirstOrDefault();

                    if (nextExtGroup == null)
                    {
                        return "false";
                    }

                    int nextRow = nextExtGroup.TblExtGrRow;
                    int selRow = selectedExtGroup.TblExtGrRow;
                    nextExtGroup.TblExtGrRow = selRow;
                    selectedExtGroup.TblExtGrRow = nextRow;
                    db.SaveChanges();
                    await ApplyAgents(1);
                    return "true";
                }
                else
                {
                    return "false";
                }
            }

        }
        public async System.Threading.Tasks.Task ApplyAgents(int grId)
        {
            using (PFCSqlEntities db = new PFCSqlEntities())
            {
                var ExtensionList = db.TblExtGroup.Where(eg => eg.TblExtGrGroupID == 1).OrderBy(ex => ex.TblExtGrRow).Select(ex => ex.TblExtensions.TblExtNumber.ToString()).ToList();
                ApiExecuter Ex = new ApiExecuter();
                string res = await Ex.EditQueueOperatorsRestApi("5000", ExtensionList);

            }
        }
    }
}