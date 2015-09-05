using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Svivonim.ObjectModel
{
    class Box : Base3DElement
    {
        public Box(Game i_Game)
            : base(i_Game)
        {
        }


        protected override void LoadContent()
        {
            base.LoadContent();
            VertexPositionColor[] vertices = new VertexPositionColor[8];

            // vertex position and color information for icosahedron
            vertices[0] = new VertexPositionColor(new Vector3(1, 1, 1), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(1, 1, -1), Color.Orange);
            vertices[2] = new VertexPositionColor(new Vector3(1, -1, 1), Color.Yellow);
            vertices[3] = new VertexPositionColor(new Vector3(1, -1, -1), Color.Green);
            vertices[4] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.Blue);
            vertices[5] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Indigo);
            vertices[6] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.Purple);
            vertices[7] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.White);

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 8, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertices);


            short[] indices = new short[36];
            //top 
            indices[0] = 1; indices[1] = 0; indices[2] = 5;
            indices[3] = 0; indices[4] = 5; indices[5] = 4;

            //left
            indices[6] = 1; indices[7] = 2; indices[8] = 3;
            indices[9] = 1; indices[10] = 2; indices[11] = 0;

            //bottom
            indices[12] = 3; indices[13] = 2; indices[14] = 7;
            indices[15] = 7; indices[16] = 2; indices[17] = 6;
            
            //right
            indices[18] = 7; indices[19] = 5; indices[20] = 6;
            indices[21] = 6; indices[22] = 5; indices[23] = 4;
            
            //front
            indices[24] = 3; indices[25] = 2; indices[26] = 7;
            indices[27] = 7; indices[28] = 1; indices[29] = 5;
            
            //back
            indices[30] = 0; indices[31] = 4; indices[32] = 6;
            indices[33] = 6; indices[34] = 0; indices[35] = 2;
            //            indices[36] = 5; indices[37] = 8; indices[38] = 3;
            //            indices[39] = 5; indices[40] = 9; indices[41] = 4;
            //            indices[42] = 6; indices[43] = 10; indices[44] = 1;
            //            indices[45] = 6; indices[46] = 11; indices[47] = 7;
            //            indices[48] = 7; indices[49] = 10; indices[50] = 6;
            //            indices[51] = 7; indices[52] = 11; indices[53] = 2;
            //            indices[54] = 8; indices[55] = 10; indices[56] = 3;
            //            indices[57] = 9; indices[58] = 11; indices[59] = 0;

            indexBuffer = new IndexBuffer(Game.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);


        }

        public IndexBuffer indexBuffer { get; set; }

        public VertexBuffer vertexBuffer { get; set; }

        protected override void DoDraw(GameTime i_GameTime)
        {
            Effect.VertexColorEnabled = true;
            Effect.View = Matrix.CreateLookAt(new Vector3(-50, -20, -20), Vector3.Forward, Vector3.Up);

            GraphicsDevice.SetVertexBuffer(vertexBuffer);
            GraphicsDevice.Indices = indexBuffer;
            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 8, 0, 12);

            //            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, m_Verts, 0, 5);
        }
    }
}
