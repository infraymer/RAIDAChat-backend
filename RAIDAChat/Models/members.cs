//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RAIDAChat.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class members
    {
        public System.Guid public_id { get; set; }
        public System.Guid an { get; set; }
        public System.Guid private_id { get; set; }
        public System.DateTime month_last_use { get; set; }
        public System.Guid org_id { get; set; }
        public string description_fragment { get; set; }
        public byte[] photo_fragment { get; set; }
        public int kb_bandwidth_used { get; set; }
        public string away_busy_ready { get; set; }
        public string login { get; set; }
    }
}
