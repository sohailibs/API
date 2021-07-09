using BLL;
using BOL;
using DPS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.Http;

namespace DPS.Controllers
{
    public class TestController : ApiController
    {
        private UserBs _userBs;
        private Common _common;

        public TestController()
        {
            _userBs = new UserBs();
            _common = new Common();
           
        }

        //

        [HttpGet]
        public ResponseObj<List<Users>> GetAllUser()
        {
            ResponseObj<List<Users>> responseObj = new ResponseObj<List<Users>>();

            try
            {
                var lang = Request.Headers.GetValues("Accept-Language").First();
                var _userList = _userBs.GetAllUser();
                responseObj.Data = _userList;
                responseObj.isSuccess = true;
                responseObj.Message = lang == "En" ? "List of User" : "List of User - Ar";
            }
            catch (Exception ex)
            {
                responseObj.Message = ex.Message;
                responseObj.isSuccess = false;
                _common.ExeceptionHandleWithMethodName(ex, "GetAllUser");
            }
            return responseObj;
        }

        [HttpPost]
        public ResponseObj<Users> UserRegister(Users obj)
        {
            ResponseObj<Users> returnObj = new ResponseObj<Users>();
            Users _u = new Users();
            try
            {
               
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(obj.Email);

                if(obj != null)
                {
                    _u = obj;
                    bool userObj = _userBs.InsertUser(_u);


                    if (userObj)
                    {
                        string RootPath = HostingEnvironment.MapPath("~");
                        var EmailTemplate = (dynamic)null;
                        string Subject = "Your are now registered with BIT Coin";

                        string textFile = RootPath + "/EmailTemplate/register.html";

                        if (File.Exists(textFile))
                        {
                            string text = File.ReadAllText(textFile);
                            text = text.Replace("%Name%", obj.Name);
                            EmailTemplate = text;
                            EmailConfiguration(obj.Email, Subject, EmailTemplate, "", new List<string>());
                        }

                        returnObj.Data = _u;
                        returnObj.isSuccess = true;
                        returnObj.Message = "User Created Successfully";
                    }
                    else
                    {

                    }

                }
                    

                          
                    
                

            }
            catch (Exception ex)
            {
                throw;
            }
            return returnObj;
        }

        public void EmailConfiguration(string to, string subject, string body, string Attachment, List<string> cc)
        {
            try
            {
                string Port = ConfigurationManager.AppSettings["Port"];
                string From = ConfigurationManager.AppSettings["From"];
                string SMTP = ConfigurationManager.AppSettings["SMTP"];
                string UserName = ConfigurationManager.AppSettings["UserName"];
                string Password = ConfigurationManager.AppSettings["Password"];

                using (MailMessage mm = new MailMessage(From, to))
                {
                    foreach (var item in cc)
                    {
                        mm.CC.Add(item);
                    }

                    mm.Subject = subject;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = SMTP;
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(UserName, Password);
                    //smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = Convert.ToInt32(Port);
                    smtp.Send(mm);
                }
            }
            catch (Exception ex)
            {

            }

        }



    }
}
