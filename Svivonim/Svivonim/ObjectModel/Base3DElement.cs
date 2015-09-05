using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Svivonim.ObjectModel
{
    abstract class Base3DElement : DrawableGameComponent
    {

        protected Vector3 m_Position = Vector3.Zero;
        protected Vector3 m_Rotations = Vector3.Zero;
        protected Vector3 m_Scales = Vector3.One;
        protected Matrix m_WorldMatrix = Matrix.Identity;
        public bool SpinEnabled { get; set; }

        private bool m_HasOwnEffect;

        private BasicEffect m_Effect;
        public BasicEffect Effect
        {
            get { return m_Effect; }
            set
            {
                m_Effect = value;
                m_HasOwnEffect = true;
            } 
        }

        private float m_RotationsPerSecond;
        public float RotationsPerSecond
        {
            get { return m_RotationsPerSecond; }
            set { m_RotationsPerSecond = value; }
        }

        protected Base3DElement(Game i_Game) : base(i_Game)
        {
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (SpinEnabled)
            {
                m_Rotations.Y += (float)i_GameTime.ElapsedGameTime.TotalSeconds * m_RotationsPerSecond;
            }

            m_WorldMatrix =
                Matrix.Identity *
                Matrix.CreateScale(m_Scales) *
                Matrix.CreateRotationX(m_Rotations.X) *
                Matrix.CreateRotationY(m_Rotations.Y) *
                Matrix.CreateRotationZ(m_Rotations.Z) *
                Matrix.CreateTranslation(m_Position);
        }


        protected abstract void DoDraw(GameTime i_GameTime);

        public override void Draw(GameTime i_GameTime)
        {

            if (!m_HasOwnEffect)
            {
                m_Effect = Game.Services.GetService(typeof(BasicEffect)) as BasicEffect;
            }

            Effect.World = m_WorldMatrix;

            foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                DoDraw(i_GameTime);
            }
            
            base.Draw(i_GameTime);
        }
    }
}
