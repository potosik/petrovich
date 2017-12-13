using Petrovich.Context.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context.Entities
{
    public class Client : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClientId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address { get; set; }
        public string Registered { get; set; }

        [Required]
        public string PassportId { get; set; }
        public string PassportData { get; set; }
        public string PersonalId { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhonesJson { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
    }
}
