using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EcsAlhpaPrototype
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 position;

        private readonly int W_WIDTH = 1024;
        private readonly int W_HEIGHT = 768;

        private Texture2D background_grass;
        private Texture2D foreground_sky;
        private Texture2D player;

        private SpriteFont font;
        private int display_int = 0;

        KeyboardState previousState;

        bool isValid = true;


        public Game()
        {

            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = W_WIDTH,     // Window Width-Size
                PreferredBackBufferHeight = W_HEIGHT    // Window Height-Size
            };
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            position = new Vector2(W_WIDTH/4, W_HEIGHT/4);

            previousState = Keyboard.GetState();
        }

        /// LoadContent called once and is the place to load all of your content.
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background_grass = Content.Load<Texture2D>("Sprites/grass");  // if you are using your own images.
            foreground_sky = Content.Load<Texture2D>("Sprites/sky");
            player = this.Content.Load<Texture2D>("Sprites/player");

            font = Content.Load<SpriteFont>("Fonts/Message");
        }

        /// UnloadContent will be called once per game and is the place to unload game-specific content.
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// Allows the game to run logic such as updating the world, checking for collisions, gathering input, and playing audio.
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            // "gameTime" gives timing values

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
                Exit();

            if (state.IsKeyDown(Keys.Enter))
            {
                isValid = false;
                display_int = 1;
            } else
            {
                isValid = true;
                display_int = 0;
            }

            if (state.IsKeyDown(Keys.Right) & !previousState.IsKeyDown(
                Keys.Right))
                position.X += 10;
            if (state.IsKeyDown(Keys.Left) & !previousState.IsKeyDown(
                Keys.Left))
                position.X -= 10;
            if (state.IsKeyDown(Keys.Up))
                position.Y -= 10;
            if (state.IsKeyDown(Keys.Down))
                position.Y += 10;

            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Red);
            spriteBatch.Begin();

            spriteBatch.Draw(foreground_sky, new Rectangle(0, 0, W_WIDTH, W_HEIGHT), Color.White);
            if (isValid)    //For Testing, Press/Hold Enter to activate
            {
                spriteBatch.Draw(background_grass, new Rectangle(0, 0, W_WIDTH, W_HEIGHT), Color.White);
            }
            spriteBatch.Draw(player, position, Color.White);
            spriteBatch.DrawString(font, display_int.ToString(), new Vector2(100, 100), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
