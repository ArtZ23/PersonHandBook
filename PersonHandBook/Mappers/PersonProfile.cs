using AutoMapper;
using PersonHandBook.Dtos;
using PersonHandBook.Models;
using PersonHandBook.Models.UpdatePerson;

namespace PersonHandBook
{
	public class PersonProfile : Profile
	{
		public PersonProfile()
		{
			CreateMap<PersonDto, Person>()
				.ForMember(x => x.ImagePath, opt => opt.Ignore())
				.ForMember(x => x.Id, opt => opt.Ignore());
			CreateMap<PhoneDto, Phone>()
				.ForMember(x => x.Id, opt => opt.Ignore())
				.ForMember(x => x.PersonId, opt => opt.Ignore());
			CreateMap<RelatedPersonDto, RelatedPerson>();

			CreateMap<Person, PersonDto>()
				.ForMember(x => x.Image, opt => opt.Ignore());
			CreateMap<Phone, PhoneDto>();
			CreateMap<RelatedPerson, RelatedPersonDto>();

			//CreateMap<Person, PersonUpdateModel>()
			//		.ForMember(x => x.Image, opt => opt.Ignore());
			//CreateMap<Phone, PhoneUpdateModel>();
			//CreateMap<RelatedPerson, RelatedPersonUpdateModel>();

			//CreateMap<PersonUpdateModel, Person>();
			//CreateMap<PhoneUpdateModel, Phone>();
			//CreateMap<RelatedPersonUpdateModel, RelatedPerson>()
			//	.ForMember(x => x.Id, opt => opt.Ignore());
		}
	}
}
