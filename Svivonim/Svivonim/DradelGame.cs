using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Svivonim.ObjectModel;

namespace Svivonim
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DradelGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private BasicEffect effect;
        private RasterizerState m_RasterizerState = new RasterizerState();

        private SimpleTriangle a;
        private Box b;

        #region Camera Initialization

        // the data about the camera location/rotation/features:
        private Matrix m_CameraSettings;
        private Matrix m_CameraState;

        private void setCameraSettings()
        {
            float k_NearPlaneDistance = 0.5f;
            float k_FarPlaneDistance = 1000.0f;
            float k_ViewAngle = MathHelper.PiOver4;

            // we are storing the camera settings data in a matrix:
            m_CameraSettings = Matrix.CreatePerspectiveFieldOfView(
                k_ViewAngle,
                GraphicsDevice.Viewport.AspectRatio,
                k_NearPlaneDistance,
                k_FarPlaneDistance);
        }

        // we want to look at the center of the 3D world:
        Vector3 m_CameraLooksAt = Vector3.Zero;
        // we are standing 80 units in front of our target:
        Vector3 m_CameraLocation = new Vector3(1, 3, 10);
        // the camera stands straight:
        Vector3 m_CameraUpDirection = Vector3.Up;

        private void setCameraState()
        {
            // we are storing the camera state data in a matrix:
            m_CameraState = Matrix.CreateLookAt(
                m_CameraLocation, m_CameraLooksAt, m_CameraUpDirection);
        }
        #endregion Camera Initialization

        public DradelGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            setCameraSettings();
            setCameraState();

            effect = new BasicEffect(GraphicsDevice);
            Services.AddService(typeof(BasicEffect), effect);

            a = new SimpleTriangle(this, new Vector3(5, 0, 5));
            //b = new SimpleTriangle(this, new Vector3(-5, 0, 5));
            b = new Box(this);

            Components.Add(a);
            Components.Add(b);

            m_RasterizerState.CullMode = CullMode.None;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            effect.View = m_CameraState;
            effect.Projection = m_CameraSettings;
            effect.GraphicsDevice.RasterizerState = m_RasterizerState;

            base.Draw(gameTime);
        }
    }
}
