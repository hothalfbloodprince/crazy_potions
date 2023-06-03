using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class Clones : Game1
    {
        public static void MakeClones()
        {
            Vector2 clickPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            for (int i = 0; i < 10; i++)
            {
                Vector2 position = clickPosition + new Vector2(i * 10, i * 10);
                positions.Add(position);
            }
            scaleFactor *= 0.99f;
        }

        public static void Draw()
        {
            foreach (Vector2 position in positions)
            {
                Vector2 origin = new Vector2(Ingredient.ginsengTexture.Width / 2, Ingredient.ginsengTexture.Height / 2);
                Vector2 scale = new Vector2(scaleFactor, scaleFactor);

                _spriteBatch.Draw(Ingredient.ginsengTexture, position, null, Color.White, 0f, origin, scale,
                    SpriteEffects.None, 0f);
            }
        }
    }
}
