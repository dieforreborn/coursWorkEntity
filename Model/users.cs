//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace courseWorkEntity.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class users
    {
        public int idUser { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string nameUser { get; set; }
        public string surnameUser { get; set; }
        public string patronymicUser { get; set; }
        public string sexUser { get; set; }
        public string phoneNumber { get; set; }
    }
}