using Demo_Improper_Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Security.Claims;
//using Microsoft.Data;
//using Microsoft.Data.SqlClients;
//using UserReg.Models;
using System.Data.SqlClient;
using BC= BCrypt.Net.BCrypt;


namespace Demo_Improper_Auth.Controllers
{
    public class UserRegController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserRegController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        private string doHashedPassword(string pwd)
        {

            var hashedpwd = BC.HashPassword(pwd);
            return hashedpwd;
        }
        /*public PasswordVerificationResult VerifyHashedPassword(UserReg customer,
                        string hashedpwd, string password)
        {
            if (BC.Verify(password, hashedpwd))
                return PasswordVerificationResult.Success;
            else return PasswordVerificationResult.Failed;
        }            
        */
            /*using (var md5 = new MD5CryptoServiceProvider())
            {
                var hashedByte = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwd));
                var hashedpwd = BitConverter.ToString(hashedByte).Replace("-", "").ToLower();
                return hashedpwd;
            }*/
            
            /*string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string salted = pwd + md5Salt;
            string hashed = salted.MD5();*/
            
        

        public IActionResult Create()
        {
            return View();
        }
        /*public static void Main(UserReg ur)
        {
            // declaring key
            var key = "b14ca5898a4e4133bbce2ea2315a1916";

            // encrypt parameters
            var input = ur.Pwd;
        }

        public static string EncryptString(string key, string plainInput)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainInput);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            hashedpwd = Convert.ToBase64String(array);

            return hashedpwd;
        }*/

            [HttpPost]
        public IActionResult Create(UserReg ur)
        {
            string pasword = doHashedPassword(ur.Pwd);
            string connection = "Data Source=ADMIN-MACHINE-1;Initial Catalog=NewDb;Integrated Security=True";

            using (SqlConnection sqlconn = new SqlConnection(connection))
            {
                string sqlquery = "insert into users(Username,Uemail,Pwd,Gender) values ('" + ur.Username + "','" + ur.Uemail + "','" + pasword+ "','" + ur.Gender + "')";
                using (SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn))
                {
                    sqlconn.Open();
                    sqlcomm.ExecuteNonQuery();
                    ViewData["Message"] = "User is saved sucessfully";
                }
            }
            return View(ur);
        }
    }
}
