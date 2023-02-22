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

        [HttpPost("createMember")]
        public void CreateMember(string name, long memberId)
        {
            try
            {
                string conString = _configuration.GetConnectionString("MembershipApp");
                MySqlConnection conn = new MySqlConnection(conString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO member (name,memberId) VALUES (@name,@memberID)", conn);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("memberID", memberId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Get specific member
        [HttpGet("getmember")]
        public Member GetMember(long memberId)
        {
            try
            {
                string conString = _configuration.GetConnectionString("MembershipApp");
                MySqlConnection conn = new MySqlConnection(conString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM member WHERE memberId=@memberId",conn);
                cmd.Parameters.AddWithValue("@memberId", memberId);
                MySqlDataReader dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    return new Member((string)dReader[1], (long)dReader[2], (int)dReader[3], (string)dReader[4]); 
                }

                dReader.Close();
                conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new Member("non found",0);
        }

        [HttpPost("setmembertype")]
        public void SetMembershiptype(long memberId, string membershipType)
        {
            try
            {
                String conString = _configuration.GetConnectionString("MembershipApp");
                MySqlConnection conn = new MySqlConnection(conString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE member SET memberType=@membershipType WHERE memberId=@memberId", conn);
                cmd.Parameters.AddWithValue("@membershipType",membershipType);
                cmd.Parameters.AddWithValue("@memberId", memberId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Set amount of points of a member
        [HttpPost("setpoints")]
        public void SetPoints(int points, long memberId)
        {
            try
            {
                String conString = _configuration.GetConnectionString("MembershipApp");
                MySqlConnection conn = new MySqlConnection(conString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM member WHERE memberId=@memberID", conn);
                cmd.Parameters.AddWithValue("@memberID", memberId);
                cmd.Prepare();
                MySqlDataReader dReader = cmd.ExecuteReader();
                long tmpMemberId = 0;
                int tmpPoints = 0;

                while (dReader.Read())
                {
                    tmpMemberId = (long) dReader[2];
                    tmpPoints = (int) dReader[3];
                }
                dReader.Close();

                if (tmpPoints + points >= 0) {
                    int newPoints = tmpPoints + points;
                    Console.WriteLine(newPoints);
                    MySqlCommand cmd1 = new MySqlCommand("UPDATE member SET points=@newPoints WHERE memberId=@memberID", conn);
                    cmd1.Parameters.AddWithValue("@memberID", memberId);
                    cmd1.Parameters.AddWithValue("@newPoints", newPoints);
                    cmd1.Prepare();
                    cmd1.ExecuteNonQuery();
                }

                conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Get all members in database tables
        [HttpGet("members")]
        public List<Member> Get()
        {

            List<Member> list = new List<Member>();

            try
            {
                string conString = _configuration.GetConnectionString("MembershipApp");
                MySqlConnection conn = new MySqlConnection(conString);
                conn.Open();

                string sqlCommand = "SELECT * FROM member";
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                MySqlDataReader dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    list.Add(new Member((string)dReader[1], (long)dReader[2])); //  name, memberid
                }
                dReader.Close();
                
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return list;
        }
    }
}
