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
    
    public partial class content_over_8000
    {
        public int id { get; set; }
        public System.Guid shar_id { get; set; }
        public byte[] file_data { get; set; }
    
        public virtual shares shares { get; set; }
    }
}
