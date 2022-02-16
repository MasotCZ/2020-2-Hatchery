using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    [Serializable]
    public class Ingredient
    {
        public Ingredient(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"[{Name}|{Count}]";
        }
    }

    [Serializable]
    public class CookingRecipe
    {
        public CookingRecipe(string name, ICollection<Ingredient> ingredients)
        {
            Name = name;
            Ingredients = ingredients;
        }

        public string Name { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{{{Name}");
            foreach (var item in Ingredients)
            {
                sb.Append(item);
            }

            sb.Append("}");
            return sb.ToString();
        }
    }
}
