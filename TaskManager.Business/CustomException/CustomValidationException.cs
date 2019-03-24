using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business
{
    public class CustomValidationException:Exception
    {
        
        private Dictionary<string, string> errpCollection;
        public Dictionary<string, string> ErrorCollection { get { return errpCollection; } }
        public CustomValidationException()
        {
            errpCollection = new Dictionary<string, string>();
        }

        public void AddException(string key,string message)
        {
            errpCollection.Add(key, message);
        }




    }
}
