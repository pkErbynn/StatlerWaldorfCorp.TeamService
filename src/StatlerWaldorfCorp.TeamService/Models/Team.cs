using System;
namespace StatlerWaldorfCorp.TeamService.Models
{
	public class Team
	{
		public string Name { get; set; }
		public Guid Id { get; set; }	// Member Id
		public ICollection<Member> Members { get; set; }

		public Team()
		{
			this.Members = new List<Member>();
		}

		public Team(string name): this()
		{
			this.Name = name;
		}

		public Team(string name, Guid id): this(name)
		{
			this.Name = name;
			this.Id = id;
		}

        public override string ToString()
        {
			return this.Name;
        }
    }
}

