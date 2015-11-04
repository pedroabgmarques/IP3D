using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPC1_10855
{
    /// <summary>
    /// Desenha um polígono com n lados e um vértice de topo
    /// </summary>
    class Poligono
    {
        //Array de vértices
        private VertexPositionColor[] vertexes;

        public Poligono(GraphicsDevice graphics, int nLados)
        {

            vertexes = new VertexPositionColor[2*nLados+1];

            Vector3 topo = new Vector3(0, 1, 0);

            float angulo = 0;
            float passo = MathHelper.ToRadians(360 / nLados);

            for (int i = 0; i < nLados; i++)
            {
                vertexes[2*i] = new VertexPositionColor(new Vector3((float)Math.Cos(angulo),0,(float)-Math.Sin(angulo)), Color.Red);
                vertexes[2*i+1] = new VertexPositionColor(topo, Color.Red);
                angulo += passo;
            }

            vertexes[2 * nLados] = vertexes[0];
            
        }

        public void Draw(GraphicsDevice graphics, BasicEffect efeito)
        {

            //World, View, Projection
            efeito.World = Camera.World;
            efeito.View = Camera.View;
            efeito.Projection = Camera.Projection;

            foreach (EffectPass pass in efeito.CurrentTechnique.Passes)
            {
                pass.Apply();

                //Definir os buffers a utilizar
                graphics.SetVertexBuffer(vertexBuffer);
                graphics.Indices = indexBuffer;

                //Desenhar as primitivas
                graphics.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertexes, 0, vertexes.Length - 2);
            }
        }
    }
}
