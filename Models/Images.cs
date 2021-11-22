using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaMVC.Models
{
    public class Images
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Description")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("File Name")]
        public string Filename { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [DisplayName("File type ")]
        public string Filetype { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("File path")]
        public string Path { get; set; }

        [DisplayName("Rating")]
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        public List<ImagesVotes> ImagesVotes { get; set; }
    }
}
