using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static Texture2D cauldronTexture;
        public static Vector2 cauldronPosition;
        public static Point cauldronSize;
        Texture2D shelfTexture;
        
        public static bool isDragging;
        public static MouseState prevMouseState;
        public static List<Vector2> positions;
        public static float scaleFactor = 0.5f;

        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            cauldronPosition = new Vector2(500, 100);
            positions = new List<Vector2>();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            cauldronTexture = Content.Load<Texture2D>("cauldron");
            cauldronSize = new Point(cauldronTexture.Width, cauldronTexture.Height);
            shelfTexture = Content.Load<Texture2D>("shelfs");
            Ingredient.ginsengTexture = Content.Load<Texture2D>("ginseng");
            Ingredient.eyebrightTexture = Content.Load<Texture2D>("eyebright");
            Ingredient.pixieWingsTexture = Content.Load<Texture2D>("pixieWings");
            Ingredient.MakeIngrediens();
        }        

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Ingredient.MoveWithClick();
            }
            else
            {
                isDragging = false;
            }
            prevMouseState = Mouse.GetState();

            if (Collides.Collide(Ingredient.ginseng))
            {
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    Clones.MakeClones();
                }
            }
                
            base.Update(gameTime);
        }       

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            _spriteBatch.Begin();            
            _spriteBatch.Draw(shelfTexture, Vector2.Zero, Color.White);
            _spriteBatch.Draw(cauldronTexture, cauldronPosition, Color.White);
            foreach (Ingredient ingredient in Ingredient.ingredients)
            {
            _spriteBatch.Draw(ingredient.Texture, ingredient.Position, Color.White);
            }
            _spriteBatch.Draw(cauldronTexture, cauldronPosition, Color.White);
            Clones.Draw();
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }   
}