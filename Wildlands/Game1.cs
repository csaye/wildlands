using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Wildlands.Objects;

namespace Wildlands
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public ObjectManager ObjectManager { get; private set; }
        public Player Player { get; private set; }

        public KeyboardState KeyboardState { get; private set; }

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Initialize objects
            ObjectManager = new ObjectManager();
            Player = new Player(0, 0, Drawing.Grid, Drawing.Grid);
            ObjectManager.AddObject(Player);
        }

        protected override void Initialize()
        {
            // Initialize graphics
            Drawing.InitializeGraphics(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // Get time delta
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Get and process keyboard state
            KeyboardState = Keyboard.GetState();
            if (KeyboardState.IsKeyDown(Keys.Escape)) Exit();

            ObjectManager.Update(this, delta); // Update objects

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin(); // Begin sprite batch
            ObjectManager.Draw(this); // Draw obejcts
            SpriteBatch.End(); // End sprite batch

            base.Draw(gameTime);
        }
    }
}
