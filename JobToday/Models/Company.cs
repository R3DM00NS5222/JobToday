namespace JobToday.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        //multiple referrences of multiple postings
        public List<JobPosting>? Postings { get; set; }

    }
}
