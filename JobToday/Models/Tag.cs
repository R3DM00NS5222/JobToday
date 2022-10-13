using System.ComponentModel.DataAnnotations;

namespace JobToday.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        [Required]
        public string TagName { get; set; }

        public List<JobPosting>? JobPostings { get; set; }
    }
}
