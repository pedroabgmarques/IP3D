using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPC1_7994
{
    class Piramide
    {
        //Array de vertices
        private VertexPositionColor[] vertexes;

        //Array de cores
        private Color[] cor;


        // construtor piramide
        public Piramide(GraphicsDevice graphics, int nLados)
        {
            //inicializao do Array
            vertexes = new VertexPositionColor[2*nLados+1];

            //atribuicao de cores
            cor = new Color[4];

            cor[0] = Color.Red;
            cor[1] = Color.Gray;
            cor[2] = Color.Blue;
            cor[3] = Color.Yellow;

            //vertice do topo
            Vector3 topo = new Vector3(0, 1, 0);

            //angulo de vertices
            float angulo = 0;
            //diferenca de angulo por cada vertice
            float passo = MathHelper.ToRadians(360 / nLados);
            // contador de cor
            int c = 0;


            //atribuiçao de vertices ao trocar angulo e cor
            for (int i = 0; i < nLados; i++)
            {
                vertexes[2*i] = new VertexPositionColor(new Vector3((float)Math.Cos(angulo),0,(float)-Math.Sin(angulo)), cor[c]);
                vertexes[2*i+1] = new VertexPositionColor(topo, cor[c]);
                angulo += passo;
                c++;
                if (c > 3)
                {
                    c = 0;

                }
                {
                    
                }
            }


            vertexes[2 * nLados] = vertexes[0];
            
        }

        public void Draw(GraphicsDevice graphics, BasicEffect efeito, Matrix world, Matrix view, Matrix projection)
        {

            //World, View, Projection
            efeito.World = world;
            efeito.View = view;
            efeito.Projection = projection;

            foreach (EffectPass pass in efeito.CurrentTechnique.Passes)
            {
                pass.Apply();

               

                //Desenhar as primitivas
                graphics.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertexes, 0, vertexes.Length - 2);
            }
        }
    }
}
