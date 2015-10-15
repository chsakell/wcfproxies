using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfProxies.Data.Entities
{
    public class Post : IEntity
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public string URI { get; set; }

        public int BlogId { get; set; }

        public int ID
        {
            get { return PostId; }
            set { PostId = value; }
        }
    }
}
