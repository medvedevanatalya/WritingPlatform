using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using WritingPlatform.BusinessLayer.BusinessObjects;
using WritingPlatform.DataLayer;
using WritingPlatform.Web.Models;

namespace WritingPlatform.Web.App_Start
{
    public class AutomapperConfig
    {
        public static void RegisterWithUnity(IUnityContainer container)
        {
            IMapper mapper = CreateMapperConfig().CreateMapper();

            container.RegisterInstance<IMapper>(mapper);
        }

        static MapperConfiguration CreateMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Roles, RoleBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<RoleBO>());

                cfg.CreateMap<RoleBO, RoleViewModel>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<RoleViewModel>());

                cfg.CreateMap<RoleViewModel, RoleBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<RoleBO>());

                cfg.CreateMap<RoleBO, Roles>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Roles>());



                cfg.CreateMap<Users, UserBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());

                cfg.CreateMap<UserBO, UserViewModel>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<UserViewModel>());

                cfg.CreateMap<UserBO, RegisterModel>()
               .ConstructUsing(item => DependencyResolver.Current.GetService<RegisterModel>()); 
                cfg.CreateMap<UserBO, LoginModel>()
               .ConstructUsing(item => DependencyResolver.Current.GetService<LoginModel>());

                cfg.CreateMap<UserViewModel, UserBO>() 
                .ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());

                cfg.CreateMap<RegisterModel, UserBO>() 
                .ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());
                cfg.CreateMap<LoginModel, UserBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());

                cfg.CreateMap<UserBO, Users>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Users>());



                cfg.CreateMap<Genres, GenreBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<GenreBO>());

                cfg.CreateMap<GenreBO, GenreViewModel>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<GenreViewModel>());

                cfg.CreateMap<GenreViewModel, GenreBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<GenreBO>());

                cfg.CreateMap<GenreBO, Genres>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Genres>());



                cfg.CreateMap<WritersWorks, WriterWorkBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<WriterWorkBO>());

                cfg.CreateMap<WriterWorkBO, WriterWorkViewModel>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<WriterWorkViewModel>());

                cfg.CreateMap<WriterWorkViewModel, WriterWorkBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<WriterWorkBO>());

                cfg.CreateMap<WriterWorkBO, WritersWorks>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<WritersWorks>());

                                       

                cfg.CreateMap<CommentsUsers, CommentUserBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<CommentUserBO>());

                cfg.CreateMap<CommentUserBO, CommentUserViewModel>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<CommentUserViewModel>());

                cfg.CreateMap<CommentUserViewModel, CommentUserBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<CommentUserBO>());

                cfg.CreateMap<CommentUserBO, CommentsUsers>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<CommentsUsers>());   
            } );
        }
    }
}