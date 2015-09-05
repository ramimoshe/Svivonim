using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Svivonim.ObjectModel
{
    class Cube : DrawableGameComponent
    {

        private VertexBuffer m_Buffer;
        private BasicEffect m_Effect;

        protected const float k_MinZCoordinate = 1;
        protected const float k_MaxZCoordinate = -1;
        protected const float k_MinXCoordinate = -1;
        protected const float k_MaxXCoordinate = 1;
        protected const float k_MinYCoordinate = -1;
        protected const float k_MaxYCoordinate = 1;
        protected readonly Color r_UpDownColor = Color.BurlyWood;
        protected Vector3[] m_VerticesCoordinates = new Vector3[8];
        private VertexPositionColor[] m_Verts;
        private float m_RotationY;
        private Vector3 m_Position;

        public override void Initialize()
        {

            m_Effect = new BasicEffect(GraphicsDevice);
            m_Verts = new VertexPositionColor[8]
            {
                new VertexPositionColor(new  Vector3(k_MinXCoordinate, k_MinYCoordinate, k_MaxZCoordinate), Color.Red),
                new VertexPositionColor(new  Vector3(k_MinXCoordinate, k_MaxYCoordinate, k_MaxZCoordinate), Color.Red),
                new VertexPositionColor(new  Vector3(k_MaxXCoordinate, k_MaxYCoordinate, k_MaxZCoordinate), Color.Red),
                new VertexPositionColor(new  Vector3(k_MaxXCoordinate, k_MinYCoordinate, k_MaxZCoordinate), Color.Red),
                new VertexPositionColor(new  Vector3(k_MaxXCoordinate, k_MinYCoordinate, k_MinZCoordinate), Color.Red),
                new VertexPositionColor(new  Vector3(k_MaxXCoordinate, k_MaxYCoordinate, k_MinZCoordinate), Color.Red),
                new VertexPositionColor(new  Vector3(k_MinXCoordinate, k_MaxYCoordinate, k_MinZCoordinate), Color.Red),
                new VertexPositionColor(new  Vector3(k_MinXCoordinate, k_MinYCoordinate, k_MinZCoordinate), Color.Red)
            };

            var sd = new RasterizerState();
            sd.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = sd;
            m_Effect.VertexColorEnabled = true;


            m_Buffer = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, 8, BufferUsage.WriteOnly);
            m_Buffer.SetData(m_Verts);

            //            position = new Vector3(0, 0, 5);

            base.Initialize();
        }


        public Cube(Game i_Game, Vector3 i_Vec) : base(i_Game)
        {
            m_Position = i_Vec;
        }


        public override void Draw(GameTime i_GameTime)
        {
            m_Effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.01f, 1000f);
            m_Effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -5), Vector3.Forward, Vector3.Up);
            m_Effect.World = Matrix.Identity * Matrix.CreateRotationY(m_RotationY) * Matrix.CreateTranslation(m_Position);


            foreach (var pass in m_Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, m_Verts, 0, 6);
            }


            base.Draw(i_GameTime);
        }

        public override void Update(GameTime i_GameTime)
        {

            float deltaTime = (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            m_RotationY += deltaTime;


            base.Update(i_GameTime);
        }

    }
}
