using AutoMapper;
using ToDoServer.Models;
using ToDoServer.Resources;
using System;

namespace ToDoServer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /** Model => Ressource */
            CreateMap<ToDoItemList, ToDoItemListRessource>()
                .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.ToDoItems.Count))
                .ForMember(dest => dest.ToDoItems, opts => opts.MapFrom(src => src.ToDoItems));
            CreateMap<ToDoItem, ToDoItemRessource>();

            /** Ressource => Model */
            CreateMap<ToDoItemListRessource, ToDoItemList>();
            CreateMap<ToDoItemRessource, ToDoItem>();

        }
    }
}
