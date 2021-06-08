using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Service
{
    public class FileInfos : FileInformation
    {
        public override string ToString()
        {
            return $"{Name}+{ ContentLength}+{ContentType}";
        }
    }
    
}