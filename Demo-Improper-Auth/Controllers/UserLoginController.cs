using Microsoft.AspNetCore.Mvc;
using Demo_Improper_Auth.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using NuGet.Protocol.Plugins;
using Microsoft.Extensions.Configuration;

namespace Demo_Improper_Auth.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserLoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginClass lc)
        {
            // Boolean User = InsecureLogin(lc.UserName, lc.Password);
            Boolean User = SecureLogin(lc.Username, lc.Pwd);


            if (User == true)
                return RedirectToAction("Welcome");
            else
                return View();

        }
        public Boolean SecureLogin(string Username, string Pwd)
        {
            string constr = _configuration.GetConnectionString("MyConn");

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT Username,Pwd FROM users where Username=@Username and Pwd=@Pwd";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", Username.ToString());
                    cmd.Parameters.AddWithValue("@Pwd", Pwd.ToString());
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {

                        ViewData["Message"] = "Invalid User";
                        return false;
                    }

                    //  }
                }

            }

        }
        public IActionResult Welcome()
        {
            return View();
        }

        /*[HttpPost]
        public IActionResult Index(LoginClass lc)
        {
            string mainconn = "Data Source=ADMINMACHINE\\SQLEXPRESS;Initial Catalog=CaseDemo;Integrated Security=True";
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                string sqlquery = "select Uemail, Pwd from users where Uemail=@Uemail and Pwd=@Pwd";
                using (SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn))
                {
                    sqlconn.Open();
                    sqlcomm.Parameters.AddWithValue("@Uemail", lc.Username);
                    sqlcomm.Parameters.AddWithValue("@Pwd", lc.Pwd);
                    SqlDataReader sdr = sqlcomm.ExecuteReader();
                    if (sdr.Read())
                    {
                       Session["username"] = lc.Username.ToString();
                        return RedirectToAction("Welcome");
                    }
                    else
                    {
                        ViewData["Message"] = "Invalid Login Attempt!";
                    }
                    sqlcomm.ExecuteNonQuery();
                    sqlconn.Close();
                }
            }
            return View();
        }*/
    }
}
