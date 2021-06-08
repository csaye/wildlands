using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Wildlands.Items;
using Wildlands.Objects;
using Wildlands.SaveLoad;
using Wildlands.Tiles;
using Wildlands.UI;

namespace Wildlands
{
    public class Game1 : Game
    {
        private const int Grid = Drawing.Grid;

        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public SaveData SaveData { get; set; }

        public Camera Camera { get; private set; } = new Camera();
        public Inventory Inventory { get; private set; } = new Inventory();
        public TileManager TileManager { get; private set; } = new TileManager();
        public UIManager UIManager { get; private set; } = new UIManager();
        public ObjectManager ObjectManager { get; private set; } = new ObjectManager();
        public Player Player { get; private set; } = new Player(0, 0, Grid, Grid);

        private KeyboardState keyboardState;
        private KeyboardState lastKeyboardState;

        private MouseState mouseState;
        private MouseState lastMouseState;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Set up window
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnScreenSizeChange;

            // Initialize objects
            ObjectManager.AddDynamicObject(Player);

            // Load save data
            SaveLoadManager.Load(this);
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
            ProcessKeyboardState();

            // Get and process mouse state
            ProcessMouseState();

            // If not paused
            if (!UIManager.MenuOpen)
            {
                ObjectManager.Update(this, delta); // Update objects
                Camera.Update(this); // Update camera
            }
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

        public void OnSave()
        {
            TileManager.OnSave(this); // Save tiles
            ObjectManager.OnSave(this); // Save objects
            Inventory.OnSave(this); // Save inventory
        }

        public void OnLoad()
        {
            TileManager.OnLoad(this); // Load tiles
            ObjectManager.OnLoad(this); // Load objects
            Inventory.OnLoad(this); // Load inventory
        }

        // Returns whether given key is down
        public bool IsKeyDown(Keys key) => keyboardState.IsKeyDown(key);
        // Returns whether given key has been pressed in the last frame
        public bool IsKeyPressed(Keys key) => keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);

        // Processes current keyboard state
        private void ProcessKeyboardState()
        {
            // Get keyboard state
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            if (IsKeyDown(Keys.Escape)) Exit(); // Quit game
            if (IsKeyPressed(Keys.F)) Graphics.ToggleFullScreen(); // Toggle fullscreen
            if (IsKeyPressed(Keys.X)) SaveLoadManager.Save(this); // Save

            if (IsKeyPressed(Keys.M)) Inventory.AddItem(Item.Flower, 1);
        }

        // Processes current mouse state
        private void ProcessMouseState()
        {
            // Get mouse state
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            // Whether left or right click has been pressed in last frame
            bool leftClick = mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;
            bool rightClick = mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released;

            // Process left click
            if (leftClick)
            {
                // If menu open, click UI
                if (UIManager.MenuOpen) UIManager.OnLeftClick(this, mouseState.Position);
                // If menu closed, click objects
                else ObjectManager.OnLeftClick(this, mouseState.Position);
            }

            // Process right click
            if (rightClick)
            {
                // If menu open, click UI
                if (UIManager.MenuOpen) UIManager.OnRightClick(this, mouseState.Position);
                // If menu closed, click objects
                else ObjectManager.OnRightClick(this, mouseState.Position);
            }
        }

        // Called when user resizes window
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
