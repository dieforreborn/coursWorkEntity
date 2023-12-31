﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class colledgeDepartmentEntities : DbContext
    {
        public colledgeDepartmentEntities()
            : base("name=colledgeDepartmentEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<disciplines> disciplines { get; set; }
        public virtual DbSet<evaluations> evaluations { get; set; }
        public virtual DbSet<groups> groups { get; set; }
        public virtual DbSet<history> history { get; set; }
        public virtual DbSet<specialities> specialities { get; set; }
        public virtual DbSet<students> students { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<collegdeSexView> collegdeSexView { get; set; }
    
        public virtual int addNewStudent(string surname, string name, string patromic, string idGroup, string sex, string dateOfBirth)
        {
            var surnameParameter = surname != null ?
                new ObjectParameter("surname", surname) :
                new ObjectParameter("surname", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var patromicParameter = patromic != null ?
                new ObjectParameter("patromic", patromic) :
                new ObjectParameter("patromic", typeof(string));
    
            var idGroupParameter = idGroup != null ?
                new ObjectParameter("idGroup", idGroup) :
                new ObjectParameter("idGroup", typeof(string));
    
            var sexParameter = sex != null ?
                new ObjectParameter("sex", sex) :
                new ObjectParameter("sex", typeof(string));
    
            var dateOfBirthParameter = dateOfBirth != null ?
                new ObjectParameter("dateOfBirth", dateOfBirth) :
                new ObjectParameter("dateOfBirth", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("addNewStudent", surnameParameter, nameParameter, patromicParameter, idGroupParameter, sexParameter, dateOfBirthParameter);
        }
    
        public virtual ObjectResult<countSexColledge_Result> countSexColledge()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<countSexColledge_Result>("countSexColledge");
        }
    
        public virtual ObjectResult<countSexGroup_Result> countSexGroup(string nameGroup)
        {
            var nameGroupParameter = nameGroup != null ?
                new ObjectParameter("nameGroup", nameGroup) :
                new ObjectParameter("nameGroup", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<countSexGroup_Result>("countSexGroup", nameGroupParameter);
        }
    
        public virtual int deleteStudent(Nullable<int> studentId)
        {
            var studentIdParameter = studentId.HasValue ?
                new ObjectParameter("studentId", studentId) :
                new ObjectParameter("studentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("deleteStudent", studentIdParameter);
        }
    
        public virtual int editNewStudent(Nullable<int> idStudent, string surname, string name, string patronymic, string newGroup, string sex, string dateOfBirth)
        {
            var idStudentParameter = idStudent.HasValue ?
                new ObjectParameter("idStudent", idStudent) :
                new ObjectParameter("idStudent", typeof(int));
    
            var surnameParameter = surname != null ?
                new ObjectParameter("surname", surname) :
                new ObjectParameter("surname", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var patronymicParameter = patronymic != null ?
                new ObjectParameter("patronymic", patronymic) :
                new ObjectParameter("patronymic", typeof(string));
    
            var newGroupParameter = newGroup != null ?
                new ObjectParameter("newGroup", newGroup) :
                new ObjectParameter("newGroup", typeof(string));
    
            var sexParameter = sex != null ?
                new ObjectParameter("sex", sex) :
                new ObjectParameter("sex", typeof(string));
    
            var dateOfBirthParameter = dateOfBirth != null ?
                new ObjectParameter("dateOfBirth", dateOfBirth) :
                new ObjectParameter("dateOfBirth", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("editNewStudent", idStudentParameter, surnameParameter, nameParameter, patronymicParameter, newGroupParameter, sexParameter, dateOfBirthParameter);
        }
    
        public virtual ObjectResult<getEvalutiionColledge_Result> getEvalutiionColledge()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getEvalutiionColledge_Result>("getEvalutiionColledge");
        }
    
        public virtual ObjectResult<getEvalutiionGroup_Result> getEvalutiionGroup(string idGroup)
        {
            var idGroupParameter = idGroup != null ?
                new ObjectParameter("idGroup", idGroup) :
                new ObjectParameter("idGroup", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getEvalutiionGroup_Result>("getEvalutiionGroup", idGroupParameter);
        }
    
        public virtual ObjectResult<getReportAllGroup_Result> getReportAllGroup(string idGroup)
        {
            var idGroupParameter = idGroup != null ?
                new ObjectParameter("idGroup", idGroup) :
                new ObjectParameter("idGroup", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getReportAllGroup_Result>("getReportAllGroup", idGroupParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int transGroup(string oldGroup, string newGroup)
        {
            var oldGroupParameter = oldGroup != null ?
                new ObjectParameter("oldGroup", oldGroup) :
                new ObjectParameter("oldGroup", typeof(string));
    
            var newGroupParameter = newGroup != null ?
                new ObjectParameter("newGroup", newGroup) :
                new ObjectParameter("newGroup", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("transGroup", oldGroupParameter, newGroupParameter);
        }
    
        public virtual int transGroupStudent(Nullable<int> idStudent, string oldGroup, string newGroup)
        {
            var idStudentParameter = idStudent.HasValue ?
                new ObjectParameter("idStudent", idStudent) :
                new ObjectParameter("idStudent", typeof(int));
    
            var oldGroupParameter = oldGroup != null ?
                new ObjectParameter("oldGroup", oldGroup) :
                new ObjectParameter("oldGroup", typeof(string));
    
            var newGroupParameter = newGroup != null ?
                new ObjectParameter("newGroup", newGroup) :
                new ObjectParameter("newGroup", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("transGroupStudent", idStudentParameter, oldGroupParameter, newGroupParameter);
        }
    
        public virtual ObjectResult<getDisciplineStatistics_Result> getDisciplineStatistics(string disciplineName)
        {
            var disciplineNameParameter = disciplineName != null ?
                new ObjectParameter("DisciplineName", disciplineName) :
                new ObjectParameter("DisciplineName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDisciplineStatistics_Result>("getDisciplineStatistics", disciplineNameParameter);
        }
    }
}
