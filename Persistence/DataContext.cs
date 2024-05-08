using System.Diagnostics;
using System.Reflection.Emit;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementChannel> AnnouncementChannels { get; set; }
        public DbSet<AnnouncementCondition> AnnouncementConditions { get; set; }
        public DbSet<AnnouncementConditionAnnouncementChannel> AnnouncementConditionAnnouncementChannels { get; set; }
        public DbSet<AnnouncementConditionGroup> AnnouncementConditionGroups { get; set; }
        public DbSet<AnnouncementMessage> AnnouncementMessages { get; set; }
        public DbSet<AnnouncementPriority> AnnouncementPriorities { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorGenre> AuthorGenres { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerBannerLocation> BannerBannerLocations { get; set; }
        public DbSet<BannerGroup> BannerGroups { get; set; }
        public DbSet<BannerLocation> BannerLocations { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookBookCollection> BookBookCollections { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookCollection> BookCollections { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookPublisher> BookPublishers { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<BookThematicArea> BookThematicAreas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTag> CategoryTags { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CustomerCategory> CustomerCategories { get; set; }
        public DbSet<CustomerGenre> CustomerGenres { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<CustomerReward> CustomerRewards { get; set; }
        public DbSet<CustomerTag> CustomerTags { get; set; }
        public DbSet<CustomerThematicArea> CustomerThematicAreas { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Medium> Media { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderSubscription> OrderSubscriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<PublisherGenre> PublisherGenres { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<RewardGroup> RewardGroups { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ThematicArea> ThematicAreas { get; set; }
        public DbSet<GiftPayment> GiftPayments { get; set; }

        public DbSet<SuggestBook> SugggestBook { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Address>()
                .Property(s => s.Ad_ID).ValueGeneratedOnAdd();
            builder.Entity<Address>()
                .Property(s => s.Ad_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Announcement>()
                .Property(s => s.An_ID).ValueGeneratedOnAdd();
            builder.Entity<Announcement>()
                .Property(s => s.An_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<AnnouncementChannel>()
                .Property(s => s.AnCh_ID).ValueGeneratedOnAdd();
            builder.Entity<AnnouncementChannel>()
                .Property(s => s.AnCh_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<AnnouncementCondition>()
                .Property(s => s.AnCn_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<AnnouncementConditionAnnouncementChannel>()
                .Property(s => s.AnChGr_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<AnnouncementConditionGroup>()
                .Property(s => s.AnCnGr_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<AnnouncementMessage>()
                .Property(s => s.AnMs_ID).ValueGeneratedOnAdd();
            builder.Entity<AnnouncementMessage>()
                .Property(s => s.AnMs_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<AnnouncementPriority>()
                .Property(s => s.AnPr_ID).ValueGeneratedOnAdd();
            builder.Entity<AnnouncementPriority>()
                .Property(s => s.AnPr_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Author>()
                .Property(s => s.At_ID).ValueGeneratedOnAdd();
            builder.Entity<Author>()
                .Property(s => s.At_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<AuthorGenre>()
                .Property(s => s.AtGn_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Avatar>()
                .Property(s => s.Av_ID).ValueGeneratedOnAdd();
            builder.Entity<Avatar>()
                .Property(s => s.Av_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Banner>()
                .Property(s => s.Bn_ID).ValueGeneratedOnAdd();
            builder.Entity<Banner>()
                .Property(s => s.Bn_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<BannerBannerLocation>()
                .Property(s => s.BnBl_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<BannerGroup>()
                .Property(s => s.BnGr_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<BannerLocation>()
                .Property(s => s.Bl_ID).ValueGeneratedOnAdd();
            builder.Entity<BannerLocation>()
                .Property(s => s.Bl_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Book>()
                .Property(s => s.Bk_ID).ValueGeneratedOnAdd();
            builder.Entity<Book>()
                .Property(s => s.Bk_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<BookAuthor>()
                .Property(s => s.BkAt_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<BookCategory>()
                .Property(s => s.BkCt_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<BookGenre>()
                .Property(s => s.BkGn_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<BookPublisher>()
                .Property(s => s.BkPb_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<BookTag>()
                .Property(s => s.BkTg_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<BookThematicArea>()
                .Property(s => s.BkTh_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Category>()
                .Property(s => s.Ct_ID).ValueGeneratedOnAdd();
            builder.Entity<Category>()
                .Property(s => s.Ct_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Category>()
               .HasMany(x => x.SubCategories)
               .WithOne(z => z.Classification)
               .HasForeignKey(g => g.Ct_ClassificationID);

            builder.Entity<CategoryTag>()
                .Property(s => s.CtTg_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Country>()
                .Property(s => s.Cn_ID).ValueGeneratedOnAdd();
            builder.Entity<Country>()
                .Property(s => s.Cn_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<CustomerCategory>()
                .Property(s => s.CsCt_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<CustomerGenre>()
                .Property(s => s.CsGn_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<CustomerGroup>()
                .Property(s => s.CsGr_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<CustomerReward>()
                .Property(s => s.CsRw_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<CustomerTag>()
                .Property(s => s.CsTg_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<CustomerThematicArea>()
                .Property(s => s.CsTh_CreatedOn).ValueGeneratedOnAdd();



            builder.Entity<Genre>()
                .Property(s => s.Gn_ID).ValueGeneratedOnAdd();
            builder.Entity<Genre>()
                .Property(s => s.Gn_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Group>()
                .Property(s => s.Gr_ID).ValueGeneratedOnAdd();
            builder.Entity<Group>()
                .Property(s => s.Gr_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Medium>()
                .Property(s => s.Md_ID).ValueGeneratedOnAdd();
            builder.Entity<Medium>()
                .Property(s => s.Md_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Order>()
                .Property(s => s.Or_ID).ValueGeneratedOnAdd();
            builder.Entity<Order>()
                .Property(s => s.Or_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<OrderSubscription>()
                .Property(s => s.OrSb_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<PaymentMethod>()
                .Property(s => s.Py_ID).ValueGeneratedOnAdd();
            builder.Entity<PaymentMethod>()
                .Property(s => s.Py_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Payment>()
                .Property(s => s.Pm_ID).ValueGeneratedOnAdd();
            builder.Entity<Payment>()
                .Property(s => s.Pm_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Publisher>()
                .Property(s => s.Pb_ID).ValueGeneratedOnAdd();
            builder.Entity<Publisher>()
                .Property(s => s.Pb_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<PublisherGenre>()
                .Property(s => s.PbGn_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Reward>()
                .Property(s => s.Rw_ID).ValueGeneratedOnAdd();
            builder.Entity<Reward>()
                .Property(s => s.Rw_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<RewardGroup>()
                .Property(s => s.RwGr_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Subscription>()
                .Property(s => s.Sb_ID).ValueGeneratedOnAdd();
            builder.Entity<Subscription>()
                .Property(s => s.Sb_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<Tag>()
                .Property(s => s.Tg_ID).ValueGeneratedOnAdd();
            builder.Entity<Tag>()
                .Property(s => s.Tg_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<ThematicArea>()
                .Property(s => s.Th_ID).ValueGeneratedOnAdd();
            builder.Entity<ThematicArea>()
                .Property(s => s.Th_CreatedOn).ValueGeneratedOnAdd();

            builder.Entity<GiftPayment>()
                .Property(s => s.Pm_ID).ValueGeneratedOnAdd();
            builder.Entity<GiftPayment>()
                .Property(s => s.PM_GiftedOn).ValueGeneratedOnAdd();

            builder.Entity<Domain.SuggestBook>()
                .Property(s => s.Bk_ID).ValueGeneratedOnAdd();
            builder.Entity<Domain.SuggestBook>()
                .Property(s => s.Bk_CreatedOn).ValueGeneratedOnAdd();

            var cascadeFKs = builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            builder.Entity<Book>()
                .HasOne(b => b.Md_AudioEn)
                .WithMany()
                .HasForeignKey(b => b.Md_AudioEn_ID);

            builder.Entity<Book>()
                .HasOne(b => b.Md_AudioAr)
                .WithMany()
                .HasForeignKey(b => b.Md_AudioAr_ID);
        }
    }
}