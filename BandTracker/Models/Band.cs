using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BandTrackerApp;
using System;

namespace BandTrackerApp
{
    public class Band
    {
        private string _name;
        private string _genre;
        private string _leader;
        private string _members;
        private string _originPlace;
        private string _originYear;
        private string _agent;
        private string _agentContact;
        private int _id;

        public Band(string name, string genre, string leader, string members, string originPlace, string originYear, string agentName, string agentContact, int id = 0)
        {
            _name = name;
            _genre = genre;
            _leader = leader;
            _members = members;
            _originPlace = originPlace;
            _originYear = originYear;
            _agent = agentName;
            _agentContact = agentContact;
            _id = id;
        }

        public void SetName(string newName)
        {
            _name = newName;
        }

        public void SetGenre(string newGenre)
        {
            _genre = newGenre;
        }

        public void SetLeader(string newLeader)
        {
            _leader = newLeader;
        }

        public void SetMembers(string newMembers)
        {
            _members = newMembers;
        }

        public void SetOriginPlace(string newOrigin)
        {
            _originPlace = newOrigin;
        }

        public void SetOriginYear(string newYear)
        {
            _originYear = newYear;
        }

        public void SetAgent(string newAgent)
        {
            _agent = newAgent;
        }

        public void SetAgentContact(string newContact)
        {
            _agentContact = newContact;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetGenre()
        {
            return _genre;
        }

        public string GetLeader()
        {
            return _leader;
        }

        public string GetMembers()
        {
            return _members;
        }

        public string GetOriginplace()
        {
            return _originPlace;
        }

        public string GetoriginYear()
        {
            return _originYear;
        }

        public string GetAgent()
        {
            return _agent;
        }

        public string GetAgentContact()
        {
            return _agentContact;
        }

        public int GetId()
        {
            return _id;
        }

        public override bool Equals(System.Object otherBand)
        {
            if (!(otherBand is Band))
            {
              return false;
            }
            else
          {
              Band newBand = (Band) otherBand;
              bool idEquality = this.GetId() == newBand.GetId();
              bool nameEquality = this.GetName() == newBand.GetName();
              bool agentContactEquality = this.GetAgentContact() == newBand.GetAgentContact();
              bool agentEquality = this.GetAgent() == newBand.GetAgent();
              bool genreEquality = this.GetGenre() == newBand.GetGenre();
              bool leaderEquality = this.GetLeader() == newBand.GetLeader();
              bool membersEquality = this.GetMembers() == newBand.GetMembers();
              bool originPlaceEquality = this.GetOriginplace() == newBand.GetOriginplace();
              bool originYearEquality = this.GetoriginYear() == newBand.GetoriginYear();

              return (idEquality && nameEquality && agentEquality && agentContactEquality && genreEquality && leaderEquality && membersEquality && originPlaceEquality && originYearEquality);
          }
        }

        public override int GetHashCode()
        {
          return this.GetName().GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO bands (name, genre, leader, members, originPlace, originYear, agent, agentContact) VALUES (@name, @genre, @leader, @members, @originPlace, @originYear, @agent, @agentContact);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter genre = new MySqlParameter();
            genre.ParameterName = "@genre";
            genre.Value = this._genre;
            cmd.Parameters.Add(genre);

            MySqlParameter leader = new MySqlParameter();
            leader.ParameterName = "@leader";
            leader.Value = this._leader;
            cmd.Parameters.Add(leader);

            MySqlParameter members = new MySqlParameter();
            members.ParameterName = "@members";
            members.Value = this._members;
            cmd.Parameters.Add(members);

            MySqlParameter originPlace = new MySqlParameter();
            originPlace.ParameterName = "@originPlace";
            originPlace.Value = this._originPlace;
            cmd.Parameters.Add(originPlace);

            MySqlParameter originYear = new MySqlParameter();
            originYear.ParameterName = "@originYear";
            originYear.Value = this._originYear;
            cmd.Parameters.Add(originYear);

            MySqlParameter agent = new MySqlParameter();
            agent.ParameterName = "@agent";
            agent.Value = this._agent;
            cmd.Parameters.Add(agent);

            MySqlParameter agentContact = new MySqlParameter();
            agentContact.ParameterName = "@agentContact";
            agentContact.Value = this._agentContact;
            cmd.Parameters.Add(agentContact);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddVenue(Venue newVenue)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO bands_venues (band_id,  venue_id) VALUES (@BandId, @VenueId);";

            MySqlParameter venue_id = new MySqlParameter();
            venue_id.ParameterName = "@VenueId";
            venue_id.Value = newVenue.GetId();
            cmd.Parameters.Add(venue_id);

            MySqlParameter band_id = new MySqlParameter();
            band_id.ParameterName = "@BandId";
            band_id.Value = _id;
            cmd.Parameters.Add(band_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void DeleteVenueFromBand(int venueId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM bands_venues WHERE venue_id = @venueId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@venueId";
            searchId.Value = venueId;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Venue> GetVenues()
        {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT venues.* FROM bands
               JOIN bands_venues ON (bands.id = bands_venues.band_id)
               JOIN venues ON (bands_venues.venue_id = venues.id)
               WHERE bands.id = @BandId;";

           MySqlParameter bandIdParameter = new MySqlParameter();
           bandIdParameter.ParameterName = "@BandId";
           bandIdParameter.Value = _id;
           cmd.Parameters.Add(bandIdParameter);

           MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
           List<Venue> venues = new List<Venue>{};

           while(rdr.Read())
           {
             Venue newVenue = new Venue(rdr.GetString(1), rdr.GetString(4), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(0));
             venues.Add(newVenue);
           }
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           return venues;
       }

       public void Edit(string newName, string newGenre, string newLeader, string newMembers, string newOriginPlace, string newOriginYear, string newAgent, string newAgentContact)
       {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE bands SET name = @name, genre = @genre, leader = @leader, members = @members, originPlace = @originPlace, originYear = @originYear, agent = @agent, agentContact = @agentContact WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = newName;
            cmd.Parameters.Add(name);

            MySqlParameter genre = new MySqlParameter();
            genre.ParameterName = "@genre";
            genre.Value = newGenre;
            cmd.Parameters.Add(genre);

            MySqlParameter leader = new MySqlParameter();
            leader.ParameterName = "@leader";
            leader.Value = newLeader;
            cmd.Parameters.Add(leader);

            MySqlParameter members = new MySqlParameter();
            members.ParameterName = "@members";
            members.Value = newMembers;
            cmd.Parameters.Add(members);

            MySqlParameter originPlace = new MySqlParameter();
            originPlace.ParameterName = "@originPlace";
            originPlace.Value = newOriginPlace;
            cmd.Parameters.Add(originPlace);

            MySqlParameter originYear = new MySqlParameter();
            originYear.ParameterName = "@originYear";
            originYear.Value = newOriginYear;
            cmd.Parameters.Add(originYear);

            MySqlParameter agent = new MySqlParameter();
            agent.ParameterName = "@agent";
            agent.Value = newAgent;
            cmd.Parameters.Add(agent);

            MySqlParameter agentContact = new MySqlParameter();
            agentContact.ParameterName = "@agentContact";
            agentContact.Value = newAgentContact;
            cmd.Parameters.Add(agentContact);

            cmd.ExecuteNonQuery();
            _name = newName;
            _genre = newGenre;
            _leader = newLeader;
            _members = newMembers;
            _originPlace = newOriginPlace;
            _originYear = newOriginYear;
            _agent = newAgent;
            _agentContact = newAgentContact;
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
           cmd.CommandText = @"DELETE FROM bands WHERE id = @BandId; DELETE FROM bands_venues WHERE band_id = @BandId;";

           MySqlParameter searchId = new MySqlParameter();
           searchId.ParameterName = "@BandId";
           searchId.Value = _id;
           cmd.Parameters.Add(searchId);

           cmd.ExecuteNonQuery();
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
       }

       public static Band Find(int id)
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM bands WHERE id = (@searchId);";

           MySqlParameter searchId = new MySqlParameter();
           searchId.ParameterName = "@searchId";
           searchId.Value = id;
           cmd.Parameters.Add(searchId);

           var rdr = cmd.ExecuteReader() as MySqlDataReader;
           Band newBand = new Band("","","","","","","","");

           while(rdr.Read())
           {
              newBand.SetId(rdr.GetInt32(0));
              newBand.SetName(rdr.GetString(1));
              newBand.SetGenre(rdr.GetString(2));
              newBand.SetLeader(rdr.GetString(3));
              newBand.SetMembers(rdr.GetString(4));
              newBand.SetOriginPlace(rdr.GetString(5));
              newBand.SetOriginYear(rdr.GetString(6));
              newBand.SetAgent(rdr.GetString(7));
              newBand.SetAgentContact(rdr.GetString(8));
           }

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }

           return newBand;
       }

       public static List<Band> Find(string name)
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM bands WHERE name LIKE (@searchName);";

           MySqlParameter searchName = new MySqlParameter();
           searchName.ParameterName = "@searchName";
           searchName.Value = name + "%";
           cmd.Parameters.Add(searchName);

           var rdr = cmd.ExecuteReader() as MySqlDataReader;
           List<Band> allBands = new List<Band>{};

           while(rdr.Read())
           {
               Band newBand = new Band(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetString(6), rdr.GetString(7), rdr.GetString(8), rdr.GetInt32(0));
               allBands.Add(newBand);
           }

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }

           return allBands;
       }

       public static List<Band> Find(string genre)
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM bands WHERE genre = (@searchGenre);";

           MySqlParameter searchGenre = new MySqlParameter();
           searchGenre.ParameterName = "@searchGenre";
           searchGenre.Value = genre;
           cmd.Parameters.Add(searchGenre);

           var rdr = cmd.ExecuteReader() as MySqlDataReader;
           List<Band> allBands = new List<Band>{};

           while(rdr.Read())
           {
               Band newBand = new Band(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetString(6), rdr.GetString(7), rdr.GetString(8), rdr.GetInt32(0));
               allBands.Add(newBand);
           }

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }

           return allBands;
       }

       public static List<Band> GetAll()
       {
           List<Band> allBands = new List<Band> {};
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM bands;";
           var rdr = cmd.ExecuteReader() as MySqlDataReader;
           while(rdr.Read())
           {
               Band newBand = new Band(rdr.GetString(1), rdr.GetString(2),  rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetString(6),  rdr.GetString(7), rdr.GetString(8), rdr.GetInt32(0));
               allBands.Add(newBand);
           }
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           return allBands;
       }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM bands;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
