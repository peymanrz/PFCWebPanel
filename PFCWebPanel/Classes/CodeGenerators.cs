using System;
using System.Collections.Generic;
using System.Text;

namespace PFCWebPanel.Classes
{
   public class CodeGenerators
    {
        public static string ActiveCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
        }
    }
}
