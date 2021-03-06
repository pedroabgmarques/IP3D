﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vertex
{
    static class Create3DAxis
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

        static public void Initialize(GraphicsDevice graphics)
        {
            vertexBuffer = Initialize3DAxis(graphics);
            efeito = new BasicEffect(graphics);
        }

        static private VertexBuffer Initialize3DAxis(GraphicsDevice graphics)
        {
            vertexList = new VertexPositionColor[6];
            int size = 2;

            //Eixo dos XX
            vertexList[0] = new VertexPositionColor(new Vector3(-size, 0, 0), Color.Red);
            vertexList[1] = new VertexPositionColor(new Vector3(size, 0, 0), Color.Red);

            //Eixo dos YY
            vertexList[2] = new VertexPositionColor(new Vector3(0, -size, 0), Color.Green);
            vertexList[3] = new VertexPositionColor(new Vector3(0, size, 0), Color.Green);

            //Eixo dos ZZ
            vertexList[4] = new VertexPositionColor(new Vector3(0, 0, -size), Color.Blue);
            vertexList[5] = new VertexPositionColor(new Vector3(0, 0, size), Color.Blue);

            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), vertexList.GetLength(0), BufferUsage.WriteOnly);

            return vertexBuffer;
        }

        static public void Draw(Matrix World, Matrix View, Matrix Projection, GraphicsDevice graphics, Matrix world)
        {
            //World, View, Projection
            efeito.World = world;
            efeito.View = View;
            efeito.Projection = Projection;

            //Iluminação
            efeito.VertexColorEnabled = true;

            //Fog
            efeito.FogEnabled = true;
            efeito.FogColor = Vector3.Zero;
            efeito.FogStart = Camera.nearPlane;
            efeito.FogEnd = Camera.farPlaneShort;

            //Load the buffer
            vertexBuffer.SetData(vertexList);

            // Send the vertex buffer to the device
            graphics.SetVertexBuffer(vertexBuffer);

            foreach (EffectPass pass in efeito.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives(PrimitiveType.LineList, vertexList, 0, 3);
            }
        }
    }
}
