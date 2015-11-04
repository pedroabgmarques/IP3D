using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vertex
{
    class PyramidBuffers
    {

        private VertexPositionColor[] vertexes;
        private short[] indices;

        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        float angulo = 0;

        public PyramidBuffers(GraphicsDevice graphics, Random random, int nLados)
        {

            //O tamanho do array de vertices é o número de lados + 1 (vértice do topo)
            vertexes = new VertexPositionColor[nLados + 1];

            //Posição do vértice do topo
            Vector3 topo = new Vector3(0, 1, 0);

            //Guarda o ângulo de cada vértice
            float angulo = 0;
            float step = MathHelper.ToRadians(360 / nLados);

            //Criar os vértices da base
            for (int i = 0; i < nLados; i++)
            {
                vertexes[i] = new VertexPositionColor(new Vector3((float)Math.Cos(angulo), 0, (float)-Math.Sin(angulo)), 
                    new Color (( byte)random.Next( 0, 255 ), (byte) random.Next(0, 255), (byte)random.Next( 0, 255 )));
                angulo += step;
            }
            //Vértice de topo
            vertexes[nLados] = new VertexPositionColor(topo, Color.Black);

            //Inicializar o array de índices
            indices = new short[2 * nLados + 1];

            //Gerar os índices
            for(int i = 0; i < nLados; i++){
               indices[2 * i] = (short)(i % nLados);
               indices[2 * i + 1] = (short)nLados;
            }

            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), vertexes.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertexes);

            indexBuffer = new IndexBuffer(graphics, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData<short>(indices);
            
        }

        public void Draw(Matrix World, Matrix View, Matrix Projection, GraphicsDevice graphics, BasicEffect efeito)
        {

            angulo += 0.01f;

            Matrix rotacaoEixoAngulo = Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), angulo);
            Matrix translacao = Matrix.CreateTranslation(new Vector3(-2, 0, 2));

            //World, View, Projection
            efeito.World = rotacaoEixoAngulo * translacao;
            efeito.View = View;
            efeito.Projection = Projection;

            foreach (EffectPass pass in efeito.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.SetVertexBuffer(vertexBuffer);
                graphics.Indices = indexBuffer;

                graphics.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, vertexes.Length, 0, indices.Length - 2);
            }
        }

    }
}
