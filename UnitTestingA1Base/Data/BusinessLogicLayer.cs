using UnitTestingA1Base.Models;

namespace UnitTestingA1Base.Data
{
    public class BusinessLogicLayer
    {
        private AppStorage _appStorage;

        public BusinessLogicLayer(AppStorage appStorage) {
            _appStorage = appStorage;
        }

        public class RecipeIngredientRequest
        {
            public Recipe Recipe { get; set; }
            public List<Ingredient> Ingredients { get; set; }
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
                        recipes = GetRecipesByIngredientHelper(ingredient);
                    }
                }else
                {
                    ingredient = GetIngredientById(id.Value);
                    if (ingredient != null)
                    {
                        recipes = GetRecipesByIngredientHelper(ingredient);
                    } else
                    {
                        ingredient = GetIngredientByName(name);

                        if (ingredient != null)
                        {
                            recipes = GetRecipesByIngredientHelper(ingredient);
                        }
                    }
                }
            }else if(!string.IsNullOrEmpty(name))
            {
                ingredient = GetIngredientByName(name);

                if (ingredient != null)
                {
                    recipes = GetRecipesByIngredientHelper(ingredient);
                }
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
                    diet = _appStorage.DietaryRestrictions.First(dr => dr.Id == id);

                    if (diet != null)
                    {
                        recipes = GetRecipesByDietHelper(diet);
                    }
                } else
                {
                    diet = _appStorage.DietaryRestrictions.FirstOrDefault(dr => dr.Id == id);

                    if(diet != null)
                    {
                        recipes = GetRecipesByDietHelper(diet);
                    } else
                    {
                        diet = _appStorage.DietaryRestrictions
                            .FirstOrDefault(dR => dR.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

                        if (diet != null)
                        {
                            recipes = GetRecipesByDietHelper(diet);
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(name))
            {
                diet = _appStorage.DietaryRestrictions
                    .FirstOrDefault(dR => dR.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

                if (diet != null)
                {
                    recipes = GetRecipesByDietHelper(diet);
                }
            }
            return recipes;
        }

        public HashSet<Recipe> GetRecipesByNameOrId(int? id, string? name)
        {
            HashSet<Recipe> recipes = new HashSet<Recipe>();
            if (id != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    Recipe recipeById = _appStorage.Recipes.First(r => r.Id == id);
                    if (recipeById != null)
                    {
                        recipes.Add(recipeById);
                    }
                }else
                {
                    Recipe? recipeById = _appStorage.Recipes.FirstOrDefault(r => r.Id == id);
                    if (recipeById != null)
                    {
                        recipes.Add(recipeById);
                    } else
                    {
                        recipes = _appStorage.Recipes.Where(r => r.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToHashSet();
                    }
                }
            }else if (!string.IsNullOrEmpty(name))
            {
                recipes = _appStorage.Recipes.Where(r => r.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToHashSet();
            }

            return recipes;
        }

        public void AddRecipeWithIngredients(RecipeIngredientRequest request)
        {
            if (RecipeExists(request.Recipe.Name))
            {
                throw new InvalidOperationException("Recipe with the same name already exists.");
            }
            request.Recipe.Id = _appStorage.GeneratePrimaryKey();

            foreach (Ingredient ingredient in request.Ingredients)
            {
                Ingredient? existingIngredient = GetIngredientByName(ingredient.Name);
                if (existingIngredient != null)
                {
                    ingredient.Id = existingIngredient.Id;
                }
                else
                {
                    ingredient.Id = _appStorage.GeneratePrimaryKey();
                    AddIngredient(ingredient);
                }
            }

            AddRecipe(request.Recipe);

            foreach (Ingredient ingredient in request.Ingredients)
            {
                var recipeIngredient = new RecipeIngredient
                {
                    RecipeId = request.Recipe.Id,
                    IngredientId = ingredient.Id,
                };

                AddRecipeIngredient(recipeIngredient);
            }
        }

        public void DeleteIngredient(int? id, string? name)
        {
            Ingredient? ingredientToDelete;

            if (id != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    ingredientToDelete = GetIngredientById(id.Value);

                    if (ingredientToDelete == null)
                    {
                        throw new InvalidOperationException("Ingredient not found by ID.");
                    }

                    DeleteIngredientConfirm(ingredientToDelete);
                } else
                {
                    ingredientToDelete = GetIngredientById(id.Value);

                    if (ingredientToDelete != null)
                    {
                        DeleteIngredientConfirm(ingredientToDelete);
                    } else
                    {
                        ingredientToDelete = GetIngredientByName(name);

                        if(ingredientToDelete != null)
                        {
                            DeleteIngredientConfirm(ingredientToDelete);
                        }
                    }
                    
                }
            }
            else if (!string.IsNullOrEmpty(name))
            {
                ingredientToDelete = GetIngredientByName(name);
                
                if (ingredientToDelete == null)
                {
                    throw new InvalidOperationException("Ingredient not found by Name.");
                }

                DeleteIngredientConfirm(ingredientToDelete);
            }
            else
            {
                throw new InvalidOperationException("Invalid request. Please provide either an ID or a name.");
            }
        }

        public void DeleteRecipe(int? id, string? name)
        {
            HashSet<Recipe>? recipeToDelete;

            if (id == null && name == null)
            {
                throw new InvalidOperationException("Invalid request. Please provide either an ID or a name.");
            } else
            {
                recipeToDelete = GetRecipesByNameOrId(id.Value, name);
            }

            if(recipeToDelete.Count == 0)
            {
                throw new InvalidOperationException("Recipe(s) not found.");
            } else
            {
                DeleteRecipeConfirm(recipeToDelete);
            }
        }

        #region HelperMethod

        public Ingredient? GetIngredientById(int id)
        {
            Ingredient? ingredient = new Ingredient();
            ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Id == id);
            return ingredient;
        }

        public Ingredient? GetIngredientByName(string name)
        {
            Ingredient? ingredient = new Ingredient();
            ingredient = _appStorage.Ingredients
                            .FirstOrDefault(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            return ingredient;
        }

        private void DeleteIngredientConfirm(Ingredient ingredient)
        {
            Ingredient ingredientToDelete = ingredient;
            List<RecipeIngredient> recipeIngredient = new List<RecipeIngredient>();
            recipeIngredient = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToList();

            if(recipeIngredient.Count == 1)
            {
                Recipe recipe = new Recipe();
                recipe = _appStorage.Recipes.First(r => r.Id == recipeIngredient[0].RecipeId);

                _appStorage.Ingredients.Remove(ingredientToDelete);
                _appStorage.RecipeIngredients.Remove(recipeIngredient[0]);
                _appStorage.Recipes.Remove(recipe);
            }else
            {
                throw new InvalidOperationException("Cannot delete ingredient with multiple associated recipes.");
            }
        }

        private void DeleteRecipeConfirm(HashSet<Recipe> recipeToDelete)
        {
            foreach (Recipe r in recipeToDelete)
            {
                _appStorage.Recipes.Remove(r);
            }

            HashSet<RecipeIngredient> recipeIngredientsToDelete = new HashSet<RecipeIngredient>(
                _appStorage.RecipeIngredients.Where(rI => recipeToDelete.Any(r => r.Id == rI.RecipeId)));

            foreach (RecipeIngredient rI in recipeIngredientsToDelete)
            {
                _appStorage.RecipeIngredients.Remove(rI);
            }
        }

        public bool RecipeExists(string recipeName)
        {
            return _appStorage.Recipes.Any(recipe => string.Equals(recipe.Name, recipeName, StringComparison.OrdinalIgnoreCase));
        }

        private void AddIngredient(Ingredient ingredient)
        {
            _appStorage.Ingredients.Add(ingredient);
        }

        private void AddRecipe(Recipe recipe)
        {
            _appStorage.Recipes.Add(recipe);
        }

        private void AddRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            _appStorage.RecipeIngredients.Add(recipeIngredient);
        }

        public HashSet<Recipe> GetRecipesByIngredientHelper(Ingredient ingredient)
        {
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                .Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();
            recipes.UnionWith(_appStorage.Recipes
                .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                .ToHashSet());

            return recipes;
        }

        public HashSet<Recipe> GetRecipesByDietHelper(DietaryRestriction diet)
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

        #endregion

    }
}
