using Microsoft.Xna.Framework;

namespace Project1
{
    public class Collides : Game1
    {
        public static bool Collide(Ingredient ingredient)
        {
            var ingredientSize = new Point(ingredient.Texture.Width, ingredient.Texture.Height);

            Rectangle ingredientRect = new Rectangle((int)ingredient.Position.X,
                                                     (int)ingredient.Position.Y,
                                                     ingredientSize.X,
                                                     ingredientSize.Y);

            Rectangle cauldronRect = new Rectangle((int)cauldronPosition.X,
                                                   (int)cauldronPosition.Y,
                                                   cauldronSize.X,
                                                   cauldronSize.Y);

            return ingredientRect.Intersects(cauldronRect);
        }
    }
}
