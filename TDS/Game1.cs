using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TDS
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private NetworkConnection _networkConnection;
        private Color _color;
        private SpriteFont basicFont;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _networkConnection = new NetworkConnection();
            _color = Color.Red;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _networkConnection.Start();
            if (_networkConnection.Start())
            {
                _color = Color.Green;
            }
            else
            {
                _color = Color.Red;
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Globals.content = this.Content;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            basicFont = Content.Load<SpriteFont>("File");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_color);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawString(basicFont, _networkConnection.Start().ToString(), new Vector2(100, 100), Color.Green);
            
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }


}
