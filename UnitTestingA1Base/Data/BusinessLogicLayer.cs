using System.Linq;
using UnitTestingA1Base.Models;

namespace UnitTestingA1Base.Data
{
    public class BusinessLogicLayer
    {
        private AppStorage _appStorage;

        public BusinessLogicLayer(AppStorage appStorage) {
            _appStorage = appStorage;
        }
        public HashSet<Recipe> GetRecipesByIngredient(int? id, string? name)
        {
            Ingredient? ingredient;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    ingredient = _appStorage.Ingredients.First(i => i.Id == id);
                    if(ingredient != null)
                    {
                        HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();
                        recipes = _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();
                    }
                }
            }

            if (recipes.Count == 0 && !string.IsNullOrEmpty(name))
            {
                recipes = _appStorage.Recipes
                    .Where(r => _appStorage.RecipeIngredients
                        .Any(ri => ri.RecipeId == r.Id && _appStorage.Ingredients
                            .Any(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase) && i.Id == ri.IngredientId)))
                    .ToHashSet();
            }

            return recipes;
        }

        public HashSet<Recipe> GetRecipesByDiet(int? id, string? name)
        {
            HashSet<Recipe> recipes = new HashSet<Recipe>();
            DietaryRestriction? diet;

            if (id != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    // search by Id
                    diet = _appStorage.DietaryRestrictions.First(dr => dr.Id == id);

                    if (diet != null)
                    {
                        recipes = GetRecipesByDietHelper_ById(diet);
                    }
                } else
                {
                    diet = _appStorage.DietaryRestrictions.FirstOrDefault(dr => dr.Id == id);

                    if(diet != null)
                    {
                        // search by Id
                        recipes = GetRecipesByDietHelper_ById(diet);
                    } else
                    {
                        // search by name
                        diet = _appStorage.DietaryRestrictions
                            .FirstOrDefault(dR => dR.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

                        if (diet != null)
                        {
                            recipes = GetRecipesByDietHelper_ByName(diet);
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(name))
            {
                // search by name
                diet = _appStorage.DietaryRestrictions
                    .FirstOrDefault(dR => dR.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

                if (diet != null)
                {
                    recipes = GetRecipesByDietHelper_ByName(diet);
                }
            }
            return recipes;
        }

        private HashSet<Recipe> GetRecipesByDietHelper_ById(DietaryRestriction diet)
        {
            HashSet<Recipe> recipes = new HashSet<Recipe>();
            HashSet<IngredientRestriction> ingredientRestrictions = _appStorage.IngredientRestrictions
                .Where(ir => ir.DietaryRestrictionId == diet.Id)
                .ToHashSet();

            foreach (IngredientRestriction iR in ingredientRestrictions)
            {
                HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                    .Where(rI => rI.IngredientId == iR.IngredientId)
                    .ToHashSet();

                recipes.UnionWith(_appStorage.Recipes
                    .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                    .ToHashSet());
            }

            return recipes;
        }

        private HashSet<Recipe> GetRecipesByDietHelper_ByName(DietaryRestriction diet)
        {
            HashSet<Recipe> recipes = new HashSet<Recipe>();            
            HashSet<IngredientRestriction> ingredientRestrictions = _appStorage.IngredientRestrictions
                .Where(ir => ir.DietaryRestrictionId == diet.Id)
                .ToHashSet();

            foreach (IngredientRestriction iR in ingredientRestrictions)
            {
                HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                    .Where(rI => rI.IngredientId == iR.IngredientId)
                    .ToHashSet();

                recipes.UnionWith(_appStorage.Recipes
                    .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                    .ToHashSet());
            }            
            return recipes;
        }


    }



}
