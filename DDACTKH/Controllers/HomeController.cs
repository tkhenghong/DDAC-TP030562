using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DDACTKH.Models;

namespace DDACTKH.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult CreateCargo()
        {
            return View();
        }

        public ActionResult CreateVessel()
        {
            return View();
        }

        public ActionResult CreateYard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateYard(Yard yard)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [YARD] VALUES (@yardName, @yardLocation, @yardCountry)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@yardName", yard.yardName);
                    cmd.Parameters.AddWithValue("@yardLocation", yard.yardLocation);
                    cmd.Parameters.AddWithValue("@yardCountry", yard.yardCountry);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewYard", "Home");
        }

        [HttpPost]
        public ActionResult CreateVessel(Vessel vessel)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [VESSEL] VALUES (@vesselName, @containers, @destinationYard, @arrivalYard)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@vesselName", vessel.vesselName);
                    cmd.Parameters.AddWithValue("@containers", vessel.containers);
                    cmd.Parameters.AddWithValue("@destinationYard", vessel.destinationYard);
                    cmd.Parameters.AddWithValue("@arrivalYard", vessel.arrivalYard);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewVessel", "Home");
        }

        [HttpPost]
        public ActionResult CreateCargo(Cargo model)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [CARGO] VALUES (@cargoID, @cargoName, @cargoDeparture, @cargoArrival, @cargoVolume)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@cargoID", model.cargoId);
                    cmd.Parameters.AddWithValue("@cargoName", model.cargoName);
                    cmd.Parameters.AddWithValue("@cargoDeparture", model.cargoDeparture);
                    cmd.Parameters.AddWithValue("@cargoArrival", model.cargoArrival);
                    cmd.Parameters.AddWithValue("@cargoVolume", model.cargoVolume);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewCargo", "Home");
        }

        private SqlConnection getConnection()
        {
            String conString = ConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conString);
            return sqlConnection;
        }

        public ActionResult ViewCargo()
        {
            List<Cargo> myCargoList = new List<Cargo>();
            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [CARGO]");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cargo c = new Cargo();
                        c.cargoId = reader["cargoId"].ToString();
                        c.cargoName = reader["cargoName"].ToString();
                        c.cargoDeparture = reader["cargoDeparture"].ToString();
                        c.cargoArrival = reader["cargoArrival"].ToString();
                        c.cargoVolume = reader["cargoVolume"].ToString();
                        myCargoList.Add(c);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myCargoList);
        }

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult EditCargo(string cargoId)
        {
            Cargo myCargo = new Cargo();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [CARGO] WHERE cargoId=@cargoId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@cargoId", cargoId);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        myCargo.cargoId = reader["cargoId"].ToString();
                        myCargo.cargoName = reader["cargoName"].ToString();
                        myCargo.cargoDeparture = reader["cargoDeparture"].ToString();
                        myCargo.cargoArrival = reader["cargoArrival"].ToString();
                        myCargo.cargoVolume = reader["cargoVolume"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myCargo);
        }

        [HttpPost]
        public ActionResult EditCargo(Cargo model)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [CARGO] SET cargoName=@cargoName, cargoDeparture=@cargoDeparture, cargoArrival=@cargoArrival, cargoVolume=@cargoVolume WHERE cargoId=@cargoId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@cargoID", model.cargoId);
                    cmd.Parameters.AddWithValue("@cargoName", model.cargoName);
                    cmd.Parameters.AddWithValue("@cargoDeparture", model.cargoDeparture);
                    cmd.Parameters.AddWithValue("@cargoArrival", model.cargoArrival);
                    cmd.Parameters.AddWithValue("@cargoVolume", model.cargoVolume);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewCargo", "Home");
        }

        public ActionResult DeleteCargo(string cargoId)
        {
            Cargo myCargo = new Cargo();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [CARGO] WHERE cargoId=@cargoId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@cargoId", cargoId);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        myCargo.cargoId = reader["cargoId"].ToString();
                        myCargo.cargoName = reader["cargoName"].ToString();
                        myCargo.cargoDeparture = reader["cargoDeparture"].ToString();
                        myCargo.cargoArrival = reader["cargoArrival"].ToString();
                        myCargo.cargoVolume = reader["cargoVolume"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myCargo);
        }

        [HttpPost]
        public ActionResult DeleteCargo(Cargo model)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM [CARGO] WHERE cargoId=@cargoId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@cargoId", model.cargoId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewCargo", "Home");
        }

        public ActionResult ViewYard()
        {
            List<Yard> myYardList = new List<Yard>();
            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [YARD]");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Yard y = new Yard();
                        y.yardName = reader["yardName"].ToString();
                        y.yardLocation = reader["yardLocation"].ToString();
                        y.yardCountry = reader["yardCountry"].ToString();
                        myYardList.Add(y);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myYardList);
        }

        public ActionResult EditYard(string yardName)
        {
            Yard myYard = new Yard();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [YARD] WHERE yardName=@yardName");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@yardName", yardName);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        myYard.yardName = reader["yardName"].ToString();
                        myYard.yardLocation = reader["yardLocation"].ToString();
                        myYard.yardCountry = reader["yardCountry"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myYard);
        }

        [HttpPost]
        public ActionResult EditYard(Yard model)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [YARD] SET yardLocation=@yardLocation, yardCountry=@yardCountry WHERE yardName=@yardName");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@yardLocation", model.yardLocation);
                    cmd.Parameters.AddWithValue("@yardCountry", model.yardCountry);
                    cmd.Parameters.AddWithValue("@yardName", model.yardName);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewYard", "Home");
        }

        public ActionResult DeleteYard(string yardName)
        {
            Yard myYard = new Yard();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [YARD] WHERE yardName=@yardName");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@yardName", yardName);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        myYard.yardName = reader["yardName"].ToString();
                        myYard.yardLocation = reader["yardLocation"].ToString();
                        myYard.yardCountry = reader["yardCountry"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myYard);
        }

        [HttpPost]
        public ActionResult DeleteYard(Yard model)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM [YARD] WHERE yardName=@yardName");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@yardName", model.yardName);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewYard", "Home");
        }

        public ActionResult ViewVessel()
        {
            List<Vessel> myVesselList = new List<Vessel>();
            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [VESSEL]");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Vessel y = new Vessel();
                        y.vesselName = reader["vesselName"].ToString();
                        y.containers = reader["containers"].ToString();
                        y.destinationYard = reader["destinationYard"].ToString();
                        y.arrivalYard = reader["arrivalYard"].ToString();
                        myVesselList.Add(y);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myVesselList);
        }

        public ActionResult EditVessel(string vesselName)
        {
            Vessel myVessel = new Vessel();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [VESSEL] WHERE vesselName=@vesselName");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@vesselName", vesselName);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        myVessel.vesselName = reader["vesselName"].ToString();
                        myVessel.containers = reader["containers"].ToString();
                        myVessel.destinationYard = reader["destinationYard"].ToString();
                        myVessel.arrivalYard = reader["arrivalYard"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myVessel);
        }

        [HttpPost]
        public ActionResult EditVessel(Vessel model)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [VESSEL] SET containers=@containers, destinationYard=@destinationYard, arrivalYard=@arrivalYard WHERE vesselName=@vesselName");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@containers", model.containers);
                    cmd.Parameters.AddWithValue("@destinationYard", model.destinationYard);
                    cmd.Parameters.AddWithValue("@arrivalYard", model.arrivalYard);
                    cmd.Parameters.AddWithValue("@vesselName", model.vesselName);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewVessel", "Home");
        }

        public ActionResult DeleteVessel(string vesselName)
        {
            Vessel myVessel = new Vessel();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [VESSEL] WHERE vesselName=@vesselName");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@vesselName", vesselName);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        myVessel.vesselName = reader["vesselName"].ToString();
                        myVessel.containers = reader["containers"].ToString();
                        myVessel.destinationYard = reader["destinationYard"].ToString();
                        myVessel.arrivalYard = reader["arrivalYard"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myVessel);
        }

        [HttpPost]
        public ActionResult DeleteVessel(Vessel model)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM [VESSEL] WHERE vesselName=@vesselName");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@vesselName", model.vesselName);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewVessel", "Home");
        }

        public ActionResult CreateSchedule()
        {
            Schedule mySchedule = new Schedule();
            mySchedule.yardList = new List<Yard>();
            mySchedule.cargoList = new List<Cargo>();
            mySchedule.vesselList = new List<Vessel>();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [YARD]");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Yard y = new Yard();
                        y.yardName = reader["yardName"].ToString();
                        y.yardLocation = reader["yardLocation"].ToString();
                        y.yardCountry = reader["yardCountry"].ToString();
                        mySchedule.yardList.Add(y);
                    }
                    reader.Close();
                    connection.Close();

                    cmd = new SqlCommand("SELECT * FROM [CARGO]");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Cargo y = new Cargo();
                        y.cargoId = reader["cargoId"].ToString();
                        y.cargoName = reader["cargoName"].ToString();
                        y.cargoDeparture = reader["cargoDeparture"].ToString();
                        y.cargoArrival = reader["cargoArrival"].ToString();
                        y.cargoVolume = reader["cargoVolume"].ToString();
                        mySchedule.cargoList.Add(y);
                    }
                    reader.Close();
                    connection.Close();

                    cmd = new SqlCommand("SELECT * FROM [VESSEL]");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Vessel y = new Vessel();
                        y.vesselName = reader["vesselName"].ToString();
                        y.containers = reader["containers"].ToString();
                        y.destinationYard = reader["destinationYard"].ToString();
                        y.arrivalYard = reader["arrivalYard"].ToString();
                        mySchedule.vesselList.Add(y);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(mySchedule);
        }

        [HttpPost]
        public ActionResult CreateSchedule(Schedule model)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [SCHEDULE] VALUES (@cargoId, @yardName, @vesselName, @departureDate, @arrivalDate, @scheduleId)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@cargoId", model.selectedCargoId);
                    cmd.Parameters.AddWithValue("@yardName", model.selectedYardName);
                    cmd.Parameters.AddWithValue("@vesselName", model.selectedVesselName);
                    cmd.Parameters.AddWithValue("@departureDate", model.departureDate);
                    cmd.Parameters.AddWithValue("@arrivalDate", model.arrivalDate);
                    cmd.Parameters.AddWithValue("@scheduleId", model.scheduleId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewSchedule()
        {
            List<Schedule> myScheduleList = new List<Schedule>();
            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [SCHEDULE]");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Schedule s = new Schedule();
                        s.selectedCargoId = reader["cargoId"].ToString();
                        s.selectedVesselName = reader["vesselName"].ToString();
                        s.selectedYardName = reader["yardName"].ToString();
                        s.departureDate = reader["departureDate"].ToString();
                        s.arrivalDate = reader["arrivalDate"].ToString();
                        s.scheduleId = reader["scheduleId"].ToString();
                        myScheduleList.Add(s);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myScheduleList);
        }

        public ActionResult EditSchedule(string scheduleId)
        {
            Schedule mySchedule = new Schedule();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [SCHEDULE] WHERE scheduleId=@scheduleId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@scheduleId", scheduleId);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        mySchedule.selectedCargoId = reader["cargoId"].ToString();
                        mySchedule.selectedVesselName = reader["vesselName"].ToString();
                        mySchedule.selectedYardName = reader["yardName"].ToString();
                        mySchedule.departureDate = reader["departureDate"].ToString();
                        mySchedule.arrivalDate = reader["arrivalDate"].ToString();
                        mySchedule.scheduleId = reader["scheduleId"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(mySchedule);
        }

        [HttpPost]
        public ActionResult EditSchedule(Schedule mySchedule)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [SCHEDULE] SET yardName=@yardName, vesselName=@vesselName, departureDate=@departureDate, arrivalDate=@arrivalDate WHERE scheduleId=@scheduleId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@scheduleId", mySchedule.scheduleId);
                    cmd.Parameters.AddWithValue("@yardName", mySchedule.selectedYardName);
                    cmd.Parameters.AddWithValue("@vesselName", mySchedule.selectedVesselName);
                    cmd.Parameters.AddWithValue("@departureDate", mySchedule.departureDate);
                    cmd.Parameters.AddWithValue("@arrivalDate", mySchedule.arrivalDate);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewSchedule", "Home");
        }

        public ActionResult DeleteSchedule(string scheduleId)
        {
            Schedule mySchedule = new Schedule();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [SCHEDULE] WHERE scheduleId=@scheduleId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@scheduleId", scheduleId);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        mySchedule.selectedCargoId = reader["cargoId"].ToString();
                        mySchedule.selectedVesselName = reader["vesselName"].ToString();
                        mySchedule.selectedYardName = reader["yardName"].ToString();
                        mySchedule.departureDate = reader["departureDate"].ToString();
                        mySchedule.arrivalDate = reader["arrivalDate"].ToString();
                        mySchedule.scheduleId = reader["scheduleId"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(mySchedule);
        }

        [HttpPost]
        public ActionResult DeleteSchedule(Schedule mySchedule)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM [SCHEDULE] WHERE scheduleId=@scheduleId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@scheduleId", mySchedule.scheduleId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ViewSchedule", "Home");
        }

        public ActionResult NewBooking()
        {
            List<Schedule> myScheduleList = new List<Schedule>();
            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [SCHEDULE]");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Schedule s = new Schedule();
                        s.selectedCargoId = reader["cargoId"].ToString();
                        s.selectedVesselName = reader["vesselName"].ToString();
                        s.selectedYardName = reader["yardName"].ToString();
                        s.departureDate = reader["departureDate"].ToString();
                        s.arrivalDate = reader["arrivalDate"].ToString();
                        s.scheduleId = reader["scheduleId"].ToString();
                        myScheduleList.Add(s);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myScheduleList);
        }

        public ActionResult CreateBooking(string scheduleId)
        {
            Booking myBooking = new Booking();

            myBooking.scheduleId = scheduleId;

            return View(myBooking);
        }

        [HttpPost]
        public ActionResult CreateBooking(Booking myBooking)
        {
            myBooking.bookingDate = DateTime.Now.ToString();
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [BOOKING] VALUES (@bookingId, @scheduleId, @bookingDate)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@bookingId", myBooking.bookingId);
                    cmd.Parameters.AddWithValue("@scheduleId", myBooking.scheduleId);
                    cmd.Parameters.AddWithValue("@bookingDate", myBooking.bookingDate);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CancelBooking()
        {
            List<Booking> myBookingList = new List<Booking>();
            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [BOOKING]");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking b = new Booking();
                        b.bookingId = reader["bookingId"].ToString();
                        b.scheduleId = reader["scheduleId"].ToString();
                        b.bookingDate = reader["bookingDate"].ToString();
                        myBookingList.Add(b);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myBookingList);
        }

        public ActionResult CancelMyBooking(string bookingId)
        {
            Booking myBooking = new Booking();

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [BOOKING] WHERE bookingId=@bookingId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@bookingId", bookingId);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        myBooking.bookingId = reader["bookingId"].ToString();
                        myBooking.scheduleId = reader["scheduleId"].ToString();
                        myBooking.bookingDate = reader["bookingDate"].ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(myBooking);
        }

        [HttpPost]
        public ActionResult CancelMyBooking(Booking myBooking)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM [BOOKING] WHERE bookingId=@bookingId");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@bookingId", myBooking.bookingId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("CancelBooking", "Home");
        }
    }
}