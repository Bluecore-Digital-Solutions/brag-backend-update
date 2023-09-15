


using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace Brag.SharedServices.Helpers.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {  //CreateMap<TSource,TDestination>()
            //Define your Dto,param, Entities mapping here 

            //CreateMap<ProfileDto, UserProfile>().ReverseMap(); 
             
        }
    }
}
