using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class UserBs
    {
        private UserDb ObjDb;

        public UserBs()
        {
            ObjDb = new UserDb();
        }

        public List<string> Errors = new List<string>();

        public List<Users> GetAllUser()
        {
            return ObjDb.GetAllUser();
        }

        public bool InsertUser(Users u)
        {
            try
            {
                ObjDb.InsertUser(u);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
