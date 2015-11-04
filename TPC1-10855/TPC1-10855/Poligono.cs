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
        //Array de índices
        private short[] indices;

        //Buffers
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        public Poligono(GraphicsDevice graphics, Random random, int nLados, Vector3 posicao)
        {

            //O tamanho do array de vertices é o número de lados + 1 (vértice do topo)
            vertexes = new VertexPositionColor[nLados + 1];

            //Posição do vértice do topo
            Vector3 topo = new Vector3(posicao.X, posicao.Y + 1, posicao.Z);

            //Guarda o ângulo de cada vértice
            float angulo = 0;
            //Diferença de ângulo para cada ponto
            float step = MathHelper.ToRadians(360 / nLados);

            //Criar os vértices da base
            for (int i = 0; i < nLados; i++)
            {
                vertexes[i] = new VertexPositionColor(new Vector3(
                    posicao.X + (float)Math.Cos(angulo), 
                    posicao.Y, 
                    posicao.Z + (float)-Math.Sin(angulo)), 
                    new Color (( byte)random.Next( 0, 255 ), (byte) random.Next(0, 255), (byte)random.Next( 0, 255 )));
                //Incrementar o ângulo para o próximo ponto
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

            //Enviar os buffers para a memória de GPU
            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), vertexes.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertexes);

            indexBuffer = new IndexBuffer(graphics, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData<short>(indices);
            
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
                graphics.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, vertexes.Length, 0, indices.Length - 2);
            }
        }
    }
}
