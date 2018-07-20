using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private string _name;
    private int _id;
    private int _stylistId;


    public Client(string name, int stylistId, int id = 0)
    {
      _name = name;
      _id = id;
      _stylistId = stylistId;
    }
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());

        return(idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@ClientName, @ClientStylistId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@ClientName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@ClientStylistId";
      stylist_id.Value = this._stylistId;
      cmd.Parameters.Add(stylist_id);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int ClientId = rdr.GetInt32(0);
        string ClientName = rdr.GetString(1);
        int ClientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(ClientName, ClientStylistId, ClientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int ClientId = 0;
      string ClientName = "";
      int ClientStylistId = 0;

      while(rdr.Read())
      {
        ClientId = rdr.GetInt32(0);
        ClientName = rdr.GetString(1);
        ClientStylistId = rdr.GetInt32(2);
      }
      Client newClient = new Client(ClientName, ClientStylistId, ClientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newClient;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @ClientId; DELETE FROM clients_stylists WHERE client_id = @ClientId;";

      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.GetId();
      cmd.Parameters.Add(clientIdParameter);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients_stylists (client_id, stylist_id) VALUES (@ClientId, @StylistId );";

      MySqlParameter client_id = new MySqlParameter();
      client_id.ParameterName = "@StylistId";
      client_id.Value = newStylist.GetId();
      cmd.Parameters.Add(client_id);

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@ClientId";
      stylist_id.Value = _id;
      cmd.Parameters.Add(stylist_id);

      cmd.ExecuteNonQuery();
      conn. Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"SELECT stylists.* FROM clients JOIN stylists_clients ON (clients.id = stylists_clients.stylist_id) JOIN stylists ON (stylists_clients.client_id = stylists.id) WHERE clients.id = @ClientId;";

      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = _id;
      cmd.Parameters.Add(clientIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylists = new List<Stylist>{};

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        stylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylists;
    }
  }
}
