using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace game
{
    public interface IGameplayModel
    {
        event EventHandler<GameplayEventArgs> Updated;

        void Update();
        void MovePlayer(Direction dir);

        public enum Direction : byte
        {
            forward,
            backward,
            right,
            left
        }
    }

    public class GameplayEventArgs : EventArgs
    {
        public Vector2 PlayerPos { get; set; }
    }

    public interface IGameplayView
    {
        event EventHandler CycleFinished;
        event EventHandler<ControlsEventArgs> PlayerMoved;

        void LoadGameCycleParameters(Vector2 pos);
        void Run();
    }

    public class ControlsEventArgs : EventArgs
    {
        public IGameplayModel.Direction Direction { get; set; }
    }

    public class GameplayPresenter
    {
        private IGameplayView _gameplayView = null;
        private IGameplayModel _gameplayModel = null;

        public GameplayPresenter(
          IGameplayView gameplayView,
          IGameplayModel gameplayModel
        )
        {
            _gameplayView = gameplayView;
            _gameplayModel = gameplayModel;

            _gameplayView.CycleFinished += ViewModelUpdate;
            _gameplayView.PlayerMoved += ViewModelMovePlayer;
            _gameplayModel.Updated += ModelViewUpdate;

        }

        private void ViewModelMovePlayer(object sender, ControlsEventArgs e)
        {
            _gameplayModel.MovePlayer(e.Direction);
        }

        private void ModelViewUpdate(object sender, GameplayEventArgs e)
        {
            _gameplayView.LoadGameCycleParameters(e.PlayerPos);
        }

        private void ViewModelUpdate(object sender, EventArgs e)
        {
            _gameplayModel.Update();
        }

        public void LaunchGame()
        {
            _gameplayView.Run();
        }
    }


    public class GameCycleView : Game, IGameplayView
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public event EventHandler CycleFinished = delegate { };
        public event EventHandler<ControlsEventArgs> PlayerMoved = delegate { };

        private Vector2 _playerPos = Vector2.Zero;
        private Texture2D _playerImage;

        public GameCycleView()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _playerImage = Content.Load<Texture2D>("Snape");
        }

        public void LoadGameCycleParameters(Vector2 pos)
        {
            _playerPos = pos;
        }

        protected override void Update(GameTime gameTime)
        {
            var keys = Keyboard.GetState().GetPressedKeys();
            if (keys.Length > 0)
            {
                var k = keys[0];
                switch (k)
                {
                    case Keys.W:
                        {
                            PlayerMoved.Invoke(
                              this,
                              new ControlsEventArgs
                              {
                                  Direction = IGameplayModel.Direction.forward
                              }
                            );
                            break;
                        }
                    case Keys.S:
                        {
                            PlayerMoved.Invoke(
                                this,
                                new ControlsEventArgs
                                {
                                    Direction = IGameplayModel.Direction.backward
                                }
                              );
                            break;
                        }
                    case Keys.D:
                        {
                            PlayerMoved.Invoke(
                                this,
                                new ControlsEventArgs
                                {
                                    Direction = IGameplayModel.Direction.right
                                }
                              );
                            break;
                        }
                    case Keys.A:
                        {
                            PlayerMoved.Invoke(
                                this,
                                new ControlsEventArgs
                                {
                                    Direction = IGameplayModel.Direction.left
                                }
                              );
                            break;
                        }
                    case Keys.Escape:
                        {
                            Exit();
                            break;
                        }
                }
            }
            base.Update(gameTime);
            CycleFinished.Invoke(this, new EventArgs());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerImage, _playerPos, Color.White);
            _spriteBatch.End();
        }
    }

    public class GameCycle : IGameplayModel
    {
        public event EventHandler<GameplayEventArgs> Updated = delegate { };

        private Vector2 _pos = new Vector2(300, 300);

        public void Update()
        {
            _pos += new Vector2(1, 0);
            Updated.Invoke(this, new GameplayEventArgs { PlayerPos = _pos });
        }

        public void MovePlayer(IGameplayModel.Direction dir)
        {
            switch (dir)
            {
                case IGameplayModel.Direction.forward:
                    {
                        _pos += new Vector2(0, -1);
                        break;
                    }
                case IGameplayModel.Direction.backward:
                    {
                        _pos += new Vector2(0, 1);
                        break;
                    }
                case IGameplayModel.Direction.right:
                    {
                        _pos += new Vector2(1, 0);
                        break;
                    }
                case IGameplayModel.Direction.left:
                    {
                        _pos += new Vector2(-1, 0);
                        break;
                    }
            }
        }
    }
}