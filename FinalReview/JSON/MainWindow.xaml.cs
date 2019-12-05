using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClearAll();
        }

       

        private void StartBTN_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            List<Movies> mov = new List<Movies>();
            List<Movies> highestIMDBScores = new List<Movies>();
            //Getting all of the data to perform analytics
            mov = GettingDataFromWebService(mov);
            //1. List all of the different genres for the movies

            GetAllGenresForMovies(mov);

            //2. Which movie has the highest IMDB score ?

            GetHighestIMDBScoreMovies(mov, highestIMDBScores);

            //3. List all of the different movies that have a number of voted users with 350000 or more

            GetAllMoviesWithVotedUsersGreaterThan(mov, 350000);

            //4. How many movies where Anthony Russo is the director ?
            HowManyMoviesRussoDirected(mov);
            //5. How many movies where Robert Downey Jr. is the actor 1 ?
            HowManyMoviesRDJWasActor1(mov);
        }

        private void HowManyMoviesRDJWasActor1(List<Movies> mov)
        {
            int count = 0;
            foreach (var movie in mov)
            {
                if (movie.actor_1_name == "Robert Downey Jr.")
                {
                    count++;

                }
            }
            rdjActorTXT.Text = count.ToString("N0");
        }

        private void HowManyMoviesRussoDirected(List<Movies> mov)
        {
            int count = 0;
            foreach (var movie in mov)
            {
                if (movie.director_name == "Anthony Russo")
                {
                    count++;

                }
            }
            RussoMoviesTXT.Text = count.ToString("N0");
            
        }

        private void GetAllMoviesWithVotedUsersGreaterThan(List<Movies> movies, int v)
        {
            foreach (var movie in movies)
            {
                if(movie.num_voted_users>=v)
                {
                    Hyperlink h = new Hyperlink();
                    h.NavigateUri = new Uri(movie.movie_imdb_link);
                    h.Inlines.Add(movie.movie_title);
                    h.RequestNavigate += LinkOnRequestNavigate;
                    
                    votedUsersLB.Items.Add(h);
                }
            }
        }

        private void GetHighestIMDBScoreMovies(List<Movies> mov, List<Movies> highestIMDBScores)
        {
            foreach (var movie in mov)
            {
                if (highestIMDBScores.Count < 1)
                {
                    highestIMDBScores.Add(movie);
                    continue;
                }
                else
                {
                    if (highestIMDBScores[0].imdb_score < movie.imdb_score)
                    {
                        highestIMDBScores.Clear();
                        highestIMDBScores.Add(movie);
                    }
                    else if (highestIMDBScores[0].imdb_score == movie.imdb_score)
                    {
                        highestIMDBScores.Add(movie);
                    }
                    else
                    {
                        //don't need to add to the list or clear the list
                    }
                }
            }
            if (highestIMDBScores.Count() > 1)
            {
                string content = "";
                foreach (var m in highestIMDBScores)
                {
                    content += m.movie_title + '\n';
                }
                imdbScoreLB.Items.Add(content);
            }
            else
            {
                Hyperlink h = new Hyperlink();
                h.NavigateUri = new Uri(highestIMDBScores[0].movie_imdb_link);
                h.Inlines.Add(highestIMDBScores[0].movie_title);
                h.RequestNavigate += LinkOnRequestNavigate;
                imdbScoreLB.Items.Add(h);
            }
        }

        private void LinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void GetAllGenresForMovies(List<Movies> mov)
        {
            //To Do: Break out all the genres and limit repeating genres
            Dictionary<string, int> genres = new Dictionary<string, int>();
            foreach (var movie in mov)
            {
                if (movie.genres.Contains("|"))
                {
                    var gs = movie.genres.Split('|');
                    foreach (var g in gs)
                    {
                        if(genres.ContainsKey(g))
                        {
                            genres[g] = genres[g] + 1;
                        }
                        else
                        {
                            genres.Add(g, 1);
                        }
                        //genreLB.Items.Add(g);
                    }
                }
                else
                {
                    if (genres.ContainsKey(movie.genres))
                    {
                        genres[movie.genres] = genres[movie.genres] + 1;
                    }
                    else
                    {
                        genres.Add(movie.genres, 1);
                    }
                    //genreLB.Items.Add(movie.genres);
                }
            }
            foreach (var key in genres.Keys)
            {
                genreLB.Items.Add($"{key}({genres[key].ToString("N0")})");

            }
        }

        private static List<Movies> GettingDataFromWebService(List<Movies> mov)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(@"http://pcbstuou.w27.wh-2.com/webservices/3033/api/Movies?number=100").Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    mov = JsonConvert.DeserializeObject<List<Movies>>(content);

                    var x = JsonConvert.SerializeObject(mov);
                }
            }

            return mov;
        }

        private void ClearAll()
        {
            rdjActorTXT.Clear();
            RussoMoviesTXT.Clear();
            votedUsersLB.Items.Clear();
            genreLB.Items.Clear();
            imdbScoreLB.Items.Clear();
        }
    }
}
