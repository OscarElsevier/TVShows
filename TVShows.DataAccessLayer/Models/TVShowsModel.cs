using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVShows.DataAccessLayer.Models
{
    public class TVShowsModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int ID { get; set; }
        public string TVShowName { get; set; }
        public bool IsFavorite { get; set; }
    }
}
