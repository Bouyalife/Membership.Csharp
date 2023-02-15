using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Membership.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string name { get; set; }

        public long memberId { get; set; }

        public int points { get; set; }

        public string memberType { get; set; }

        public Member(string name, long memberId, int points, string memberType)
        {
            this.points = points;
            this.memberType = memberType;
            this.name = name;
            this.memberId = memberId;
        }
        public Member(string name, long memberId)
        {
            this.name = name;
            this.memberId = memberId;
        }
    }
}
