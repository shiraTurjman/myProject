using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Dal.Models
{
    public partial class FinalProjectContext : DbContext
    {
        public FinalProjectContext()
        {
        }

        public FinalProjectContext(DbContextOptions<FinalProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Chips> Chips { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<OutfitItems> OutfitItems { get; set; }
        public virtual DbSet<Outfits> Outfits { get; set; }
        public virtual DbSet<TagItem> TagItem { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Uses> Uses { get; set; }
        public object User { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-MS96HH8; Database=FinalProject; Trusted_Connection=true");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("pk_category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("categoryName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user_category");
            });

            modelBuilder.Entity<Chips>(entity =>
            {
                entity.HasKey(e => e.ChipId)
                    .HasName("pk_chip");

                entity.Property(e => e.ChipId)
                    .HasColumnName("chipId")
                    .ValueGeneratedNever();

                entity.Property(e => e.ItemId).HasColumnName("itemId");

                entity.Property(e => e.ItemLocation)
                    .IsRequired()
                    .HasColumnName("itemLocation")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Chips)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_chip_item");
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.HasKey(e => e.ColorId)
                    .HasName("pk_colors");

                entity.Property(e => e.ColorId).HasColumnName("colorId");

                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasColumnName("colorName")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("eventId");

                entity.Property(e => e.DateEvent)
                    .HasColumnName("dateEvent")
                    .HasColumnType("datetime");

                entity.Property(e => e.ItemId).HasColumnName("itemId");

                entity.Property(e => e.OutfitId).HasColumnName("outfitId");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("fk_event_item");

                entity.HasOne(d => d.Outfit)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.OutfitId)
                    .HasConstraintName("fk_event_outfit");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("pk_items");

                entity.Property(e => e.ItemId).HasColumnName("itemId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.ColorId).HasColumnName("colorId");

                entity.Property(e => e.EntryDate)
                    .HasColumnName("entryDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasColumnName("img")
                    .HasColumnType("image");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("fk_item_color");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_item_user");
            });

            modelBuilder.Entity<OutfitItems>(entity =>
            {
                entity.HasKey(e => e.OutfitItemId)
                    .HasName("pk_outfit_item");

                entity.ToTable("Outfit_Items");

                entity.Property(e => e.OutfitItemId).HasColumnName("outfitItemId");

                entity.Property(e => e.ItemId).HasColumnName("itemId");

                entity.Property(e => e.OutfitId).HasColumnName("outfitId");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OutfitItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_outfitItem_item");

                entity.HasOne(d => d.Outfit)
                    .WithMany(p => p.OutfitItems)
                    .HasForeignKey(d => d.OutfitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_outfitItem_outfit");
            });

            modelBuilder.Entity<Outfits>(entity =>
            {
                entity.HasKey(e => e.OutfitId)
                    .HasName("pk_outfits");

                entity.Property(e => e.OutfitId).HasColumnName("outfitId");

                entity.Property(e => e.OutfitName)
                    .HasColumnName("outfitName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Outfits)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_outfit_user");
            });

            modelBuilder.Entity<TagItem>(entity =>
            {
                entity.ToTable("Tag_Item");

                entity.Property(e => e.TagItemId).HasColumnName("tagItemId");

                entity.Property(e => e.ItemId).HasColumnName("itemId");

                entity.Property(e => e.TagId).HasColumnName("tagId");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TagItem)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tagItem_item");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagItem)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tagItem_tag");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.HasKey(e => e.TagId)
                    .HasName("pk_tags");

                entity.Property(e => e.TagId).HasColumnName("tagId");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasColumnName("tagName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tag_user");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("pk_userId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Address)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Uses>(entity =>
            {
                entity.HasKey(e => e.UseId)
                    .HasName("pk_uses");

                entity.Property(e => e.UseId).HasColumnName("useId");

                entity.Property(e => e.DateUse)
                    .HasColumnName("dateUse")
                    .HasColumnType("datetime");

                entity.Property(e => e.ItemId).HasColumnName("itemId");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Uses)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_use_item");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
