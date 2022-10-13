using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace JobToday.Models
{

    public class JobPosting

	{
        public int JobPostingId { get; set; }
        public int TagId { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public string JobPostName { get; set; }
        [Required]
        public string PostedBy { get; set; }
        [Required]
        
        public string CompanyName { get; set; }
        [Required]
        public string JobPostStatus { get; set; }

        [Range(0.01, 999)]
        [DisplayFormat(DataFormatString = "{0:c}")] //MS currency Format
        public double JobPay { get; set; }
        [Required]
        public string JobPostDescription { get; set; }

        //references the parent class
        public Company? Company { get; set; }
        
        public Tag? Tag { get; set; }
       
	}
}