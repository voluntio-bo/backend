using System.ComponentModel.DataAnnotations;
using Voluntio.Data.Entity;

namespace Voluntio.Models
{
    public class OrganizationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string WhatsAppUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string WebPageUrl { get; set; }
        public string? CoverImagePath { get; set; }
        public string? ProfileImagePath { get; set; }
        public IEnumerable<EventModel> Events { get; set; }
    }
}
