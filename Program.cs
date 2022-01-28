// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;

namespace CoreConsole
{
    class Program
    {
        HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.GetCartoons();
        }

        private async Task GetCartoons()
        {
            string response = await client.GetStringAsync(
                "https://api.sampleapis.com/cartoons/cartoons2D");


            List<Cartoons> cartoons = JsonConvert.DeserializeObject<List<Cartoons>>(response);

            List<Genres> genres = new List<Genres>();

            List<Series> series = new List<Series>();


            foreach (var item in cartoons)
            {

                series.Add(new Series() { name = item.title});

                if (item.year < 2000)
                {
                    foreach (string genre in item.genre)
                    {

                        

                        if (!genres.Exists(x => x.genre == genre))
                        {
                            genres.Add(new Genres() { genre = genre });                     
                        }

                        
                        
                    }



                }

                foreach (var genre in genres)
                {
                    foreach (string titolo in item.genre)
                    {
                        if (titolo == genre.genre)
                        {

                            genre.movies = new List<Series> {
                                new Series {title = item.title }
                                };
                            Console.WriteLine(JsonConvert.SerializeObject(genre));
                        }
                    }

                }
            }

        }

    }

    class Cartoons
    {
        public string title { get; set; }
        public int year { get; set; }
        public List<string> creator { get; set; }
        public string rating { get; set; }
        public List<string> genre { get; set; }
        public int runtime_in_minutes { get; set; }
        public int episodes { get; set; }
        public string image { get; set; }
        public int id { get; set; }
    }

    class Genres
    {
        public string genre { get; set; }
        public List<Series> movies { get; set; }
    }

    class Series
    {
        public string title { get; set; }

    }
}

