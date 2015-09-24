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
        private VertexPositionNormalTexture[] vertexList;

        /// <summary>
        /// Buffer de vértices a desenhar
        /// </summary>
        private VertexBuffer vertexBuffer;

        private BasicEffect efeito;
        /// <summary>
        /// Efeito a utilizar para desenhar a geometria
        /// </summary>
        public BasicEffect Efeito
        {
            get { return efeito; }
        }

        //Tamanho do lado do quadrado
        private Vector3 size;
        //Posição do cubo no mundo
        private Vector3 position;
        //Velocidade da rotacao
        private int vRotacao;

        /// <summary>
        /// Construtor
        /// </summary>
        public Geometry(GraphicsDevice graphics, Random random)
        {
            this.vertexBuffer = Create3DGeometry(graphics, random);
        }

        Texture2D textura;
        
        /// <summary>
        /// Define a geometria a ser desenhada
        /// </summary>
        /// <param name="graphics">Instância de GraphicsDevice</param>
        /// <returns></returns>
        private VertexBuffer Create3DGeometry(GraphicsDevice graphics, Random random)
        {
            //3 vértices, 1 triângulo
            vertexList = new VertexPositionNormalTexture[36];

            position = new Vector3(random.Next(-25, 25), random.Next(-15, 15), random.Next(-30, 30));
            size = Vector3.One;
            vRotacao = random.Next(40, 100);

            // Calculate the position of the vertices on the top face.
            Vector3 topLeftFront = position + new Vector3(size.X, size.Y, size.Z);
            Vector3 topLeftBack = position + new Vector3(0, size.Y, size.Z);
            Vector3 topRightFront = position + new Vector3(size.X, size.Y, 0);
            Vector3 topRightBack = position + new Vector3(0, size.Y, 0);

            // Calculate the position of the vertices on the bottom face.
            Vector3 btmLeftFront = position + new Vector3(size.X, 0, size.Z);
            Vector3 btmLeftBack = position + new Vector3(0, 0, size.Z);
            Vector3 btmRightFront = position + new Vector3(size.X, 0, 0);
            Vector3 btmRightBack = position;

            // Normal vectors for each face (needed for lighting / display)
            Vector3 normalFront = new Vector3(0.0f, 0.0f, 1.0f) * size;
            Vector3 normalBack = new Vector3(0.0f, 0.0f, -1.0f) * size;
            Vector3 normalTop = new Vector3(0.0f, 1.0f, 0.0f) * size;
            Vector3 normalBottom = new Vector3(0.0f, -1.0f, 0.0f) * size;
            Vector3 normalLeft = new Vector3(-1.0f, 0.0f, 0.0f) * size;
            Vector3 normalRight = new Vector3(1.0f, 0.0f, 0.0f) * size;

            // UV texture coordinates
            Vector2 textureTopLeft = new Vector2(1.0f, 0.0f);
            Vector2 textureTopRight = new Vector2(0.0f, 0.0f);
            Vector2 textureBottomLeft = new Vector2(1.0f, 1.0f);
            Vector2 textureBottomRight = new Vector2(0.0f, 1.0f);

            // Add the vertices for the FRONT face.
            vertexList[0] = new VertexPositionNormalTexture(topLeftFront, normalFront, textureTopLeft);
            vertexList[1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            vertexList[2] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);
            vertexList[3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            vertexList[4] = new VertexPositionNormalTexture(btmRightFront, normalFront, textureBottomRight);
            vertexList[5] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);

            // Add the vertices for the BACK face.
            vertexList[6] = new VertexPositionNormalTexture(topLeftBack, normalBack, textureTopRight);
            vertexList[7] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            vertexList[8] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            vertexList[9] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            vertexList[10] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            vertexList[11] = new VertexPositionNormalTexture(btmRightBack, normalBack, textureBottomLeft);

            // Add the vertices for the TOP face.
            vertexList[12] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            vertexList[13] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);
            vertexList[14] = new VertexPositionNormalTexture(topLeftBack, normalTop, textureTopLeft);
            vertexList[15] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            vertexList[16] = new VertexPositionNormalTexture(topRightFront, normalTop, textureBottomRight);
            vertexList[17] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);

            // Add the vertices for the BOTTOM face. 
            vertexList[18] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            vertexList[19] = new VertexPositionNormalTexture(btmLeftBack, normalBottom, textureBottomLeft);
            vertexList[20] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            vertexList[21] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            vertexList[22] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            vertexList[23] = new VertexPositionNormalTexture(btmRightFront, normalBottom, textureTopRight);

            // Add the vertices for the LEFT face.
            vertexList[24] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);
            vertexList[25] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            vertexList[26] = new VertexPositionNormalTexture(btmLeftFront, normalLeft, textureBottomRight);
            vertexList[27] = new VertexPositionNormalTexture(topLeftBack, normalLeft, textureTopLeft);
            vertexList[28] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            vertexList[29] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);

            // Add the vertices for the RIGHT face. 
            vertexList[30] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            vertexList[31] = new VertexPositionNormalTexture(btmRightFront, normalRight, textureBottomLeft);
            vertexList[32] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);
            vertexList[33] = new VertexPositionNormalTexture(topRightBack, normalRight, textureTopRight);
            vertexList[34] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            vertexList[35] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);

            //Inserir os vértices no buffer
            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionNormalTexture), vertexList.GetLength(0), BufferUsage.WriteOnly);

            return vertexBuffer;
        }

        /// <summary>
        /// Define o efeito (shader / HLSL) a usar para desenhar a cena
        /// </summary>
        /// <param name="graphics"></param>
        public void LoadContent(GraphicsDevice graphics, ContentManager content)
        {
            //Criar um basicEffect que vamos usar para renderizar o triângulo
            efeito = new BasicEffect(graphics);
            textura = content.Load<Texture2D>("box_texture");
        }

        public void Draw(Matrix World, Matrix View, Matrix Projection, GraphicsDevice graphics)
        {
            //World, View, Projection
            efeito.World *=  Matrix.CreateTranslation(-position) //Voltar para a origem
                * Matrix.CreateTranslation(-size.X / 2, -size.Y / 2, -size.Z / 2) //Origem no centro do objeto
                * Matrix.CreateRotationX(MathHelper.PiOver4 / vRotacao) //Rotações
                * Matrix.CreateRotationY(MathHelper.PiOver4 / vRotacao)
                * Matrix.CreateRotationZ(MathHelper.PiOver4 / vRotacao)
                * Matrix.CreateTranslation(position) //Voltar para a posição inicial
                * Matrix.CreateTranslation(size.X / 2, size.Y / 2, size.Z / 2); //"Descentrar"
            efeito.View = View;
            efeito.Projection = Projection;

            //Textura
            efeito.TextureEnabled = true;
            efeito.Texture = textura;

            //Iluminação
            efeito.EnableDefaultLighting();
            
            // Load the buffer
            vertexBuffer.SetData(vertexList);

            // Send the vertex buffer to the device
            graphics.SetVertexBuffer(vertexBuffer);

            foreach (EffectPass pass in efeito.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives(PrimitiveType.TriangleList, vertexList, 0, 12);
            }
        }

    }
}
