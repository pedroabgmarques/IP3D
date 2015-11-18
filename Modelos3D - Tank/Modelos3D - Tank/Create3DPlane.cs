using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos3D___Tank
{
    class Create3DPlane
    {
        private VertexPositionNormalTexture[] vertexes;

        public Create3DPlane(Vector3 position, float size, Vector3 normal)
        {
            vertexes = new VertexPositionNormalTexture[6];

            //Vértices
            //1º triângulo
            Vector3 topLeft = new Vector3(position.X - size / 2, position.Y, -position.Z - size / 2);
            Vector3 bottomLeft = new Vector3(position.X - size / 2, position.Y, position.Z + size / 2);
            Vector3 topRight = new Vector3(position.X + size / 2, position.Y, position.Z - size / 2);

            //2º triângulo
            //bottomLeft já está definido
            Vector3 bottomRight = new Vector3(position.X + size / 2, position.Y, position.Z + size / 2);
            //topRight já está definido

            // Coordenadas da textura
            Vector2 textureTopLeft = new Vector2(0.0f, 0.0f);
            Vector2 textureTopRight = new Vector2(1.0f, 0.0f);
            Vector2 textureBottomLeft = new Vector2(0.0f, 1.0f);
            Vector2 textureBottomRight = new Vector2(1.0f, 1.0f);

            vertexes[0] = new VertexPositionNormalTexture(topLeft, normal, textureTopLeft);
            vertexes[1] = new VertexPositionNormalTexture(bottomLeft, normal, textureBottomLeft);
            vertexes[2] = new VertexPositionNormalTexture(topRight, normal, textureTopRight);

            vertexes[3] = new VertexPositionNormalTexture(bottomLeft, normal, textureBottomLeft);
            vertexes[4] = new VertexPositionNormalTexture(bottomRight, normal, textureBottomRight);
            vertexes[5] = new VertexPositionNormalTexture(topRight, normal, textureTopRight);
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
                graphics.DrawUserPrimitives(PrimitiveType.TriangleList, vertexes, 0, 2);
            }
        }

    }
}
