using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class SpecialtiesController : Controller
  {
    [HttpGet("/specialties/")]
    public ActionResult Index()
    {
      return View(Specialty.GetAll());
    }
    [HttpGet("/specialties/add")]
    public ActionResult SpecialtyForm()
    {
      return View(Specialty.GetAll());
    }
    [HttpPost("/specialties")]
    public ActionResult CollectInfo(string newspeciality)
    {
      Specialty newSpecialty = new Specialty(newspeciality);
      newSpecialty.Save();
      // List<Item> all = Item.GetAll();
      return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{id}/stylists")]
    public ActionResult List(int id)
    {
      Specialty thisSpecialty = Specialty.Find(id);

      List<Stylist> allStylists = thisSpecialty.GetStylists();
      return View(thisSpecialty);
    }
    [HttpGet("/specialties/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Specialty thisSpecialty = Specialty.Find(id);
      return View(thisSpecialty);
    }
    [HttpPost("/specialties/{id}/delete")]
    public ActionResult DeleteSpecialty(int id)
    {
      Specialty thisSpecialty = Specialty.Find(id);
      thisSpecialty.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Specialty thisSpecialty = Specialty.Find(id);
      return View(thisSpecialty);
    }
    [HttpPost("/specialties/{id}/edit")]
    public ActionResult EditName(int id)
    {
      Specialty thisSpecialty = Specialty.Find(id);
      thisSpecialty.Edit(Request.Form["newName"]);
      return RedirectToAction("Index");
    }
  }
}
