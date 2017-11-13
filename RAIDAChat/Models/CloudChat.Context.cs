﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CloudChatEntities : DbContext
    {
        public CloudChatEntities()
            : base("name=CloudChatEntities")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CloudChatEntities>());
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<content_under_8000> content_under_8000 { get; set; }
        public virtual DbSet<group_members> group_members { get; set; }
        public virtual DbSet<groups> groups { get; set; }
        public virtual DbSet<members> members { get; set; }
        public virtual DbSet<shares> shares { get; set; }
        public virtual DbSet<organizations> organizations { get; set; }
        public virtual DbSet<content_over_8000> content_over_8000 { get; set; }
    
        public virtual int usp_content_over_8000Delete(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_content_over_8000Delete", idParameter);
        }
    
        public virtual ObjectResult<usp_content_over_8000Insert_Result> usp_content_over_8000Insert(Nullable<int> id, Nullable<System.Guid> share_id, byte[] file_data)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var share_idParameter = share_id.HasValue ?
                new ObjectParameter("share_id", share_id) :
                new ObjectParameter("share_id", typeof(System.Guid));
    
            var file_dataParameter = file_data != null ?
                new ObjectParameter("file_data", file_data) :
                new ObjectParameter("file_data", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_content_over_8000Insert_Result>("usp_content_over_8000Insert", idParameter, share_idParameter, file_dataParameter);
        }
    
        public virtual ObjectResult<usp_content_over_8000Select_Result> usp_content_over_8000Select(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_content_over_8000Select_Result>("usp_content_over_8000Select", idParameter);
        }
    
        public virtual ObjectResult<usp_content_over_8000Update_Result> usp_content_over_8000Update(Nullable<int> id, Nullable<int> shar_id, byte[] file_data)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var shar_idParameter = shar_id.HasValue ?
                new ObjectParameter("shar_id", shar_id) :
                new ObjectParameter("shar_id", typeof(int));
    
            var file_dataParameter = file_data != null ?
                new ObjectParameter("file_data", file_data) :
                new ObjectParameter("file_data", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_content_over_8000Update_Result>("usp_content_over_8000Update", idParameter, shar_idParameter, file_dataParameter);
        }
    
        public virtual int usp_content_under_8000Delete(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_content_under_8000Delete", idParameter);
        }
    
        public virtual ObjectResult<usp_content_under_8000Insert_Result> usp_content_under_8000Insert(Nullable<int> id, byte[] file_data)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var file_dataParameter = file_data != null ?
                new ObjectParameter("file_data", file_data) :
                new ObjectParameter("file_data", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_content_under_8000Insert_Result>("usp_content_under_8000Insert", idParameter, file_dataParameter);
        }
    
        public virtual ObjectResult<usp_content_under_8000Select_Result> usp_content_under_8000Select(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_content_under_8000Select_Result>("usp_content_under_8000Select", idParameter);
        }
    
        public virtual ObjectResult<usp_content_under_8000Update_Result> usp_content_under_8000Update(Nullable<int> id, Nullable<int> share_id, byte[] file_data)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var share_idParameter = share_id.HasValue ?
                new ObjectParameter("share_id", share_id) :
                new ObjectParameter("share_id", typeof(int));
    
            var file_dataParameter = file_data != null ?
                new ObjectParameter("file_data", file_data) :
                new ObjectParameter("file_data", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_content_under_8000Update_Result>("usp_content_under_8000Update", idParameter, share_idParameter, file_dataParameter);
        }
    
        public virtual int usp_group_membersDelete(Nullable<System.Guid> group_id, Nullable<System.Guid> member_id)
        {
            var group_idParameter = group_id.HasValue ?
                new ObjectParameter("group_id", group_id) :
                new ObjectParameter("group_id", typeof(System.Guid));
    
            var member_idParameter = member_id.HasValue ?
                new ObjectParameter("member_id", member_id) :
                new ObjectParameter("member_id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_group_membersDelete", group_idParameter, member_idParameter);
        }
    
        public virtual ObjectResult<usp_group_membersInsert_Result> usp_group_membersInsert(Nullable<System.Guid> group_id, Nullable<System.Guid> member_id, string allow_or_deny)
        {
            var group_idParameter = group_id.HasValue ?
                new ObjectParameter("group_id", group_id) :
                new ObjectParameter("group_id", typeof(System.Guid));
    
            var member_idParameter = member_id.HasValue ?
                new ObjectParameter("member_id", member_id) :
                new ObjectParameter("member_id", typeof(System.Guid));
    
            var allow_or_denyParameter = allow_or_deny != null ?
                new ObjectParameter("allow_or_deny", allow_or_deny) :
                new ObjectParameter("allow_or_deny", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_group_membersInsert_Result>("usp_group_membersInsert", group_idParameter, member_idParameter, allow_or_denyParameter);
        }
    
        public virtual ObjectResult<usp_group_membersSelect_Result> usp_group_membersSelect(Nullable<System.Guid> group_id, Nullable<System.Guid> member_id)
        {
            var group_idParameter = group_id.HasValue ?
                new ObjectParameter("group_id", group_id) :
                new ObjectParameter("group_id", typeof(System.Guid));
    
            var member_idParameter = member_id.HasValue ?
                new ObjectParameter("member_id", member_id) :
                new ObjectParameter("member_id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_group_membersSelect_Result>("usp_group_membersSelect", group_idParameter, member_idParameter);
        }
    
        public virtual ObjectResult<usp_group_membersUpdate_Result> usp_group_membersUpdate(Nullable<System.Guid> group_id, Nullable<System.Guid> member_id, string allow_or_deny)
        {
            var group_idParameter = group_id.HasValue ?
                new ObjectParameter("group_id", group_id) :
                new ObjectParameter("group_id", typeof(System.Guid));
    
            var member_idParameter = member_id.HasValue ?
                new ObjectParameter("member_id", member_id) :
                new ObjectParameter("member_id", typeof(System.Guid));
    
            var allow_or_denyParameter = allow_or_deny != null ?
                new ObjectParameter("allow_or_deny", allow_or_deny) :
                new ObjectParameter("allow_or_deny", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_group_membersUpdate_Result>("usp_group_membersUpdate", group_idParameter, member_idParameter, allow_or_denyParameter);
        }
    
        public virtual int usp_groupsDelete(Nullable<System.Guid> group_id)
        {
            var group_idParameter = group_id.HasValue ?
                new ObjectParameter("group_id", group_id) :
                new ObjectParameter("group_id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_groupsDelete", group_idParameter);
        }
    
        public virtual ObjectResult<usp_groupsInsert_Result> usp_groupsInsert(Nullable<System.Guid> group_id, string group_name_part, Nullable<System.Guid> owner_private_id, string photo_fragment, Nullable<System.Guid> org_private_id)
        {
            var group_idParameter = group_id.HasValue ?
                new ObjectParameter("group_id", group_id) :
                new ObjectParameter("group_id", typeof(System.Guid));
    
            var group_name_partParameter = group_name_part != null ?
                new ObjectParameter("group_name_part", group_name_part) :
                new ObjectParameter("group_name_part", typeof(string));
    
            var owner_private_idParameter = owner_private_id.HasValue ?
                new ObjectParameter("owner_private_id", owner_private_id) :
                new ObjectParameter("owner_private_id", typeof(System.Guid));
    
            var photo_fragmentParameter = photo_fragment != null ?
                new ObjectParameter("photo_fragment", photo_fragment) :
                new ObjectParameter("photo_fragment", typeof(string));
    
            var org_private_idParameter = org_private_id.HasValue ?
                new ObjectParameter("org_private_id", org_private_id) :
                new ObjectParameter("org_private_id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_groupsInsert_Result>("usp_groupsInsert", group_idParameter, group_name_partParameter, owner_private_idParameter, photo_fragmentParameter, org_private_idParameter);
        }
    
        public virtual ObjectResult<usp_groupsSelect_Result> usp_groupsSelect(Nullable<System.Guid> group_id)
        {
            var group_idParameter = group_id.HasValue ?
                new ObjectParameter("group_id", group_id) :
                new ObjectParameter("group_id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_groupsSelect_Result>("usp_groupsSelect", group_idParameter);
        }
    
        public virtual ObjectResult<usp_groupsUpdate_Result> usp_groupsUpdate(Nullable<System.Guid> group_id, string group_name_part, Nullable<System.Guid> owner_private_id, string photo_fragment, Nullable<System.Guid> org_private_id)
        {
            var group_idParameter = group_id.HasValue ?
                new ObjectParameter("group_id", group_id) :
                new ObjectParameter("group_id", typeof(System.Guid));
    
            var group_name_partParameter = group_name_part != null ?
                new ObjectParameter("group_name_part", group_name_part) :
                new ObjectParameter("group_name_part", typeof(string));
    
            var owner_private_idParameter = owner_private_id.HasValue ?
                new ObjectParameter("owner_private_id", owner_private_id) :
                new ObjectParameter("owner_private_id", typeof(System.Guid));
    
            var photo_fragmentParameter = photo_fragment != null ?
                new ObjectParameter("photo_fragment", photo_fragment) :
                new ObjectParameter("photo_fragment", typeof(string));
    
            var org_private_idParameter = org_private_id.HasValue ?
                new ObjectParameter("org_private_id", org_private_id) :
                new ObjectParameter("org_private_id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_groupsUpdate_Result>("usp_groupsUpdate", group_idParameter, group_name_partParameter, owner_private_idParameter, photo_fragmentParameter, org_private_idParameter);
        }
    
        public virtual int usp_membersDelete(Nullable<System.Guid> public_id)
        {
            var public_idParameter = public_id.HasValue ?
                new ObjectParameter("public_id", public_id) :
                new ObjectParameter("public_id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_membersDelete", public_idParameter);
        }
    
        public virtual ObjectResult<usp_membersInsert_Result> usp_membersInsert(Nullable<System.Guid> public_id, Nullable<System.Guid> an, Nullable<System.Guid> private_id, Nullable<System.DateTime> month_last_use, Nullable<System.Guid> org_id, string description_fragment, byte[] photo_fragment, Nullable<int> kb_bandwidth_used, string away_busy_ready)
        {
            var public_idParameter = public_id.HasValue ?
                new ObjectParameter("public_id", public_id) :
                new ObjectParameter("public_id", typeof(System.Guid));
    
            var anParameter = an.HasValue ?
                new ObjectParameter("an", an) :
                new ObjectParameter("an", typeof(System.Guid));
    
            var private_idParameter = private_id.HasValue ?
                new ObjectParameter("private_id", private_id) :
                new ObjectParameter("private_id", typeof(System.Guid));
    
            var month_last_useParameter = month_last_use.HasValue ?
                new ObjectParameter("month_last_use", month_last_use) :
                new ObjectParameter("month_last_use", typeof(System.DateTime));
    
            var org_idParameter = org_id.HasValue ?
                new ObjectParameter("org_id", org_id) :
                new ObjectParameter("org_id", typeof(System.Guid));
    
            var description_fragmentParameter = description_fragment != null ?
                new ObjectParameter("description_fragment", description_fragment) :
                new ObjectParameter("description_fragment", typeof(string));
    
            var photo_fragmentParameter = photo_fragment != null ?
                new ObjectParameter("photo_fragment", photo_fragment) :
                new ObjectParameter("photo_fragment", typeof(byte[]));
    
            var kb_bandwidth_usedParameter = kb_bandwidth_used.HasValue ?
                new ObjectParameter("kb_bandwidth_used", kb_bandwidth_used) :
                new ObjectParameter("kb_bandwidth_used", typeof(int));
    
            var away_busy_readyParameter = away_busy_ready != null ?
                new ObjectParameter("away_busy_ready", away_busy_ready) :
                new ObjectParameter("away_busy_ready", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_membersInsert_Result>("usp_membersInsert", public_idParameter, anParameter, private_idParameter, month_last_useParameter, org_idParameter, description_fragmentParameter, photo_fragmentParameter, kb_bandwidth_usedParameter, away_busy_readyParameter);
        }
    
        public virtual ObjectResult<usp_membersSelect_Result> usp_membersSelect(Nullable<System.Guid> public_id)
        {
            var public_idParameter = public_id.HasValue ?
                new ObjectParameter("public_id", public_id) :
                new ObjectParameter("public_id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_membersSelect_Result>("usp_membersSelect", public_idParameter);
        }
    
        public virtual ObjectResult<usp_membersUpdate_Result> usp_membersUpdate(Nullable<System.Guid> public_id, Nullable<System.Guid> an, Nullable<System.Guid> private_id, Nullable<System.DateTime> month_last_use, Nullable<System.Guid> org_id, string description_fragment, byte[] photo_fragment, Nullable<int> kb_bandwidth_used, string away_busy_ready)
        {
            var public_idParameter = public_id.HasValue ?
                new ObjectParameter("public_id", public_id) :
                new ObjectParameter("public_id", typeof(System.Guid));
    
            var anParameter = an.HasValue ?
                new ObjectParameter("an", an) :
                new ObjectParameter("an", typeof(System.Guid));
    
            var private_idParameter = private_id.HasValue ?
                new ObjectParameter("private_id", private_id) :
                new ObjectParameter("private_id", typeof(System.Guid));
    
            var month_last_useParameter = month_last_use.HasValue ?
                new ObjectParameter("month_last_use", month_last_use) :
                new ObjectParameter("month_last_use", typeof(System.DateTime));
    
            var org_idParameter = org_id.HasValue ?
                new ObjectParameter("org_id", org_id) :
                new ObjectParameter("org_id", typeof(System.Guid));
    
            var description_fragmentParameter = description_fragment != null ?
                new ObjectParameter("description_fragment", description_fragment) :
                new ObjectParameter("description_fragment", typeof(string));
    
            var photo_fragmentParameter = photo_fragment != null ?
                new ObjectParameter("photo_fragment", photo_fragment) :
                new ObjectParameter("photo_fragment", typeof(byte[]));
    
            var kb_bandwidth_usedParameter = kb_bandwidth_used.HasValue ?
                new ObjectParameter("kb_bandwidth_used", kb_bandwidth_used) :
                new ObjectParameter("kb_bandwidth_used", typeof(int));
    
            var away_busy_readyParameter = away_busy_ready != null ?
                new ObjectParameter("away_busy_ready", away_busy_ready) :
                new ObjectParameter("away_busy_ready", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_membersUpdate_Result>("usp_membersUpdate", public_idParameter, anParameter, private_idParameter, month_last_useParameter, org_idParameter, description_fragmentParameter, photo_fragmentParameter, kb_bandwidth_usedParameter, away_busy_readyParameter);
        }
    
        public virtual int usp_sharesDelete(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_sharesDelete", idParameter);
        }
    
        public virtual ObjectResult<usp_sharesInsert_Result> usp_sharesInsert(Nullable<System.Guid> member_public, Nullable<System.Guid> owner_private, Nullable<System.Guid> org_public, Nullable<System.Guid> org_private, Nullable<System.Guid> to_public, Nullable<System.DateTime> death_date, Nullable<int> kb_size, string file_extention, string self_one_or_group)
        {
            var member_publicParameter = member_public.HasValue ?
                new ObjectParameter("member_public", member_public) :
                new ObjectParameter("member_public", typeof(System.Guid));
    
            var owner_privateParameter = owner_private.HasValue ?
                new ObjectParameter("owner_private", owner_private) :
                new ObjectParameter("owner_private", typeof(System.Guid));
    
            var org_publicParameter = org_public.HasValue ?
                new ObjectParameter("org_public", org_public) :
                new ObjectParameter("org_public", typeof(System.Guid));
    
            var org_privateParameter = org_private.HasValue ?
                new ObjectParameter("org_private", org_private) :
                new ObjectParameter("org_private", typeof(System.Guid));
    
            var to_publicParameter = to_public.HasValue ?
                new ObjectParameter("to_public", to_public) :
                new ObjectParameter("to_public", typeof(System.Guid));
    
            var death_dateParameter = death_date.HasValue ?
                new ObjectParameter("death_date", death_date) :
                new ObjectParameter("death_date", typeof(System.DateTime));
    
            var kb_sizeParameter = kb_size.HasValue ?
                new ObjectParameter("kb_size", kb_size) :
                new ObjectParameter("kb_size", typeof(int));
    
            var file_extentionParameter = file_extention != null ?
                new ObjectParameter("file_extention", file_extention) :
                new ObjectParameter("file_extention", typeof(string));
    
            var self_one_or_groupParameter = self_one_or_group != null ?
                new ObjectParameter("self_one_or_group", self_one_or_group) :
                new ObjectParameter("self_one_or_group", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_sharesInsert_Result>("usp_sharesInsert", member_publicParameter, owner_privateParameter, org_publicParameter, org_privateParameter, to_publicParameter, death_dateParameter, kb_sizeParameter, file_extentionParameter, self_one_or_groupParameter);
        }
    
        public virtual ObjectResult<usp_sharesSelect_Result> usp_sharesSelect(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_sharesSelect_Result>("usp_sharesSelect", idParameter);
        }
    
        public virtual ObjectResult<usp_sharesUpdate_Result> usp_sharesUpdate(Nullable<int> id, Nullable<System.Guid> member_public, Nullable<System.Guid> owner_private, Nullable<System.Guid> org_public, Nullable<System.Guid> org_private, Nullable<System.Guid> to_public, Nullable<System.DateTime> death_date, Nullable<int> kb_size, string file_extention, string self_one_or_group)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var member_publicParameter = member_public.HasValue ?
                new ObjectParameter("member_public", member_public) :
                new ObjectParameter("member_public", typeof(System.Guid));
    
            var owner_privateParameter = owner_private.HasValue ?
                new ObjectParameter("owner_private", owner_private) :
                new ObjectParameter("owner_private", typeof(System.Guid));
    
            var org_publicParameter = org_public.HasValue ?
                new ObjectParameter("org_public", org_public) :
                new ObjectParameter("org_public", typeof(System.Guid));
    
            var org_privateParameter = org_private.HasValue ?
                new ObjectParameter("org_private", org_private) :
                new ObjectParameter("org_private", typeof(System.Guid));
    
            var to_publicParameter = to_public.HasValue ?
                new ObjectParameter("to_public", to_public) :
                new ObjectParameter("to_public", typeof(System.Guid));
    
            var death_dateParameter = death_date.HasValue ?
                new ObjectParameter("death_date", death_date) :
                new ObjectParameter("death_date", typeof(System.DateTime));
    
            var kb_sizeParameter = kb_size.HasValue ?
                new ObjectParameter("kb_size", kb_size) :
                new ObjectParameter("kb_size", typeof(int));
    
            var file_extentionParameter = file_extention != null ?
                new ObjectParameter("file_extention", file_extention) :
                new ObjectParameter("file_extention", typeof(string));
    
            var self_one_or_groupParameter = self_one_or_group != null ?
                new ObjectParameter("self_one_or_group", self_one_or_group) :
                new ObjectParameter("self_one_or_group", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_sharesUpdate_Result>("usp_sharesUpdate", idParameter, member_publicParameter, owner_privateParameter, org_publicParameter, org_privateParameter, to_publicParameter, death_dateParameter, kb_sizeParameter, file_extentionParameter, self_one_or_groupParameter);
        }
    }
}
