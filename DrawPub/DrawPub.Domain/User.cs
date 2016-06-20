using System.Collections.Generic;

namespace DrawPub.Domain
{
    public class User
    {
        public User()
        {
            Roles = new List<string>();
        }

        public User(int id)
        {
            Id = id;
        }
        public int Id { get;  }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Name {
            get { return $"{FirstName} {Surname}"; }
        }

        public List<string> Roles  { get; set; }

    }
}
