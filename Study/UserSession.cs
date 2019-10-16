using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Study
{
    [Serializable]
    public class UserSession
    {
       
        public long UserID { set; get; }
        public String UserName { set; get; }
        public String PassWord { set; get; }

 
    }
}