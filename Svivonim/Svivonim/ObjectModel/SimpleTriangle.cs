using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Svivonim.ObjectModel
{
    class SimpleTriangle : DrawableGameComponent
    {


        private VertexPositionColor[] m_Verts;
//        private BasicEffect effect;
        private VertexBuffer m_Buffer;
        private BasicEffect m_Effect;
        private Vector3 m_Position;
        private float m_RotationY;


        public SimpleTriangle(Game i_Game, Vector3 i_Position)
            : base(i_Game)
        {
            this.m_Position = i_Position;
        }


        public override void Initialize()
        {

            m_Effect = new BasicEffect(GraphicsDevice);
            m_Verts = new VertexPositionColor[7]
            {
                new VertexPositionColor(new Vector3(0.0f, 1.0f, 0.0f), Color.Red),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 0.0f), Color.Red),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 0.0f), Color.Red),
                new VertexPositionColor(new Vector3(2.0f, -2.0f, 0.0f), Color.Black),
                new VertexPositionColor(new Vector3(4.0f, -5.0f, 0.0f), Color.Black),
                new VertexPositionColor(new Vector3(3.0f, -4.0f, 0.0f), Color.Black),
                new VertexPositionColor(new Vector3(1.0f, -8.0f, 0.0f), Color.Black)
            };

            var sd = new RasterizerState();
            sd.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = sd;
            m_Effect.VertexColorEnabled = true;
            

            m_Buffer = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, 7, BufferUsage.WriteOnly);
            m_Buffer.SetData(m_Verts);

//            position = new Vector3(0, 0, 5);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime i_GameTime)
        {
            m_Effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.01f, 1000f);
            m_Effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -5), Vector3.Forward, Vector3.Up);
            m_Effect.World = Matrix.Identity * Matrix.CreateRotationY(m_RotationY) * Matrix.CreateTranslation(m_Position);


            foreach (var pass in m_Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, m_Verts, 0, 5);
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
