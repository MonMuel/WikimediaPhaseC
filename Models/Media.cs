using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public enum MediaSortBy { Title, PublishDate, Likes }

    public class Media : Record
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string YoutubeId { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;

        public int OwnerId { get; set; } = 1;
        public bool Shared { get; set; } = true;
        [JsonIgnore]
        public User Owner => DB.Users.Get(OwnerId).Copy();

        // likes stored as list of user ids
        public List<int> Likes { get; set; } = new List<int>();

        [JsonIgnore]
        public int LikesCount { get { return Likes != null ? Likes.Count : 0; } }

        [JsonIgnore]
        public List<User> LikedByUsers
        {
            get
            {
                var list = new List<User>();
                if (Likes != null)
                {
                    foreach (var id in Likes)
                    {
                        var u = DB.Users.Get(id);
                        if (u != null) list.Add(u.Copy());
                    }
                }
                return list;
            }
        }

        public override bool IsValid()
        {
            if (!HasRequiredLength(Title, 1)) return false;
            if (!HasRequiredLength(Category, 1)) return false;
            if (!HasRequiredLength(Description, 1)) return false;
            if (DB.Medias.ToList().Where(m => m.YoutubeId == YoutubeId && m.Id != Id).Any()) return false;
            return true;
        }
    }
}