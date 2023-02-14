using Membership.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;

namespace Membership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MembershipController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("members")]
        public List<Member> Get()
        {
          
            string conString = _configuration.GetConnectionString("MembershipApp");
            MySqlConnection conn = new MySqlConnection(conString);
            List<Member> list = new List<Member>(); 

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sqlCommand = "SELECT * FROM member";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    list.Add(new Member((int)rdr[0], (string)rdr[1], (int)rdr[2]));
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");

            return list;
        }
    }
}
