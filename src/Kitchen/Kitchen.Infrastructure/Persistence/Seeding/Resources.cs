using Kitchen.Domain.Entities;
using Kitchen.Domain.Entities.Enums.Ingredient;
using Kitchen.Domain.Entities.Enums.RecipeIngredient;

namespace Kitchen.Infrastructure.Persistence.Seeding;

public static class Resources
{
    private static readonly Ingredient[] SeedIngredients =
    [
        Ingredient.Create(
            "Canola Oil", 
            IngredientCategory.Fat,
            true,
            300,
            UnitOfMeasurement.Gallon),
        Ingredient.Create(
            "Extra-Virgin Olive Oil", 
            IngredientCategory.Fat,
            true,
            600,
            UnitOfMeasurement.Gallon),
        Ingredient.Create(
            "Macadamia Nut Oil", 
            IngredientCategory.Fat,
            true,
            300,
            UnitOfMeasurement.Gallon),
        Ingredient.Create(
            "Coconut Oil", 
            IngredientCategory.Fat,
            true,
            150,
            UnitOfMeasurement.Gallon),
        Ingredient.Create(
            "Butter", 
            IngredientCategory.Fat,
            true,
            500,
            UnitOfMeasurement.Pound),
        Ingredient.Create(
            "Lard", 
            IngredientCategory.Fat,
            true,
            500,
            UnitOfMeasurement.Pound),
        Ingredient.Create(
            "Ketchup", 
            IngredientCategory.Condiment,
            true,
            600,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Dijon Mustard", 
            IngredientCategory.Condiment,
            true,
            600,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Yellow Mustard", 
            IngredientCategory.Condiment,
            true,
            600,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Soy Sauce", 
            IngredientCategory.Condiment,
            true,
            100,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Chili Paste", 
            IngredientCategory.Condiment,
            true,
            50,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Hot Sauce", 
            IngredientCategory.Condiment,
            true,
            500,
            UnitOfMeasurement.Gallon),
        Ingredient.Create(
            "Mayonnaise", 
            IngredientCategory.Condiment,
            true,
            100,
            UnitOfMeasurement.Gallon),
        Ingredient.Create(
            "Kosher Salt", 
            IngredientCategory.Seasoning,
            true,
            150,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Sea Salt", 
            IngredientCategory.Seasoning,
            true,
            150,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Himalayan pink salt ", 
            IngredientCategory.Seasoning,
            true,
            75,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Black Peppercorns", 
            IngredientCategory.Seasoning,
            true,
            175,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Cayenne Pepper", 
            IngredientCategory.Seasoning,
            true,
            175,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Crushed Red Pepper", 
            IngredientCategory.Seasoning,
            true,
            100,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Paprika", 
            IngredientCategory.Seasoning,
            true,
            25,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Cinnamon", 
            IngredientCategory.Seasoning,
            true,
            60,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Cloves", 
            IngredientCategory.Seasoning,
            true,
            30,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Allspice", 
            IngredientCategory.Seasoning,
            true,
            200,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Nutmeg", 
            IngredientCategory.Seasoning,
            true,
            60,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Chili Powder", 
            IngredientCategory.Seasoning,
            true,
            32,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Curry Powder", 
            IngredientCategory.Seasoning,
            true,
            160,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Italian Seasoning", 
            IngredientCategory.Seasoning,
            true,
            160,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Vanilla Extract", 
            IngredientCategory.Seasoning,
            true,
            100,
            UnitOfMeasurement.FluidOunce),
        Ingredient.Create(
            "Garam Masala", 
            IngredientCategory.Aromatic,
            true,
            500,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Tandoori Masala", 
            IngredientCategory.Aromatic,
            true,
            500,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Chai Masala", 
            IngredientCategory.Aromatic,
            true,
            500,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Sambhar Masala", 
            IngredientCategory.Aromatic,
            true,
            300,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Biryani Masala", 
            IngredientCategory.Aromatic,
            true,
            250,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Ras el Hanout", 
            IngredientCategory.Aromatic,
            true,
            300,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Chinese Five Spice", 
            IngredientCategory.Aromatic,
            true,
            225,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Za’atar", 
            IngredientCategory.Aromatic,
            true,
            100,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Baharat", 
            IngredientCategory.Aromatic,
            true,
            150,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Herbes de Provence", 
            IngredientCategory.Aromatic,
            true,
            130,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Ginger", 
            IngredientCategory.Aromatic,
            true,
            300,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Rosemary", 
            IngredientCategory.Aromatic,
            true,
            300,
            UnitOfMeasurement.Sprig),
        Ingredient.Create(
            "Thyme Leaves", 
            IngredientCategory.Aromatic,
            true,
            400,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Coriander", 
            IngredientCategory.Aromatic,
            true,
            500,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Tarragon", 
            IngredientCategory.Aromatic,
            true,
            500,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Fennel", 
            IngredientCategory.Aromatic,
            true,
            300,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Sage", 
            IngredientCategory.Aromatic,
            true,
            330,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Green Mint", 
            IngredientCategory.Aromatic,
            true,
            500,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Peppery Mint", 
            IngredientCategory.Aromatic,
            true,
            600,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Bay Leaves", 
            IngredientCategory.Aromatic,
            true,
            220,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Cumin", 
            IngredientCategory.Aromatic,
            true,
            200,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Dill", 
            IngredientCategory.Aromatic,
            true,
            250,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Basil", 
            IngredientCategory.Aromatic,
            true,
            260,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Chervil", 
            IngredientCategory.Aromatic,
            true,
            360,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Chives", 
            IngredientCategory.Aromatic,
            true,
            500,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Oregano", 
            IngredientCategory.Aromatic,
            true,
            600,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Parsley", 
            IngredientCategory.Aromatic,
            true,
            300,
            UnitOfMeasurement.Ounce),
        Ingredient.Create(
            "Cheddar", 
            IngredientCategory.Cheese),
        Ingredient.Create(
            "Feta", 
            IngredientCategory.Cheese),
        Ingredient.Create(
            "Parmesan", 
            IngredientCategory.Cheese),
        Ingredient.Create(
            "Mozzarella", 
            IngredientCategory.Cheese),
        Ingredient.Create(
            "Swiss", 
            IngredientCategory.Cheese),
        Ingredient.Create(
            "American", 
            IngredientCategory.Cheese),
        Ingredient.Create(
            "Cream Cheese", 
            IngredientCategory.Cheese),
        Ingredient.Create(
            "Skim Milk", 
            IngredientCategory.Dairy),
        Ingredient.Create(
            "2% Milk", 
            IngredientCategory.Dairy),
        Ingredient.Create(
            "1% Milk", 
            IngredientCategory.Dairy),
        Ingredient.Create(
            "Milk", 
            IngredientCategory.Dairy),
        Ingredient.Create(
            "Greek Yogurt - Plain", 
            IngredientCategory.Dairy),
        Ingredient.Create(
            "Yogurt - Plain", 
            IngredientCategory.Dairy),
        Ingredient.Create(
            "Balsamic Vinegar", 
            IngredientCategory.Liquid),
        Ingredient.Create(
            "Cider Vinegar", 
            IngredientCategory.Liquid),
        Ingredient.Create(
            "Red Wine Vinegar", 
            IngredientCategory.Liquid),
        Ingredient.Create(
            "Rice Vinegar ", 
            IngredientCategory.Liquid),
        Ingredient.Create(
            "White Vinegar", 
            IngredientCategory.Liquid),
        Ingredient.Create(
            "Baking Soda", 
            IngredientCategory.Leavener),
        Ingredient.Create(
            "Backing Powder", 
            IngredientCategory.Leavener),
        Ingredient.Create(
            "Yeast", 
            IngredientCategory.Leavener),
        Ingredient.Create(
            "All-Purpose", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Self-Rising", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Bread", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Cake", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Pastry", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Type 00", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Strong", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "High-Protein", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Whole Wheat", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "White Whole Wheat", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Semolina", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Rye", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Barley", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Corn Meal", 
            IngredientCategory.Flour),
        Ingredient.Create(
            "Broccoli", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Cabbage", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Carrot", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Shallot", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Garlic", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Yellow Onion", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "White Onion", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Red Onion", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Green Onion", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Kale", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Arugula", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Bok Choy", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Spinach", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Collard Greens", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Cabbage", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Romaine Lettuce", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Watercress", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Sorrel", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Swiss Chard", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Endive", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Escarole", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Mustard Greens", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Turnip Greens", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Beet", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Radish", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Broccoli", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Broccoli Rabe", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Kohlrabi", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Dandelion", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Green Beans", 
            IngredientCategory.Vegetables),
        Ingredient.Create(
            "Dragon Fruit", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Passion Fruit", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Rambutan", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Acai", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Jackfruit", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Mangosteen", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Lychee", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Papaya", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Guava", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Tomato", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Avocado", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Bergamot Orange", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Blood Orange", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Buddha's Hand Citron", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Cara Cara Navel Orange", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Citron", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Clementine", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Finger Lime", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Mandarin Orange", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Grapefruit", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Kabosu", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Key Lime", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Kumquat", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Lemon", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Lime", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Limequat", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Makrut Lime", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Mandarinquat", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Mango Orange", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Meyer Lemon", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Navel Oranges", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Pomelo", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Ponderosa Lemon", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Satsuma", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Tangelo", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Tangerine", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Valencia", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Verigated Pink Lemon ", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Volkamer Lemon", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Yuzu", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Granny Smith Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Braeburn Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Honeycrisp Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Pink Lady Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Cortland Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Empire Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Gala Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "McIntosh Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Fuji Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Red Delicious Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Golden Delicious Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Ambrosia Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Sweet Tango Apple", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Nectarine", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Mango", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Gooseberry", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Cranberry", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Coconut", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Gros Michel Banana", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Cavendish Banana", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Nam Wah Banana", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Mysore Banana", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Pisang Raja Banana ", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Lady Finger Banana", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Señorita Banana", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Red Banana", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Blue Java Banana", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Manzano Banana ",
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Plantain ", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Cotton Candy Grapes", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Moon Drop Grapes", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Holiday Red Grapes", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Gum Drop Grapes", 
            IngredientCategory.Fruit),
        Ingredient.Create(
            "Ribeye", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Tenderloin", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Strip Loin", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Flank", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Ground Sirloin", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Pork Loin", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Pork Tenderloin", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Pork Belly", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Pork Chops", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Pork Shoulder", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Lamb Chops", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Rack Of Lamb", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Lamb Shank", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Veal Chops", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Veal Cutlet", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Rack Of Venison", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Ground Bison", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Ground Boar", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Chicken Breast", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Chicken Thigh", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Chicken Drumstick", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Chicken Wing", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Turkey Breast", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Turkey Thigh", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Turkey Drumstick", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Turkey Wing", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Duck Breast", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Cod", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Haddock", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Halibut", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Salmon", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Tuna", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Mackere", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Flounder", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Sole", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Maine Lobster", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Jumbo Lump Blue Crab", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Alaskan Red King Crab", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Florida Stone Crab", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Dungeness Crab", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Gulf Shimp", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Giant Prawns", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Crawfish", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Gulf Oysters", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Clam", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Mussels", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Bacon", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Pancetta", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Chorizo", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Italian Sausage", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Prosciutto", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Salami", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Smoked Ham", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Smoked Turkey", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Roast Beef", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Tofu", 
            IngredientCategory.Protein),
        Ingredient.Create(
            "Chickpea", 
            IngredientCategory.Legumes),
        Ingredient.Create(
            "Lentils", 
            IngredientCategory.Legumes),
        Ingredient.Create(
            "Black Beans", 
            IngredientCategory.Legumes),
        Ingredient.Create(
            "Red Beams", 
            IngredientCategory.Legumes),
    ];
}