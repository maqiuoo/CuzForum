using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model.DTO
{
	public class OrganizationProfile : Profile
	{
		public OrganizationProfile()
		{
			CreateMap<EmploymentData, EmploymentDataDTO>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Region)) // 将Region映射到name
			.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Population)); // 将Population映射到value
			CreateMap<ReptileData, ReptileDataDTO>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BookTitle))
				.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Clicks));
		}
	}
}
