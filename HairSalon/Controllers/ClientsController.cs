using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    [HttpPost("/clients")]
    public ActionResult CollectInfo(string newclient, int stylist)
    {
      Client newClient = new Client(newclient, stylist);
      newClient.Save();
      // List<Client> all = Client.GetAll();
      return RedirectToAction("Index");
    }

    [HttpGet("/clients")]
    public ActionResult Index()
    {
      // List<Client> all = Client.GetAll();
      return View(Client.GetAll());
    }

    [HttpGet("/clients/add")]
    public ActionResult ClientForm()
    {
      return View(Stylist.GetAll());
    }

    [HttpGet("/clients/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Client thisClient = Client.Find(id);
      return View(thisClient);
    }
    [HttpPost("/clients/{id}/delete")]
    public ActionResult DeleteClient(int id)
    {
      Client thisClient = Client.Find(id);
      thisClient.Delete();
      return RedirectToAction("index");
    }

    [HttpGet("/clients/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Dictionary<string,object> model = new Dictionary<string,object>{};
      Client thisClient = Client.Find(id);
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("clientId", id);
      model.Add("stylists", allStylists);
      model.Add("thisClient", thisClient);


      return View(model);
    }
    [HttpPost("/clients/{id}/edit")]
    public ActionResult EditClient(int id)
    {
      Stylist newStylist = Stylist.Find(int.Parse(Request.Form["newstylist"]));
      Client thisClient = Client.Find(id);
      thisClient.AddStylist(newStylist);
      return RedirectToAction("Index");
    }








    // [HttpGet("/clients")]
    // public ActionResult Index()
    // {
    // return View(Client.GetAll());
    // }
    //
    // [HttpPost("/clients/add")]
    // public ActionResult CollectClient(string newclient, int stylist)
    // {
    // Client newClient = new Client(newclient, stylist);
    // newClient.Save();
    // List<Client> allClients = Client.GetAll();
    // return RedirectToAction("Index");
    // }
    // [HttpPost("/clients/{id}/items")]
    // public ActionResult List(int id)
    // {
    //   Client thisClient = Client.Find(id);
    //
    //   List<Stylist> allItems = thisClient.GetItems();
    //   return View(this);
    // }
    //
    //
    // [HttpGet("/clients/add")]
    // public ActionResult ClientForm()
    // {
    //   return View(Client.GetAll());
    // }
  }
}
