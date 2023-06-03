using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Project1
{
    public class Ingredient : Game1
    {
        public static Texture2D ginsengTexture;
        public static Ingredient ginseng;
        public static Texture2D eyebrightTexture;
        public static Ingredient eyebright;
        public static Texture2D pixieWingsTexture;
        public static Ingredient pixieWings;

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public bool IsSelected = true;
        public Ingredient(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public static List<Ingredient> ingredients = new List<Ingredient>();

        public static void MakeIngrediens()
        {
            ginseng = new Ingredient(ginsengTexture, new Vector2(270, 325));
            ingredients.Add(ginseng);

            eyebright = new Ingredient(eyebrightTexture, new Vector2(319, 27));
            ingredients.Add(eyebright);

            pixieWings = new Ingredient(pixieWingsTexture, new Vector2(10, 210));
            ingredients.Add(pixieWings);
        }

        public static void MoveWithClick()
        {
            if (isDragging)
            {
                ingredients[0].Position += new Vector2(Mouse.GetState().X - prevMouseState.X, Mouse.GetState().Y - prevMouseState.Y);
            }
            else
            {
                foreach (Ingredient ingredient in ingredients)
                {
                    if (IsMouseOverTexture(ingredient))
                    {
                        if (ingredient.Texture == ginsengTexture)
                        {
                            isDragging = true;
                        }
                    }
                }
            }
        }

        public static bool IsMouseOverTexture(Ingredient ingredient)
        {
            var ingredientSize = new Point(ingredient.Texture.Width, ingredient.Texture.Height);
            Rectangle textureRect = new Rectangle((int)ingredient.Position.X, (int)ingredient.Position.Y, ingredientSize.X, ingredientSize.Y);
            return textureRect.Contains(Mouse.GetState().Position);
        }
    }
}
