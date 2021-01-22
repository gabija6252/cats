using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace MyLibrary
{
    public class Cat
    {
        public string id { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string url { get; set; }

        public override string ToString() {
            return id + ": " + url;
        }
    }

    public class Breed
    {
        public string id { get; set; }
        public string name { get; set; }
        public string temperament { get; set; }
        public string life_span { get; set; }
        public string alt_names { get; set; }
        public string origin { get; set; }

        public override string ToString()
        {
            return id + ": " + name;
        }
    }

    public class API
    {


        public Cat[] getCats(string breed)
        {
            using (var client = new HttpClient())

            {
                var result = client.GetAsync("https://api.thecatapi.com/v1/images/search?breed_ids=" + breed);
                var content = result.Result.Content.ReadAsStringAsync();

                string jsonString = content.Result.ToString();

                Cat[] cats = JsonConvert.DeserializeObject<Cat[]>(jsonString);

                //string[] output = Array.ConvertAll(cats, item => item.ToString());

                //Console.WriteLine(string.Join("\n", output));

                return cats;

            }
        }

        public Breed[] getBreeds()
        {
            using (var client = new HttpClient())

            {
                var result = client.GetAsync("https://api.thecatapi.com/v1/breeds");
                var content = result.Result.Content.ReadAsStringAsync();

                string jsonString = content.Result.ToString();

                Breed[] breeds = JsonConvert.DeserializeObject<Breed[]>(jsonString);

                //string[] output = Array.ConvertAll(breeds, item => item.ToString());

                //Console.WriteLine(string.Join("\n", output));

                return breeds;

            }
        }

        public Stream getImage(string url)
        {
            using (var client = new HttpClient())

            {
                var result = client.GetAsync(url);
                var content = result.Result.Content.ReadAsStreamAsync();

                Stream image = content.Result;

                return image;

                //Console.WriteLine(image);

                //string[] output = Array.ConvertAll(breeds, item => item.ToString());

                //Console.WriteLine(string.Join("\n", output));

            }
        }
    }
}
