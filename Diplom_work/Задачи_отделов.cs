//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Diplom_work
{
    using System;
    using System.Collections.Generic;
    
    public partial class Задачи_отделов
    {
        public int id { get; set; }
        public Nullable<int> id_отдела { get; set; }
        public string Задача { get; set; }
        public string Статус_задачи { get; set; }
        public int Сотрудник { get; set; }
    
        public virtual Отделы_предприятия Отделы_предприятия { get; set; }
        public virtual Сотрудники_отделов Сотрудники_отделов { get; set; }
        public virtual Статус_готовности Статус_готовности { get; set; }
    }
}
