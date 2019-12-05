using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    class Movies
    {
        public int movie_Id { get; set; }
        public string director_name { get; set; }
        public string genres { get; set; }
        public string actor_1_name { get; set; }
        public string movie_title { get; set; }
        public int num_voted_users { get; set; }
        public string movie_imdb_link { get; set; }
        public double imdb_score { get; set; }

        public Movies()
        {
            movie_Id = 0;
            director_name = string.Empty;
            genres = string.Empty;
            actor_1_name = string.Empty;
            movie_title = string.Empty;
            num_voted_users = 0;
            movie_imdb_link = string.Empty;
            imdb_score = 0;
        }

        public override string ToString()
        {
            return movie_title + " directed by " + director_name;
        }
    }

}
