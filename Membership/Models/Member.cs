using System.ComponentModel.DataAnnotations;

namespace Membership.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int memberId { get; set; }

        public Member(int id, string name, int memberId)
        {
            Id = id;
            Name = name;
            this.memberId = memberId;
        }
    }
}
