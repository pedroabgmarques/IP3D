using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vertex
{
    class Piramide
    {

        private VertexPositionColor[] vertexes;

        public Piramide(int nLados)
        {
            vertexes = new VertexPositionColor[2 * nLados + 1];

            Vector3 topo = new Vector3(0, 1, 0);
            float angulo = 0;
            float step = MathHelper.ToRadians(360 / nLados);

            for (int i = 0; i < nLados; i++)
            {
                vertexes[2*i] = new VertexPositionColor(
                    new Vector3((float)Math.Cos(angulo), 0, (float)-Math.Sin(angulo))
                    , Color.Red);
                
                vertexes[2 * i + 1] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Black);
                angulo += step;
            }

            vertexes[2 * nLados] = vertexes[0];
            
        }

        public void Draw(Matrix World, Matrix View, Matrix Projection, GraphicsDevice graphics, BasicEffect efeito)
        {

            //World, View, Projection
            efeito.World = World;
            efeito.View = View;
            efeito.Projection = Projection;

            foreach (EffectPass pass in efeito.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertexes, 0, vertexes.Length - 2);
            }
        }

    }
}
