using ASPEmpty.Middleware;
using ASPEmpty.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace ASPEmpty.Helpers
{
    public static class FlatHelper
    {
        // This method will call FlatMiddleware's GetFlats method
        public static String GetTable(IEnumerable<Flat> flats)
        {

            // Build the result into a StringBuilder for display
            StringBuilder result = new StringBuilder();
            foreach (var flat in flats)
            {
                result.Append("<tr>");
                result.Append($"<td>{flat.Header}</td>");
                result.Append($"<td>{flat.Description}</td>");
                result.Append($"<td>{flat.AvgMark}</td>");
                result.Append($"<td>{flat.City}</td>");
                result.Append($"<td>{flat.Address}</td>");
                result.Append($"<td>{flat.NumberOfRooms}</td>");
                result.Append($"<td>{flat.NumberOfFloors}</td>");
                result.Append($"<td>{flat.BathroomAvailability}</td>");
                result.Append($"<td>{flat.WiFiAvailability}</td>");
                result.Append($"<td>{flat.CostPerDay}</td>");
                result.Append("</tr>");
            }
            String resp = "<html>" +
                          "<body>" +
                          "<table>" +
                          "<tr>"+
                          "<th>Header</th>" +
                          "<th>Description</th>" +
                          "<th>AvgMark</th>" +
                          "<th>City</th>" +
                          "<th>Address</th>" +
                          "<th>Number of rooms</th>" +
                          "<th>Number of floors</th>" +
                          "<th>Bathroom availability</th>" +
                          "<th>WIFI availability</th>" +
                          "<th>Cost per day</th>" +
                          result.ToString()+
                          "</tr>"+
                          "</table>" +
                          "</body>" +
                          "</html>";
            return resp;
        }

        public static String GetForm(String city = "", String aMark = "", String wifiAv = "")
        {
            var flats = GetFlats();
            StringBuilder citiesB = new StringBuilder();
            foreach (var flat in flats)
            {
                citiesB.Append($"<option value = \"{flat.City}\">");
            }

            // Update the form with "name" attributes
            String submitBtn = "<input type=\"submit\">";
            String avgMarkLabel = "<label>Type avg mark</label>";
            String avgMark = $"<input type=\"text\" id=\"avgmark\" name=\"avgmark\" value = \"{aMark}\">";
            String datalistLabel = "<label>Choose city</label>";
            String datalistInp = $"<input list=\"cities\" id=\"city\" name=\"city\" value = \"{city}\">" +  
                                 "<datalist id=\"cities\">" +
                                 $"{citiesB}" +
                                 "</datalist>";
            String wifiAvLabel = "<label>Choose wifi availability</label>";
            String wifiAvInp = $"<select size=\"2\" multiple name=\"wifi\" value = \"{wifiAv}\">" + 
                               "<option value=\"Yes\">Yes</option>" +
                               "<option value=\"No\">No</option>" +
                               "</select>";

            String resp = "<html>" +
                          "<body>" +
                          "<form action=\"/getflats\" method=\"post\">" +
                          avgMarkLabel +
                          avgMark +
                          "</br></br>" +
                          datalistLabel +
                          datalistInp +
                          "</br></br>" +
                          wifiAvLabel +
                          wifiAvInp +
                          "</br></br>" +
                          submitBtn +
                          "</form>" +
                          "</body>" +
                          "</html>";
            return resp;
        }

        private static  IEnumerable<Flat> GetFlats()
        {
            RealestaterentalContext db = new RealestaterentalContext();
            IEnumerable<Flat> flats = db.Flats.ToList();
            return flats;
        }

        public static String FindFlats(String city, String avgMark, String wifiAv)
        {
            RealestaterentalContext db = new RealestaterentalContext();
            // Construct a response using the form values
            bool hasWifi = false;
            if (wifiAv == "Yes")
            {
                hasWifi = true;
            }
            var q = from d in db.Flats
                    where d.City == city.Trim()
                          && d.WiFiAvailability == hasWifi
                          && Math.Floor(d.AvgMark) == Math.Floor(Decimal.Parse(avgMark))
                    select d;

            StringBuilder res = new StringBuilder();
            res.Append($"Flats with avgMark = {avgMark} City = {city} WIFI availability = {wifiAv}\n");
            foreach(var item in q)
            {
                res.Append(item.ToString());
                res.Append("\n");

            }

            return res.ToString();
        }
    }
}
