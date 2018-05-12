using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BandTrackerApp;
using System;

namespace BandTrackerApp
{
    public class Venue
    {
        private string _name;
        private string _eventDate;
        private string _address;
        private string _contact;
        private int _id;

        public Venue(string name, string eventDate, string address, string contact, int id = 0)
        {
            _id = id;
            _name = name;
            _eventDate = eventDate;
            _address = address;
            _contact = contact;
        }

        public void SetId(int newId)
        {
            _id = newId;
        }

        public void SetName(string newName)
        {
            _name = newName;
        }

        public void SetEventDate(string newDate)
        {
            _eventDate = newDate;
        }

        public void SetAddress(string newAddress)
        {
            _address = newAddress;
        }

        public void SetContact(string newContact)
        {
            _contact = newContact;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetEventDate()
        {
            return _eventDate;
        }

        public string GetAddress()
        {
            return _address;
        }

        public string GetContact()
        {
            return _contact;
        }

        public override bool Equals(System.Object otherVenue)
        {
            if (!(otherVenue is Venue))
            {
              return false;
            }
            else
          {
              Venue newVenue = (Venue) otherVenue;
              bool idEquality = this.GetId() == newVenue.GetId();
              bool nameEquality = this.GetName() == newVenue.GetName();
              bool contactEquality = this.GetContact() == newVenue.GetContact();
              bool addressEquality = this.GetAddress() == newVenue.GetAddress();
              bool eventDateEquality = this.GetEventDate() == newVenue.GetEventDate();

              return (idEquality && nameEquality && eventDateEquality && contactEquality && addressEquality);
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
            cmd.CommandText = @"INSERT INTO venues (name, address, contact, eventDate) VALUES (@name, @address, @contact, @eventDate);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter address = new MySqlParameter();
            address.ParameterName = "@address";
            address.Value = this._address;
            cmd.Parameters.Add(address);

            MySqlParameter contact = new MySqlParameter();
            contact.ParameterName = "@contact";
            contact.Value = this._contact;
            cmd.Parameters.Add(contact);

            MySqlParameter eventDate = new MySqlParameter();
            eventDate.ParameterName = "@eventDate";
            eventDate.Value = this._eventDate;
            cmd.Parameters.Add(eventDate);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddBand(Band newBand)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO bands_venues (band_id,  venue_id) VALUES (@BandId, @VenueId);";

            MySqlParameter venue_id = new MySqlParameter();
            venue_id.ParameterName = "@VenueId";
            venue_id.Value = _id;
            cmd.Parameters.Add(venue_id);

            MySqlParameter band_id = new MySqlParameter();
            band_id.ParameterName = "@BandId";
            band_id.Value = newBand.GetId();
            cmd.Parameters.Add(band_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void DeleteBandFromVenue(int bandId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM bands_venues WHERE band_id = @bandId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@bandId";
            searchId.Value = bandId;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Band> GetBands()
        {
             MySqlConnection conn = DB.Connection();
             conn.Open();
             MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
             cmd.CommandText = @"SELECT bands.* FROM venues
                 JOIN bands_venues ON (venues.id = bands_venues.venue_id)
                 JOIN bands ON (bands_venues.band_id = bands.id)
                 WHERE venues.id = @VenueId;";

             MySqlParameter venueIdParameter = new MySqlParameter();
             venueIdParameter.ParameterName = "@VenueId";
             venueIdParameter.Value = _id;
             cmd.Parameters.Add(venueIdParameter);

             MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
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

        public void Edit(string newName, string newEventDate, string newAddress, string newContact)
        {
             MySqlConnection conn = DB.Connection();
             conn.Open();
             var cmd = conn.CreateCommand() as MySqlCommand;
             cmd.CommandText = @"UPDATE venues SET name = @name, address = @address, contact = @contact, eventDate = @eventDate WHERE id = @searchId;";

             MySqlParameter searchId = new MySqlParameter();
             searchId.ParameterName = "@searchId";
             searchId.Value = _id;
             cmd.Parameters.Add(searchId);

             MySqlParameter name = new MySqlParameter();
             name.ParameterName = "@name";
             name.Value = newName;
             cmd.Parameters.Add(name);

             MySqlParameter address = new MySqlParameter();
             address.ParameterName = "@address";
             address.Value = newAddress;
             cmd.Parameters.Add(address);

             MySqlParameter contact = new MySqlParameter();
             contact.ParameterName = "@contact";
             contact.Value = newContact;
             cmd.Parameters.Add(contact);

             MySqlParameter eventDate = new MySqlParameter();
             eventDate.ParameterName = "@eventDate";
             eventDate.Value = newEventDate;
             cmd.Parameters.Add(eventDate);

             cmd.ExecuteNonQuery();
             _name = newName;
             _eventDate = newEventDate;
             _address = newAddress;
             _contact = newContact;
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
            cmd.CommandText = @"DELETE FROM venues WHERE id = @VenueId; DELETE FROM bands_venues WHERE venue_id = @VenueId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@VenueId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Venue> GetAll()
        {
            List<Venue> allVenues = new List<Venue> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM venues;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              Venue newVenue = new Venue(rdr.GetString(1), rdr.GetString(4), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(0));
              allVenues.Add(newVenue);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allVenues;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM venues;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
