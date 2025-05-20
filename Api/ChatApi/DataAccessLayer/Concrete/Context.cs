using ChatApi.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseMySql(
                "server=localhost;database=ChatApi;user=root;password='';",
                new MySqlServerVersion(new Version(8, 0, 2))
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                modelBuilder.Entity<UserFriendList>()
                .HasOne(uf => uf.UserFriend)
                .WithMany(u => u.UserFriend)
                .HasForeignKey(uf => uf.UserFriendId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                base.OnModelCreating(modelBuilder);



                #region Telefon Numarası Tanımlama
                entity.Property(e => e.PhoneNumber)
                      .HasMaxLength(10)
                      .IsUnicode(false)
                      .IsRequired();
                entity.ToTable("Users", t => t.HasCheckConstraint("CK_PhoneNumber_RakamsOnly", "PhoneNumber REGEXP '^[0-9]'"));

                entity.HasIndex(e => e.PhoneNumber)
                      .IsUnique();

                #endregion

                #region Kullanici Adı

                entity.Property(e => e.UserName)
                      .HasMaxLength(80)
                      .IsRequired();
                #endregion

                #region Email Adresi Tanımlama
                entity.Property(e => e.Email)
                      .HasMaxLength(255)
                      .IsUnicode(false)
                      .IsRequired();
                entity.ToTable("Users", t => t.HasCheckConstraint("CK_Email_Format", "Email REGEXP '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$'"));

                entity.HasIndex(e => e.Email)
                .IsUnique();
                #endregion

                #region Kullanici Resmi

                entity.Property(e => e.UserImage)
                      .HasMaxLength(400)
                      .IsRequired();
                #endregion

                #region Tarih Tanımlama

                entity.Property(e => e.UserLastOnlineDate)
                        .HasColumnType("datetime");
                #endregion

                #region Kullanici Şifresi

                entity.Property(e => e.Password)
                      .HasMaxLength(100)
                      .IsRequired();
                #endregion
            });
            modelBuilder.Entity<Message>(entity =>
            {
                modelBuilder.Entity<Message>()
                             .HasOne(uf => uf.Receiver)
                             .WithMany(u => u.Receiver)
                             .HasForeignKey(uf => uf.ReceiverId)
                             .OnDelete(DeleteBehavior.ClientSetNull);

                base.OnModelCreating(modelBuilder);

                #region Tarih Tanımlama

                entity.Property(e => e.MessageTime)
                        .HasColumnType("datetime");
                #endregion

                #region Messaj içeriği tanımlama
                entity.Property(e => e.MessageContext)
                              .HasMaxLength(2000)
                              .IsRequired();
                #endregion
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserFriendList> userFriendLists { get; set; }
        public DbSet<Message> messages { get; set; }
    }
}
