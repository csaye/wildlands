using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Wildlands.Objects;
using Wildlands.Tiles;
using Wildlands.UI;

namespace Wildlands
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public Camera Camera { get; private set; }
        public TileManager TileManager { get; private set; }
        public UIManager UIManager { get; private set; }
        public ObjectManager ObjectManager { get; private set; }
        public Player Player { get; private set; }

        private KeyboardState keyboardState;
        private KeyboardState lastKeyboardState;

        private const int Grid = Drawing.Grid;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Set up window
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnScreenSizeChange;

            // Initialize classes
            Camera = new Camera();
            TileManager = new TileManager();
            UIManager = new UIManager();
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
            // Load content
            Drawing.LoadContent(this);

            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // Get time delta
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Get and process keyboard state
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            if (IsKeyDown(Keys.Escape)) Exit();
            if (IsKeyPressed(Keys.F)) Graphics.ToggleFullScreen();

            ObjectManager.Update(this, delta); // Update objects
            Camera.Update(this); // Update camera
            UIManager.Update(this); // Update UI

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform); // Begin world sprite batch
            TileManager.Draw(this); // Draw tiles
            ObjectManager.Draw(this); // Draw objects
            SpriteBatch.End(); // End world sprite batch

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp); // Begin UI sprite batch
            UIManager.Draw(this); // Draw UI
            SpriteBatch.End(); // End UI sprite batch

            base.Draw(gameTime);
        }

        // Returns whether given key is down
        public bool IsKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        // Returns whether given key has been pressed in the last frame
        public bool IsKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key);
        }

        private void OnScreenSizeChange(object sender, EventArgs e)
        {
            // Get new window bounds
            Rectangle newBounds = Window.ClientBounds;

            // Update drawing values
            Drawing.SetScreenWidth(newBounds.Width);
            Drawing.SetScreenHeight(newBounds.Height);

            // Update UI
            UIManager.UpdatePositioning();
        }
    }
}
