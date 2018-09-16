using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Category>
            {
                new Category
                {
                    Id = "1",
                    Description = "Computers"
                },
                new Category
                {
                    Id = "2",
                    ParentId = "1",
                    Description = "Notebooks"
                },
                new Category
                {
                    Id = "3",
                    ParentId = "2",
                    Description = "Notebook's subcategory"
                },
                new Category
                {
                    Id = "4",
                    Description = "Books"
                },
                new Category
                {
                    Id = "5",
                    ParentId = "4",
                    Description = "Biographies"
                }
            };
            var tree = list.BuildTree(k => k.Id, g => g.ParentId);

            // print Categories Tree as Json
            Console.WriteLine(JsonConvert.SerializeObject(tree, Formatting.Indented));
        }
    }

    internal class Category
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ParentId { get; set; }
    }
}
