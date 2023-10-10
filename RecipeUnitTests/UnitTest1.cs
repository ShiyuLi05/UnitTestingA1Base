using Microsoft.AspNetCore.Http;
using UnitTestingA1Base.Data;
using UnitTestingA1Base.Models;
using static UnitTestingA1Base.Data.BusinessLogicLayer;

namespace RecipeUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private BusinessLogicLayer _initializeBusinessLogic()
        {
            return new BusinessLogicLayer(new AppStorage());
        }

        #region GetRecipesByIngredient
        [TestMethod]
        public void GetRecipesByIngredient_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;
            int recipeCount = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "egg";
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_InvalidId_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 99;

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);
            });
        }

        [TestMethod]
        public void GetRecipesByIngredient_InvalidName_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "abc";
            int recipeCount = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_ValidIdInvalidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 3;
            string ingredientName = "abc";
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_ValidIdValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 4;
            string ingredientName = "egg";
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_InValidIdValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 99;
            string ingredientName = "par";
            int recipeCount = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_InValidIdInValidName_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 99;
            string ingredientName = "abc";
            int recipeCount = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }
        #endregion

        #region GetRecipesByDiet
        [TestMethod]
        public void GetRecipesByDiet_ValidId_ReturnsRecipesWithDiet()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietId = 3;
            int recipeCount = 3;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietId, null);

            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_ValidName_ReturnsRecipesWithDiet()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string dietName = "glu";
            int recipeCount = 3;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(null, dietName);

            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_InvalidId_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietId = 99;

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietId, null);
            });
        }

        [TestMethod]
        public void GetRecipesByDiet_InvalidName_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string dietName = "abc";
            int recipeCount = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(null, dietName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_ValidIdInvalidName_ReturnsRecipesWithDiet()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietId = 3;
            string dietName = "abc";
            int recipeCount = 3;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietId, dietName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_ValidIdValidName_ReturnsRecipesWithDiet()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietId = 3;
            string dietName = "nut";
            int recipeCount = 3;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietId, dietName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_InValidIdValidName_ReturnsRecipesWithDiet()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietId = 99;
            string dietName = "glu";
            int recipeCount = 3;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietId, dietName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_InValidIdInValidName_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietId = 99;
            string dietName = "abc";
            int recipeCount = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietId, dietName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }
        #endregion

        #region GetRecipesByIdOrName
        [TestMethod]
        public void GetRecipesByIdOrName_ValidId_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 3;
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByNameOrId(recipeId, null);

            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIdOrName_ValidName_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string recipeName = "sal";
            int recipeCount = 4;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByNameOrId(null, recipeName);

            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIdOrName_InvalidId_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietId = 99;

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                HashSet<Recipe> recipes = bll.GetRecipesByNameOrId(dietId, null);
            });
        }

        [TestMethod]
        public void GetRecipesByIdOrName_InvalidName_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string recipeName = "abc";
            int recipeCount = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByNameOrId(null, recipeName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIdOrName_ValidIdInvalidName_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 3;
            string recipeName = "abc";
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByNameOrId(recipeId, recipeName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIdOrName_ValidIdValidName_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 3;
            string recipeName = "beef";
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByNameOrId(recipeId, recipeName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIdOrName_InValidIdValidName_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 99;
            string recipeName = "sal";
            int recipeCount = 4;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByNameOrId(recipeId, recipeName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIdOrName_InValidIdInValidName_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 99;
            string dietName = "abc";
            int recipeCount = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByNameOrId(recipeId, dietName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }
        #endregion

        #region AddRecipeWithIngredients
        [TestMethod]
        public void AddRecipeWithIngredients_RecipeNotExistIngredientNotExist_ShouldAddRecipe()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            RecipeIngredientRequest request = new RecipeIngredientRequest
            {
                Recipe = new Recipe { Name = "New Recipe" },
                Ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "New Ingredient" },
            }
            };
            int newRecipeId = 256;
            Recipe testNewRecipe = new Recipe();

            // Act
            bll.AddRecipeWithIngredients(request);
            testNewRecipe = bll.GetRecipesByNameOrId(newRecipeId, null).First();

            // Assert
            Assert.AreEqual(request.Recipe, testNewRecipe);
        }

        [TestMethod]
        public void AddRecipeWithIngredients_RecipeNotExistIngredientNotExist_ShouldAddIngredient()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            RecipeIngredientRequest request = new RecipeIngredientRequest
            {
                Recipe = new Recipe { Name = "New Recipe" },
                Ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "New Ingredient" },
            }
            };
            int newIngredientId = 257;

            // Act
            bll.AddRecipeWithIngredients(request);
            Ingredient ingredient = bll.GetIngredientById(newIngredientId);

            // Assert
            Assert.AreEqual(newIngredientId, ingredient.Id);
        }

        [TestMethod]
        public void AddRecipeWithIngredients_RecipeNotExistIngredientExist_ShouldAddRecipe()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            RecipeIngredientRequest request = new RecipeIngredientRequest
            {
                Recipe = new Recipe { Name = "New Recipe" },
                Ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Eggs" },
            }
            };
            int newRecipeId = 256;
            Recipe testNewRecipe = new Recipe();

            // Act
            bll.AddRecipeWithIngredients(request);
            testNewRecipe = bll.GetRecipesByNameOrId(newRecipeId, null).First();

            // Assert
            Assert.AreEqual(request.Recipe, testNewRecipe);
        }

        [TestMethod]
        public void AddRecipeWithIngredients_RecipeNotExistIngredientExist_ShouldNotAddIngredient()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            RecipeIngredientRequest request = new RecipeIngredientRequest
            {
                Recipe = new Recipe { Name = "New Recipe" },
                Ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Eggs" },
            }
            };
            int IngredientId = 2;

            // Act
            bll.AddRecipeWithIngredients(request);
            HashSet<Recipe> recipesContainingIngredient = bll.GetRecipesByIngredient(IngredientId, null);

            // Assert
            Assert.IsTrue(recipesContainingIngredient.Any(r => r.Name == "New Recipe"));
        }

        [TestMethod]
        public void AddRecipeWithIngredients_RecipeExist_ShouldNotAddRecipe()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            RecipeIngredientRequest request = new RecipeIngredientRequest
            {
                Recipe = new Recipe { Name = "Grilled Salmon" },
                Ingredients = new List<Ingredient>()
            };

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                bll.AddRecipeWithIngredients(request);
            });
        }

        [TestMethod]
        public void AddRecipeWithIngredients_EmptyInput_ShouldReturnBadRequest()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            RecipeIngredientRequest request = new RecipeIngredientRequest();

            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                bll.AddRecipeWithIngredients(request);
            });
        }
        #endregion

        #region DeleteIngredient
        [TestMethod]
        public void DeleteIngredient_ValidIdWithOneRecipe_DeleteIngredientWithId()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 2;

            // act
            bll.DeleteIngredient(ingredientId, null);
            Ingredient deletedIngredient = bll.GetIngredientById(ingredientId);

            // assert
            Assert.IsNull(deletedIngredient);
        }

        [TestMethod]
        public void DeleteIngredient_ValidIdWithOneRecipe_DeleteRecipeWithIngredientId()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 1;
            int recipeCount = 0;

            // act
            bll.DeleteIngredient(ingredientId, null);
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, "Spaghetti");

            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void DeleteIngredient_ValidIdWithMultipleRecipe_NotDeleteIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 8;

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                bll.DeleteIngredient(ingredientId, null);
            });
        }

        [TestMethod]
        public void DeleteIngredient_InvalidId_ReturnNotFound()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 99;

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                bll.DeleteIngredient(ingredientId, null);
            });
        }

        [TestMethod]
        public void DeleteIngredient_ValidNameWithOneRecipe_DeleteIngredientWithName()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Spaghetti";
            int recipeCount = 0;

            // act
            bll.DeleteIngredient(null, ingredientName);
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void DeleteIngredient_ValidNameWithOneRecipe_DeleteRecipeWithIngredientName()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Spaghetti";
            int recipeCount = 0;

            // act
            bll.DeleteIngredient(null, ingredientName);
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void DeleteIngredient_ValidNameWithMultipleRecipe_NotDeleteIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "par";

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                bll.DeleteIngredient(null, ingredientName);
            });
        }

        [TestMethod]
        public void DeleteIngredient_InvalidName_ReturnNotFound()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "abc";

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                bll.DeleteIngredient(null, ingredientName);
            });
        }

        [TestMethod]
        public void DeleteIngredient_InvalidIdInvalidName_ReturnInvalidRequest()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                bll.DeleteIngredient(null, null);
            });
        }
        #endregion

        #region DeleteRecipe
        [TestMethod]
        public void DeleteRecipe_ValidRecipe_DeleteRecipe()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipedId = 2;

            // act
            bll.DeleteRecipe(recipedId, null);

            // assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                HashSet<Recipe> recipes = bll.GetRecipesByNameOrId(recipedId, null);
            });
        }

        [TestMethod]
        public void DeleteRecipe_InValidRecipe_NotDeleteRecipe()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipedId = 200;

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                bll.DeleteRecipe(recipedId, null);
            });
        }


        [TestMethod]
        public void DeleteRecipe_InvalidIdInvalidName_ReturnInvalidRequest()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // act & assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                bll.DeleteIngredient(null, null);
            });
        }
        #endregion

        #region GetIngredientById
        [TestMethod]
        public void GetIngredientById_ValidId_GetIngredient()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 1;

            // Act
            Ingredient ingredient = bll.GetIngredientById(ingredientId);

            // Assert

            Assert.AreEqual(ingredientId, ingredient.Id);
        }
        [TestMethod]
        public void GetIngredientById_InValidId_GetIngredient()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 99;

            // Act
            Ingredient ingredient = bll.GetIngredientById(ingredientId);

            // Assert

            Assert.IsNull(ingredient);
        }
        #endregion

        #region GetIngredientByName
        [TestMethod]
        public void GetIngredientByName_ValidName_GetIngredient()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Eggs";

            // Act
            Ingredient ingredient = bll.GetIngredientByName(ingredientName);

            // Assert

            Assert.AreEqual(ingredientName, ingredient.Name);
        }

        [TestMethod]
        public void GetIngredientByName_InValidName_GetIngredient()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "abc";

            // Act
            Ingredient ingredient = bll.GetIngredientByName(ingredientName);

            // Assert

            Assert.IsNull(ingredient);
        }
        #endregion

        #region RecipeExists
        [TestMethod]
        public void RecipeExists_ValidName_ReturnTrue()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string recipeName = "Beef Tacos";

            // Act
            bool existRecipe = bll.RecipeExists(recipeName);

            // Assert

            Assert.IsTrue(existRecipe);
        }
        [TestMethod]
        public void RecipeExists_InValidName_ReturnFalse()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string recipeName = "abc";

            // Act
            bool existRecipe = bll.RecipeExists(recipeName);

            // Assert

            Assert.IsFalse(existRecipe);
        }
        #endregion
    }
}
