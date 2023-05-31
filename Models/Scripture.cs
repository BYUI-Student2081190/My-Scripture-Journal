using System.ComponentModel.DataAnnotations;

namespace My_Scripture_Journal.Models
{
    public class Scripture
    {
        /*Attributes for a Scripture*/
        // Primary key for Object.
        public int Id { get; set; }
        // The Book of Scripture with chapter and verse.
        public string? Book { get; set; }
        public int Chapter { get; set; }
        public int Verse { get; set; }
        // Notes made by user.
        public string? Notes { get; set; }

        // Date the scripture was added to collection.
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
    }
}
