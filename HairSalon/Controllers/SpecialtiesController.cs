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
      return View(Stylist.GetAll());
    }
    [HttpPost("/specialties/add")]
    public ActionResult CollectInfo(string newspecialty)
    {
      Specialty newSpecialty = new Specialty(newspecialty);
      newSpecialty.Save();
      Stylist newStylist = Stylist.Find(int.Parse(Request.Form["stylist"]));
      newSpecialty.AddStylist(newStylist);
      return RedirectToAction("Index");
      // // specialty.AddStylist();
      // // List<Item> all = Item.GetAll();
      // return RedirectToAction("Index");
    }



    [HttpGet("/specialties/{id}/stylists")]
    public ActionResult List(int id)
    {
      Dictionary<string,object> model = new Dictionary<string,object>{};
      Specialty thisSpecialty = Specialty.Find(id);
      List<Stylist> allStylists = thisSpecialty.GetStylists();
      model.Add("specialtyId", id);
      model.Add("thisSpecialty", thisSpecialty);
      model.Add("allStylists", allStylists);
      return View(model);
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
      Dictionary<string,object> model = new Dictionary<string,object>{};
      Specialty thisSpecialty = Specialty.Find(id);
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("specialtyId", id);
      model.Add("stylists", allStylists);
      model.Add("thisSpecialty", thisSpecialty);


      return View(model);
    }
    [HttpPost("/specialties/{id}/edit")]
    public ActionResult EditSpecialty(int id)
    {
      Stylist newStylist = Stylist.Find(int.Parse(Request.Form["newstylist"]));
      Specialty thisSpecialty = Specialty.Find(id);
      thisSpecialty.AddStylist(newStylist);
      return RedirectToAction("Index");
    }

  }
}
