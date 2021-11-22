using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaMVC.Models
{
    public class ImagesVotes
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Image ID")]

        public int ImagesID { get; set; }
        public Images Images { get; set; }

        public int RegisterID { get; set; }
        public Register Register { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [DisplayName("Vote")]
        public string Vote { get; set; }



    }
}
