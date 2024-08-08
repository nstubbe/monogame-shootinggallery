using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingGallery
{
    public class Game1 : Game
    {
        private Texture2D _targetSprite;
        private Texture2D _crosshairsSprite;
        private Texture2D _backgroundSprite;
        private SpriteFont _gameFont;

        private MouseState _mouseState;
        private bool _mouseButtonReleased = true;

        private Target _target;

        private int _score = 0;
        private double _timeLeftInSeconds = 10;

        public Game1()
        {
            Global.Init(new GraphicsDeviceManager(this));
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            _target = new Target();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Global.Spritebatch = new SpriteBatch(GraphicsDevice);

            _targetSprite = Content.Load<Texture2D>("target");
            _backgroundSprite = Content.Load<Texture2D>("sky");
            _crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            _gameFont = Content.Load<SpriteFont>("galleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_timeLeftInSeconds > 0)
                _timeLeftInSeconds -= gameTime.ElapsedGameTime.TotalSeconds;

            _mouseState = Mouse.GetState();

            if (_mouseState.LeftButton == ButtonState.Pressed && _mouseButtonReleased)
            {
                var mouseTargetDistance = Vector2.Distance(_mouseState.Position.ToVector2(), _target.Position);
                if (mouseTargetDistance <= Target.Radius && _timeLeftInSeconds > 0)
                {
                    _score++;
                    _target.RandomizeLocation();
                }

                _mouseButtonReleased = false;
            }

            if (_mouseState.LeftButton == ButtonState.Released)
                _mouseButtonReleased = true;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Global.Spritebatch.Begin();

            Global.Spritebatch.Draw(_backgroundSprite, new Vector2(0, 0), Color.White);

            if (_timeLeftInSeconds > 0)
            {
                Global.Spritebatch.DrawString(_gameFont, $"Score: {_score}", new Vector2(10, 10), Color.White);
                Global.Spritebatch.DrawString(_gameFont, $"Time left: {Math.Abs(Math.Ceiling(_timeLeftInSeconds))}s",
                    new Vector2(10, 40), Color.White);
                Global.Spritebatch.Draw(_targetSprite,
                    new Vector2(_target.Position.X - Target.Radius, _target.Position.Y - Target.Radius), Color.White);
            }
            else
            {
                var centerPosition = new Vector2(Global.Graphics.PreferredBackBufferWidth / 3f,
                    Global.Graphics.PreferredBackBufferHeight / 3f);
                Global.Spritebatch.DrawString(_gameFont, $"You scored {_score}!", centerPosition, Color.White);
            }


            Global.Spritebatch.Draw(_crosshairsSprite,
                new Vector2(_mouseState.X - _crosshairsSprite.Width / 2f,
                    _mouseState.Y - _crosshairsSprite.Height / 2f), Color.White);

            Global.Spritebatch.End();

            base.Draw(gameTime);
        }
    }
}