using System.Collections.Generic;

namespace AspNetCore_enviaEmail.Models
{
    public class EmailSettings
    {
        public Host Hosts { get; set; }
        public int Port { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }

    }

    public class Host
    {
        public string Gmail { get; set; }
        public string Hotmail { get; set; }
        public string Yahoo { get; set; }
        
    }
}
