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

        public Camera Camera { get; private set; }
        public ObjectManager ObjectManager { get; private set; }
        public Player Player { get; private set; }

        public KeyboardState KeyboardState { get; private set; }

        private const int Grid = Drawing.Grid;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Initialize objects
            Camera = new Camera();
            ObjectManager = new ObjectManager();
            Player = new Player(0, 0, Grid, Grid);
            ObjectManager.AddObject(Player);
            ObjectManager.AddObject(new Rock(Grid * 4, Grid * 4, Grid, Grid));
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
            Camera.Update(this); // Update camera

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin(transformMatrix: Camera.Transform); // Begin world sprite batch
            ObjectManager.Draw(this); // Draw objects
            SpriteBatch.End(); // End world sprite batch

            base.Draw(gameTime);
        }
    }
}
