﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMG.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EMGDataBaseEntities : DbContext
    {
        public EMGDataBaseEntities()
            : base("name=EMGDataBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account_Course> Account_Course { get; set; }
        public virtual DbSet<ActImg> ActImg { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Course_Tag> Course_Tag { get; set; }
        public virtual DbSet<EMGData> EMGData { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<IEMGData> IEMGData { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Posture> Posture { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<RecordEffect> RecordEffect { get; set; }
        public virtual DbSet<Reply> Reply { get; set; }
        public virtual DbSet<Rest_Max> Rest_Max { get; set; }
        public virtual DbSet<RMSData> RMSData { get; set; }
        public virtual DbSet<Text> Text { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<TopicSelect> TopicSelect { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserHabit> UserHabit { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<Usertest> Usertest { get; set; }
    }
}
