using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vertex
{
    class Geometry
    {

        /// <summary>
        /// Array para guardar os vértices e respetivas cores
        /// </summary>
        private VertexPositionNormalTexture[] vertexes;

        /// <summary>
        /// Buffer de vértices a desenhar
        /// </summary>
        private VertexBuffer vertexBuffer;

        //Tamanho do lado do quadrado
        private Vector3 size;
        //Posição do cubo no mundo
        public Vector3 position;
        //Rotação
        public float Rotacao;
        private float vRotacao;

        /// <summary>
        /// Utilizado para verificar se o objeto está visível do ponto de vista da camera
        /// </summary>
        public BoundingSphere boundingSphere;

        bool rotateX, rotateY, rotateZ;

        /// <summary>
        /// Construtor
        /// </summary>
        public Geometry(GraphicsDevice graphics, Random random)
        {
            this.vertexBuffer = Create3DGeometry(graphics, random);
            rotateX = false;
            rotateY = false;
            rotateZ = false;

            if (random.NextDouble() > 0.5)
            {
                rotateX = true;
            }
            if (random.NextDouble() > 0.5)
            {
                rotateY = true;
            }
            if (random.NextDouble() > 0.5)
            {
                rotateZ = true;
            }
        }
        
        /// <summary>
        /// Define a geometria a ser desenhada
        /// </summary>
        /// <param name="graphics">Instância de GraphicsDevice</param>
        /// <returns></returns>
        private VertexBuffer Create3DGeometry(GraphicsDevice graphics, Random random)
        {
            //3 vértices, 1 triângulo
            vertexes = new VertexPositionNormalTexture[36];

            position = new Vector3(random.Next(-Camera.worldSize / 2, Camera.worldSize / 2),
                random.Next(-Camera.worldSize / 2, Camera.worldSize / 2), 
                random.Next(-Camera.worldSize / 2, Camera.worldSize / 2));
            int sideSize = 2;//random.Next(0, 3);
            size = new Vector3(sideSize, sideSize, sideSize);
            vRotacao = (float)random.NextDouble() / 20f;
            Rotacao = (float)random.NextDouble() / 20f;

            // Calculate the position of the vertices on the top face.

            Vector3 topLeftFront = new Vector3(-1.0f, 1.0f, -1.0f);

            Vector3 topLeftBack = new Vector3(-1.0f, 1.0f, 1.0f);

            Vector3 topRightFront = new Vector3(1.0f, 1.0f, -1.0f);

            Vector3 topRightBack = new Vector3(1.0f, 1.0f, 1.0f);



            // Calculate the position of the vertices on the bottom face.

            Vector3 btmLeftFront = new Vector3(-1.0f, -1.0f, -1.0f);

            Vector3 btmLeftBack = new Vector3(-1.0f, -1.0f, 1.0f);

            Vector3 btmRightFront = new Vector3(1.0f, -1.0f, -1.0f);

            Vector3 btmRightBack = new Vector3(1.0f, -1.0f, 1.0f);



            // Normal vectors for each face (needed for lighting / display)

            Vector3 normalFront = new Vector3(0.0f, 0.0f, -1.0f);

            Vector3 normalBack = new Vector3(0.0f, 0.0f, 1.0f);

            Vector3 normalTop = new Vector3(0.0f, 1.0f, 0.0f);

            Vector3 normalBottom = new Vector3(0.0f, -1.0f, 0.0f);

            Vector3 normalLeft = new Vector3(-1.0f, 0.0f, 0.0f);

            Vector3 normalRight = new Vector3(1.0f, 0.0f, 0.0f);



            // UV texture coordinates

            Vector2 textureTopLeft = new Vector2(1.0f, 0.0f);

            Vector2 textureTopRight = new Vector2(0.0f, 0.0f);

            Vector2 textureBottomLeft = new Vector2(1.0f, 1.0f);

            Vector2 textureBottomRight = new Vector2(0.0f, 1.0f);



            // Add the vertices for the FRONT face.

            vertexes[0] = new VertexPositionNormalTexture(topLeftFront, normalFront, textureTopLeft);

            vertexes[1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);

            vertexes[2] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);

            vertexes[3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);

            vertexes[4] = new VertexPositionNormalTexture(btmRightFront, normalFront, textureBottomRight);

            vertexes[5] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);



            // Add the vertices for the BACK face.

            vertexes[6] = new VertexPositionNormalTexture(topLeftBack, normalBack, textureTopRight);

            vertexes[7] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);

            vertexes[8] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);

            vertexes[9] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);

            vertexes[10] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);

            vertexes[11] = new VertexPositionNormalTexture(btmRightBack, normalBack, textureBottomLeft);



            // Add the vertices for the TOP face.

            vertexes[12] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);

            vertexes[13] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);

            vertexes[14] = new VertexPositionNormalTexture(topLeftBack, normalTop, textureTopLeft);

            vertexes[15] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);

            vertexes[16] = new VertexPositionNormalTexture(topRightFront, normalTop, textureBottomRight);

            vertexes[17] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);



            // Add the vertices for the BOTTOM face. 

            vertexes[18] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);

            vertexes[19] = new VertexPositionNormalTexture(btmLeftBack, normalBottom, textureBottomLeft);

            vertexes[20] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);

            vertexes[21] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);

            vertexes[22] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);

            vertexes[23] = new VertexPositionNormalTexture(btmRightFront, normalBottom, textureTopRight);



            // Add the vertices for the LEFT face.

            vertexes[24] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);

            vertexes[25] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);

            vertexes[26] = new VertexPositionNormalTexture(btmLeftFront, normalLeft, textureBottomRight);

            vertexes[27] = new VertexPositionNormalTexture(topLeftBack, normalLeft, textureTopLeft);

            vertexes[28] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);

            vertexes[29] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);



            // Add the vertices for the RIGHT face. 

            vertexes[30] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);

            vertexes[31] = new VertexPositionNormalTexture(btmRightFront, normalRight, textureBottomLeft);

            vertexes[32] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);

            vertexes[33] = new VertexPositionNormalTexture(topRightBack, normalRight, textureTopRight);

            vertexes[34] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);

            vertexes[35] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);

            boundingSphere.Center = Vector3.Transform(position, Matrix.CreateTranslation(size.X / 2, size.Y / 2, size.Z / 2));
            boundingSphere.Radius = sideSize;

            //Inserir os vértices no buffer
            //vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionNormalTexture), vertexes.GetLength(0), BufferUsage.WriteOnly);

            return vertexBuffer;
        }

        /// <summary>
        /// Define o efeito (shader / HLSL) a usar para desenhar a cena
        /// </summary>
        /// <param name="graphics"></param>
        public void LoadContent(GraphicsDevice graphics, ContentManager content)
        {
            //Criar um basicEffect que vamos usar para renderizar o triângulo
            
        }

        public void Draw(Matrix World, Matrix View, Matrix Projection, GraphicsDevice graphics, ref BasicEffect efeito, Random random)
        {
            Rotacao += vRotacao;

            //// Load the buffer
            //vertexBuffer.SetData(vertexes);

            //// Send the vertex buffer to the device
            //graphics.SetVertexBuffer(vertexBuffer);

            efeito.World = Matrix.CreateRotationX(Rotacao);

            if(rotateX){
                efeito.World *= Matrix.CreateRotationX(Rotacao);
            }
            if (rotateY)
            {
                efeito.World *= Matrix.CreateRotationY(Rotacao);
            }
            if (rotateZ)
            {
                efeito.World *= Matrix.CreateRotationZ(Rotacao);
            }  

            efeito.World *= Matrix.CreateScale(0.8f) * Matrix.CreateTranslation(position);

            foreach (EffectPass pass in efeito.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives(PrimitiveType.TriangleList, vertexes, 0, 12);
            }
        }

    }
}
