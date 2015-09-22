using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vertex
{
    static class Geometry
    {

        /// <summary>
        /// Array para guardar os vértices e respetivas cores
        /// </summary>
        static private VertexPositionColor[] vertexList;

        /// <summary>
        /// Buffer de vértices a desenhar
        /// </summary>
        static private VertexBuffer vertexBuffer;

        static private BasicEffect efeito;
        /// <summary>
        /// Efeito a utilizar para desenhar a geometria
        /// </summary>
        static public BasicEffect Efeito
        {
            get { return efeito; }
        }
        

        /// <summary>
        /// Define a geometria a ser desenhada
        /// </summary>
        /// <param name="graphics">Instância de GraphicsDevice</param>
        /// <returns></returns>
        static public VertexBuffer Create3DGeometry(GraphicsDevice graphics)
        {
            //3 vértices, 1 triângulo
            vertexList = new VertexPositionColor[3];

            //Definir os vértices
            vertexList[0] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Red);
            vertexList[1] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Green);
            vertexList[2] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Blue);

            //Inserir os vértices no buffer
            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), vertexList.GetLength(0), BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertexList);

            return vertexBuffer;
        }

        /// <summary>
        /// Define o efeito (shader / HLSL) a usar para desenhar a cena
        /// </summary>
        /// <param name="graphics"></param>
        static public void LoadContent(GraphicsDevice graphics)
        {
            //Criar um basicEffect que vamos usar para renderizar o triângulo
            efeito = new BasicEffect(graphics);
        }

        static public void Draw(Matrix World, Matrix View, Matrix Projection, GraphicsDevice graphics)
        {
            efeito.World = World;
            efeito.View = View;
            efeito.Projection = Projection;
            efeito.VertexColorEnabled = true;

            graphics.SetVertexBuffer(Create3DGeometry(graphics));

            foreach (EffectPass pass in efeito.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
            }
        }

    }
}
