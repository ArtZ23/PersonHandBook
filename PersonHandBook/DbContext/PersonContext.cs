using Microsoft.EntityFrameworkCore;
using PersonHandBook.Models;

namespace PersonHandBook
{
	public class PersonContext : DbContext
	{
		public PersonContext(DbContextOptions<PersonContext> options)
			  : base(options)
		{
		}
		public DbSet<Person> Persons { get; set; }
		public DbSet<Phone> Phones { get; set; }
		public DbSet<RelatedPerson> RelatedPersons { get; set; }
	}

}
