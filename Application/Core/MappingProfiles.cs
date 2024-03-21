using System.Diagnostics;
using Application.Addresses.Core;
using Application.Authors.Core;
using Application.BannerLocations.Core;
using Application.Banners.Core;
using Application.BookCollections.Core;
using Application.Books.Core;
using Application.Categories.Core;
using Application.Countries.Core;
using Application.Genres.Core;
using Application.Groups.Core;
using Application.Media.Core;
using Application.PaymentMehods.Core;
using Application.PaymentMethods.Core;
using Application.Payments.Core;
using Application.Profiles.Core;
using Application.Publishers.Core;
using Application.Rewards.Core;
using Application.Subscriptions.Core;
using Application.Tags.Core;
using Application.ThematicAreas.Core;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;

            //BannerLocation
            CreateMap<BannerLocation, BannerLocation>();
            CreateMap<BannerLocation, BannerLocationDto>()
                .ForMember(s => s.Bl_Banners, z => z.MapFrom(s => s.Banners.Where(s => s.Bn_Active).Count()))
                .ForMember(s => s.Bl_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Bl_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));
            CreateMap<BannerLocation, BannerLocationCmdDto>()
                .ForMember(s => s.Bl_Banners, z => z.MapFrom(s => s.Banners.Where(s => s.Bn_Active).Count()));
            CreateMap<BannerLocationCmdDto, BannerLocation>()
                .ForMember(s => s.Banners, z => z.Ignore());

            //Group
            CreateMap<Group, Group>();
            CreateMap<Group, GroupDto>()
                .ForMember(s => s.Gr_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Gr_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));
            CreateMap<Group, GroupQryDto>()
                .ForMember(s => s.Genres, z => z.MapFrom(s => s.Genres.Where(s => s.GrGn_Active).Select(s => s.Genre)))
                .ForMember(s => s.GenreItems, z => z.MapFrom(s => s.Genres.Where(s => s.GrGn_Active).Select(s => new ItemDto(s.Gn_ID, s.GrGn_Ordinal))))
                .ForMember(s => s.ThematicAreas, z => z.MapFrom(s => s.ThematicAreas.Where(s => s.GrTh_Active).Select(s => s.ThematicArea)))
                .ForMember(s => s.ThematicAreaItems, z => z.MapFrom(s => s.ThematicAreas.Where(s => s.GrTh_Active).Select(s => new ItemDto(s.Th_ID, s.GrTh_Ordinal))))
                .ForMember(s => s.Categories, z => z.MapFrom(s => s.Categories.Where(s => s.GrCt_Active).Select(s => s.Category)))
                .ForMember(s => s.CategoryItems, z => z.MapFrom(s => s.Categories.Where(s => s.GrCt_Active).Select(s => new ItemDto(s.Ct_ID, s.GrCt_Ordinal))))
                .ForMember(s => s.Tags, z => z.MapFrom(s => s.Tags.Where(s => s.GrTg_Active).Select(s => s.Tag)))
                .ForMember(s => s.TagItems, z => z.MapFrom(s => s.Tags.Where(s => s.GrTg_Active).Select(s => new ItemDto(s.Tg_ID, s.GrTg_Ordinal))))
                .ForMember(s => s.Gr_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Gr_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));
            CreateMap<Group, GroupCmdDto>();
            CreateMap<GroupCmdDto, Group>();



            //Medium
            CreateMap<Address, Address>();
            CreateMap<Address, AddressDto>()
                .ForMember(s => s.Ad_CustomerName, z => z.MapFrom(s => s.User.Us_DisplayName));

            //Country
            CreateMap<Country, Country>();
            CreateMap<Country, CountryDto>();

            //Medium
            CreateMap<Medium, Medium>();
            CreateMap<Medium, MediumDto>();
            CreateMap<Medium, MediumCmdDto>();
            CreateMap<Medium, MediumListDto>();

            //Genre
            CreateMap<Genre, Genre>();
            CreateMap<Genre, GenreDto>()
                .ForMember(s => s.Gn_Total, z => z.MapFrom(s => s.Books.Where(s => s.BkGn_Active).Count() + s.Authors.Where(s => s.AtGn_Active).Count() + s.Publishers.Where(s => s.PbGn_Active).Count()))
                .ForMember(s => s.Gn_Summaries, z => z.MapFrom(s => s.Books.Where(s=>s.BkGn_Active).Count()))
                .ForMember(s => s.Gn_Authors, z => z.MapFrom(s => s.Authors.Where(s => s.AtGn_Active).Count()))
                .ForMember(s => s.Gn_Publishers, z => z.MapFrom(s => s.Publishers.Where(s => s.PbGn_Active).Count()))
                .ForMember(s => s.Gn_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Gn_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));
            CreateMap<Genre, GenreCmdDto>();
            CreateMap<GenreCmdDto, Genre>();
            CreateMap<GenreCmdDto, GenreDto>();

            //Tag
            CreateMap<Tag, Tag>();
            CreateMap<Tag, TagDto>()
                .ForMember(s => s.Tg_Total, z => z.MapFrom(s => s.Books.Where(s => s.BkTg_Active).Count() + s.Categories.Where(s => s.CtTg_Active).Count()))
                .ForMember(s => s.Tg_Summaries, z => z.MapFrom(s => s.Books.Where(s => s.BkTg_Active).Count()))
                .ForMember(s => s.Tg_Categories, z => z.MapFrom(s => s.Categories.Where(s => s.CtTg_Active).Count()))
                .ForMember(s => s.Tg_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Tg_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));
            CreateMap<Tag, TagCmdDto>();
            CreateMap<TagCmdDto, Tag>();
            CreateMap<TagCmdDto, TagDto>();
            CreateMap<Tag, TagSubDto>();

            //ThematicArea
            CreateMap<ThematicArea, ThematicArea>();
            CreateMap<ThematicArea, ThematicAreaDto>()
                .ForMember(s => s.Th_Summaries, z => z.MapFrom(s => s.Books.Where(s => s.BkTh_Active).Count()))
                .ForMember(s=> s.Th_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Th_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));
            CreateMap<ThematicArea, ThematicAreaCmdDto>();
            CreateMap<ThematicAreaCmdDto, ThematicArea>();

            //Category
            CreateMap<Category, Category>();
            CreateMap<Category, CategoryDto>()
                .ForMember(s => s.ClassificationID, z => z.MapFrom(s => s.Ct_ClassificationID))
                .ForMember(s => s.Classification, z => z.MapFrom(s => (s.Classification != null) ? s.Classification.Ct_Name : String.Empty))
                .ForMember(s => s.Classification_Ar, z => z.MapFrom(s => (s.Classification != null) ? s.Classification.Ct_Name_Ar : String.Empty))
                .ForMember(s => s.Ct_Summaries, z => z.MapFrom(s => s.Books.Where(s => s.BkCt_Active).Count()))
                .ForMember(s => s.Tags, z => z.MapFrom(s => s.Tags.Where(s => s.CtTg_Active==true).Select(y => y.Tag)))
                .ForMember(s => s.Ct_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Ct_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<Category, CategoryCmdDto>()
                .ForMember(s => s.ClassificationID, z => z.MapFrom(s => s.Classification.Ct_ID))
                .ForMember(s => s.Tags, z => z.Ignore());

            CreateMap<CategoryCmdDto, Category>()
                .ForMember(s => s.Tags, z => z.Ignore())
                .ForMember(s => s.Ct_ClassificationID, z => z.MapFrom(s => s.ClassificationID));

            //Banner
            CreateMap<Banner, Banner>();
            CreateMap<Banner, BannerDto>()
                .ForMember(s => s.Bn_GroupTitle, z => z.MapFrom(s => (s.Group != null) ? s.Group.Gr_Title: String.Empty))
                .ForMember(s => s.Bn_GroupTitle_Ar, z => z.MapFrom(s => (s.Group != null) ? s.Group.Gr_Title_Ar : String.Empty))
                .ForMember(s => s.Bn_BannerLocationTitle, z => z.MapFrom(s => (s.BannerLocation != null) ? s.BannerLocation.Bl_Title: String.Empty))
                .ForMember(s => s.Bn_BannerLocationTitle_Ar, z => z.MapFrom(s => (s.BannerLocation != null) ? s.BannerLocation.Bl_Title_Ar : String.Empty))
                .ForMember(s => s.Bn_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Bn_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<Banner, BannerCmdDto>();
            CreateMap<BannerCmdDto, Banner>();


            //Author
            CreateMap<Author, Author>();
            CreateMap<Author, AuthorListDto>();
            CreateMap<Author, AuthorDto>()
                .ForMember(s => s.At_Summaries, z => z.MapFrom(s => s.Books.Where(s => s.BkAt_Active).Count()))
                .ForMember(s => s.At_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.At_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<Author, AuthorQryDto>()
                .ForMember(s => s.At_Summaries, z => z.MapFrom(s => s.Books.Where(s => s.BkAt_Active).Count()))
                .ForMember(s => s.Genres, z => z.MapFrom(s => s.Genres.Where(s => s.AtGn_Active).Select(s => s.Genre)))
                .ForMember(s => s.GenreItems, z => z.MapFrom(s => s.Genres.Where(s => s.AtGn_Active).Select(s => new ItemDto(s.Gn_ID, s.AtGn_Ordinal))))
                .ForMember(s => s.At_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.At_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<Author, AuthorCmdDto>()
                .ForMember(s => s.Genres, z => z.MapFrom(s => s.Genres.Where(s => s.AtGn_Active).Select(s => s.Genre)))
                .ForMember(s => s.GenreItems, z => z.MapFrom(s => s.Genres.Where(s => s.AtGn_Active).Select(s => new ItemDto(s.Gn_ID, s.AtGn_Ordinal))));
                
            CreateMap<AuthorCmdDto, Author>()
                .ForMember(s => s.Genres, z => z.Ignore());


           
            //Publisher
            CreateMap<Publisher, Publisher>();
            CreateMap<Publisher, PublisherListDto>();
            CreateMap<Publisher, PublisherDto>()
                .ForMember(s => s.Pb_Summaries, z => z.MapFrom(s => s.Books.Where(s => s.BkPb_Active).Count()))
                .ForMember(s => s.Pb_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Pb_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<Publisher, PublisherQryDto>()
                .ForMember(s => s.Pb_Summaries, z => z.MapFrom(s => s.Books.Where(s => s.BkPb_Active).Count()))
                .ForMember(s => s.Genres, z => z.MapFrom(s => s.Genres.Where(s => s.PbGn_Active).Select(s => s.Genre)))
                .ForMember(s => s.GenreItems, z => z.MapFrom(s => s.Genres.Where(s => s.PbGn_Active).Select(s => new ItemDto(s.Gn_ID, s.PbGn_Ordinal))))
                .ForMember(s => s.Pb_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Pb_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));


            CreateMap<Publisher, PublisherCmdDto>()
                .ForMember(s => s.Genres, z => z.MapFrom(s => s.Genres.Where(s => s.PbGn_Active).Select(s => s.Genre)))
                .ForMember(s => s.GenreItems, z => z.MapFrom(s => s.Genres.Where(s => s.PbGn_Active).Select(s => new ItemDto(s.Gn_ID, s.PbGn_Ordinal))));

            CreateMap<PublisherCmdDto, Publisher>()
                .ForMember(s => s.Genres, z => z.Ignore());

            //Subscription
            CreateMap<Subscription, Subscription>();
            CreateMap<Subscription, SubscriptionDto>()
                .ForMember(s => s.Sb_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Sb_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<Subscription, SubscriptionQryDto>()
                .ForMember(s => s.Sb_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Sb_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<Subscription, SubscriptionCmdDto>();

            CreateMap<SubscriptionCmdDto, Subscription>();

            //PaymentMethod
            CreateMap<PaymentMethod, PaymentMethod>();
            CreateMap<PaymentMethod, PaymentMethodDto>()
                .ForMember(s => s.Py_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Py_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<PaymentMethod, PaymentMethodQryDto>()
                .ForMember(s => s.Py_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Py_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<PaymentMethod, PaymentMethodCmdDto>();

            CreateMap<PaymentMethodCmdDto, PaymentMethod>();

            //Payment
            CreateMap<Payment, Payment>();
            CreateMap<Payment, PaymentDto>()
                .ForMember(s => s.SubscriberName, z => z.MapFrom(s => s.Subscriber.Us_DisplayName))
                .ForMember(s => s.SubscriberPhoneNumber, z => z.MapFrom(s => s.Subscriber.PhoneNumber))
                .ForMember(s => s.SubscriberEmail, z => z.MapFrom(s => s.Subscriber.Email))
                .ForMember(s => s.PaymentMethod, z => z.MapFrom(s => s.PaymentMethod.Py_Title))
                .ForMember(s => s.PaymentMethod_Ar, z => z.MapFrom(s => s.PaymentMethod.Py_Title_Ar))
                .ForMember(s => s.Subscription, z => z.MapFrom(s => s.Subscription.Sb_Name))
                .ForMember(s => s.Subscription_Ar, z => z.MapFrom(s => s.Subscription.Sb_Name_Ar))
                .ForMember(s => s.Pm_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName));

            CreateMap<Payment, PaymentQryDto>()
                .ForMember(s => s.Pm_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName));

            CreateMap<Payment, PaymentCmdDto>();

            CreateMap<PaymentCmdDto, Payment>();

            //Reward
            CreateMap<Reward, Reward>();
            CreateMap<Reward, RewardDto>()
                .ForMember(s => s.Rw_Customers, z => z.MapFrom(s => s.Customers.Where(s => s.CsRw_Active).Count()))
                .ForMember(s => s.Rw_Groups, z => z.MapFrom(s => s.Groups.Where(s => s.RwGr_Active).Count()));

            CreateMap<Reward, RewardCmdDto>()
                .ForMember(s => s.Groups, z => z.MapFrom(s => s.Groups.Where(s => s.RwGr_Active)));

            //Book
            CreateMap<Book, Book>();
            CreateMap<Book, BookListDto>()
                .ForMember(s => s.Md_ID, z => z.MapFrom(s => s.Media.Where(s => s.Md_Active).OrderByDescending(s => s.Md_CreatedOn).Select(s => s.Md_ID).FirstOrDefault()))
                .ForMember(s => s.Bk_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Bk_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));
            CreateMap<Book, BookDto>()
                .ForMember(s => s.Genres, z => z.MapFrom(s => s.Genres.Where(s=>s.BkGn_Active).Select(s => s.Genre)))
                .ForMember(s => s.ThematicAreas, z => z.MapFrom(s => s.ThematicAreas.Where(s => s.BkTh_Active).Select(s => s.ThematicArea)))
                .ForMember(s => s.Categories, z => z.MapFrom(s => s.Categories.Where(s=>s.BkCt_Active).Select(s => s.Category)))
                .ForMember(s => s.Tags, z => z.MapFrom(s => s.Tags.Where(s => s.BkTg_Active).Select(s => s.Tag)))
                .ForMember(s => s.Authors, z => z.MapFrom(s => s.Authors.Where(s => s.BkAt_Active).Select(s => s.Author)))
                .ForMember(s => s.Publishers, z => z.MapFrom(s => s.Publishers.Where(s => s.BkPb_Active).Select(s => s.Publisher)))
                .ForMember(s => s.Media, z => z.MapFrom(s => s.Media.Where(s => s.Md_Active)))
                .ForMember(s => s.Bk_Language_Ar, z => z.MapFrom(s => (s.Bk_Language.ToLower() == "arabic") ? "عربي" : (s.Bk_Language.ToLower() == "english") ? "إنجليزي" : "الكل"))
                .ForMember(s => s.Bk_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Bk_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));


            CreateMap<Book, BookQryDto>()
                .ForMember(s => s.Genres, z => z.MapFrom(s => s.Genres.Where(s => s.BkGn_Active).Select(s => s.Genre)))
                .ForMember(s => s.GenreItems, z => z.MapFrom(s => s.Genres.Where(s => s.BkGn_Active).Select(s => new ItemDto(s.Gn_ID, s.BkGn_Ordinal))))
                .ForMember(s => s.ThematicAreas, z => z.MapFrom(s => s.ThematicAreas.Where(s => s.BkTh_Active).Select(s => s.ThematicArea)))
                .ForMember(s => s.ThematicAreaItems, z => z.MapFrom(s => s.ThematicAreas.Where(s => s.BkTh_Active).Select(s => new ItemDto(s.Th_ID, s.BkTh_Ordinal))))
                .ForMember(s => s.Categories, z => z.MapFrom(s => s.Categories.Where(s => s.BkCt_Active).Select(s => s.Category)))
                .ForMember(s => s.CategoryItems, z => z.MapFrom(s => s.Categories.Where(s => s.BkCt_Active).Select(s => new ItemDto(s.Ct_ID, s.BkCt_Ordinal))))
                .ForMember(s => s.Tags, z => z.MapFrom(s => s.Tags.Where(s => s.BkTg_Active).Select(s => s.Tag)))
                .ForMember(s => s.TagItems, z => z.MapFrom(s => s.Tags.Where(s => s.BkTg_Active).Select(s => new ItemDto(s.Tg_ID, s.BkTg_Ordinal))))
                .ForMember(s => s.Authors, z => z.MapFrom(s => s.Authors.Where(s => s.BkAt_Active).Select(s => s.Author)))
                .ForMember(s => s.AuthorItems, z => z.MapFrom(s => s.Authors.Where(s => s.BkAt_Active).Select(s => new ItemDto(s.At_ID, s.BkAt_Ordinal))))
                .ForMember(s => s.Publishers, z => z.MapFrom(s => s.Publishers.Where(s => s.BkPb_Active).Select(s => s.Publisher)))
                .ForMember(s => s.PublisherItems, z => z.MapFrom(s => s.Publishers.Where(s => s.BkPb_Active).Select(s => new ItemDto(s.Pb_ID, s.BkPb_Ordinal))))
                .ForMember(s => s.Media, z => z.MapFrom(s => s.Media))
                .ForMember(s => s.Bk_Language_Ar, z => z.MapFrom(s => (s.Bk_Language.ToLower() == "arabic") ? "عربي" : (s.Bk_Language.ToLower() == "english") ? "إنجليزي" : "الكل"))
                .ForMember(s => s.Bk_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Bk_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<Book, BookCmdDto>()
                .ForMember(s => s.Genres, z => z.MapFrom(s => s.Genres.Where(s => s.BkGn_Active).Select(s => s.Genre)))
                .ForMember(s => s.GenreItems, z => z.MapFrom(s => s.Genres.Where(s => s.BkGn_Active).Select(s => new ItemDto(s.Gn_ID, s.BkGn_Ordinal))))
                .ForMember(s => s.ThematicAreas, z => z.MapFrom(s => s.ThematicAreas.Where(s => s.BkTh_Active).Select(s => s.ThematicArea)))
                .ForMember(s => s.ThematicAreaItems, z => z.MapFrom(s => s.ThematicAreas.Where(s => s.BkTh_Active).Select(s => new ItemDto(s.Th_ID, s.BkTh_Ordinal))))
                .ForMember(s => s.Categories, z => z.MapFrom(s => s.Categories.Where(s => s.BkCt_Active).Select(s => s.Category)))
                .ForMember(s => s.CategoryItems, z => z.MapFrom(s => s.Categories.Where(s => s.BkCt_Active).Select(s => new ItemDto(s.Ct_ID, s.BkCt_Ordinal))))
                .ForMember(s => s.Tags, z => z.MapFrom(s => s.Tags.Where(s => s.BkTg_Active).Select(s => s.Tag)))
                .ForMember(s => s.TagItems, z => z.MapFrom(s => s.Tags.Where(s => s.BkTg_Active).Select(s => new ItemDto(s.Tg_ID, s.BkTg_Ordinal))))
                .ForMember(s => s.Authors, z => z.MapFrom(s => s.Authors.Where(s => s.BkAt_Active).Select(s => s.Author)))
                .ForMember(s => s.AuthorItems, z => z.MapFrom(s => s.Authors.Where(s => s.BkAt_Active).Select(s => new ItemDto(s.At_ID, s.BkAt_Ordinal))))
                .ForMember(s => s.Publishers, z => z.MapFrom(s => s.Publishers.Where(s => s.BkPb_Active).Select(s => s.Publisher)))
                .ForMember(s => s.PublisherItems, z => z.MapFrom(s => s.Publishers.Where(s => s.BkPb_Active).Select(s => new ItemDto(s.Pb_ID, s.BkPb_Ordinal))))
                .ForMember(s => s.Media, z => z.MapFrom(s => s.Media));

            CreateMap<BookCmdDto, Book>()
                .ForMember(s => s.Genres, z => z.Ignore())
                .ForMember(s => s.ThematicAreas, z => z.Ignore())
                .ForMember(s => s.Categories, z => z.Ignore())
                .ForMember(s => s.Tags, z => z.Ignore())
                .ForMember(s => s.Authors, z => z.Ignore())
                .ForMember(s => s.Publishers, z => z.Ignore())
                .ForMember(s => s.Media, z => z.Ignore());

            //BookCollection
            CreateMap<BookCollection, BookCollection>();
            CreateMap<BookCollection, BookCollectionDto>()
                .ForMember(s => s.Bc_Summaries, z => z.MapFrom(s => s.Books.Where(s => s.BkBc_Active).Count()))
                .ForMember(s => s.Bc_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Bc_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<BookCollection, BookCollectionQryDto>()
                .ForMember(s => s.Bc_Summaries, z => z.MapFrom(s => s.Books.Where(s => s.BkBc_Active).Count()))
                .ForMember(s => s.Books, z => z.MapFrom(s => s.Books.Where(s => s.BkBc_Active).Select(s => s.Book)))
                .ForMember(s => s.BookItems, z => z.MapFrom(s => s.Books.Where(s => s.BkBc_Active).Select(s => new ItemDto(s.Bk_ID, s.BkBc_Ordinal))))
                .ForMember(s => s.Bc_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Bc_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName));

            CreateMap<BookCollection, BookCollectionCmdDto>()
                .ForMember(s => s.Books, z => z.MapFrom(s => s.Books.Where(s => s.BkBc_Active).Select(s => s.Book)))
                .ForMember(s => s.BookItems, z => z.MapFrom(s => s.Books.Where(s => s.BkBc_Active).Select(s => new ItemDto(s.Bk_ID, s.BkBc_Ordinal))));

            CreateMap<BookCollectionCmdDto, BookCollection>()
                .ForMember(s => s.Books, z => z.Ignore());

            //Profile
            CreateMap<AppUser, AppUser>();
            CreateMap<AppUser, ProfileDto>()
                .ForMember(s => s.Us_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Us_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName))
                .ForMember(s => s.Addresses, z => z.MapFrom(y => y.Addresses.Where(s => s.Ad_Active)));

            CreateMap<AppUser, ProfileQryDto>()
                .ForMember(s => s.Genres, z => z.MapFrom(s => s.Genres.Where(s => s.CsGn_Active).Select(s => s.Genre)))
                .ForMember(s => s.GenreItems, z => z.MapFrom(s => s.Genres.Where(s => s.CsGn_Active).Select(s => new ItemDto(s.Gn_ID, s.CsGn_Ordinal))))
                .ForMember(s => s.ThematicAreas, z => z.MapFrom(s => s.ThematicAreas.Where(s => s.CsTh_Active).Select(s => s.ThematicArea)))
                .ForMember(s => s.ThematicAreaItems, z => z.MapFrom(s => s.ThematicAreas.Where(s => s.CsTh_Active).Select(s => new ItemDto(s.Th_ID, s.CsTh_Ordinal))))
                .ForMember(s => s.Categories, z => z.MapFrom(s => s.Categories.Where(s => s.CsCt_Active).Select(s => s.Category)))
                .ForMember(s => s.CategoryItems, z => z.MapFrom(s => s.Categories.Where(s => s.CsCt_Active).Select(s => new ItemDto(s.Ct_ID, s.CsCt_Ordinal))))
                .ForMember(s => s.Tags, z => z.MapFrom(s => s.Tags.Where(s => s.CsTg_Active).Select(s => s.Tag)))
                .ForMember(s => s.TagItems, z => z.MapFrom(s => s.Tags.Where(s => s.CsTg_Active).Select(s => new ItemDto(s.Tg_ID, s.CsTg_Ordinal))))
                .ForMember(s => s.Us_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName))
                .ForMember(s => s.Us_Modifier, z => z.MapFrom(s => s.Modifier.Us_DisplayName))
                .ForMember(s => s.Addresses, z => z.MapFrom(y => y.Addresses.Where(s => s.Ad_Active)));


            CreateMap<ProfileDto, AppUser>();
            CreateMap<ProfileQryDto, AppUser>()
                .ForMember(s => s.Genres, z => z.Ignore())
                .ForMember(s => s.Categories, z => z.Ignore())
                .ForMember(s => s.Tags, z => z.Ignore())
                .ForMember(s => s.ThematicAreas, z => z.Ignore());

            CreateMap<AppUser, ProfileCmdDto>();

            // Gift Payment
            CreateMap<GiftPayment, GiftPaymentCmdDto>();

            CreateMap<GiftPaymentCmdDto, GiftPayment>();

            CreateMap<SuggestBookCmd, SuggestBook>();
            CreateMap<SuggestBook, SuggestBookQryDto>();

            CreateMap<GiftPayment, GiftPaymentDto>()
                .ForMember(s => s.Pm_Subscriber, z => z.MapFrom(s => s.Pm_Creator))
                //.ForMember(s => s.SubscriberName, z => z.MapFrom(s => s.PM_Recipient))
                //.ForMember(s => s.SubscriberPhoneNumber, z => z.MapFrom(s => s.Subscriber.PhoneNumber))
                //.ForMember(s => s.SubscriberEmail, z => z.MapFrom(s => s.Subscriber.Email))
                .ForMember(s => s.PaymentMethod, z => z.MapFrom(s => s.PaymentMethod.Py_Title))
                .ForMember(s => s.PaymentMethod_Ar, z => z.MapFrom(s => s.PaymentMethod.Py_Title_Ar))
                .ForMember(s => s.Subscription, z => z.MapFrom(s => s.Subscription.Sb_Name))
                .ForMember(s => s.Subscription_Ar, z => z.MapFrom(s => s.Subscription.Sb_Name_Ar))
                .ForMember(s => s.Pm_Creator, z => z.MapFrom(s => s.Creator.Us_DisplayName));

        }
    }
}