using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vertex
{
    class TringleStripPlane
    {

        private VertexPositionNormalTexture[] vertexes;

        public TringleStripPlane(Vector3 position, float size, Vector3 normal)
        {
            vertexes = new VertexPositionNormalTexture[6];

            //Vértices
            //Quadrado triangle strip
            Vector3 topLeft = new Vector3(position.X - size / 2, position.Y - size / 2, position.Z);
            Vector3 bottomLeft = new Vector3(position.X - size / 2, position.Y + size - size / 2, position.Z);
            Vector3 topRight = new Vector3(position.X + size - size / 2, position.Y - size / 2, position.Z);
            Vector3 bottomRight = new Vector3(position.X + size - size / 2, position.Y + size - size / 2, position.Z);

            // UV texture coordinates
            Vector2 textureTopLeft = new Vector2(0.0f, 1.0f);
            Vector2 textureTopRight = new Vector2(1.0f, 1.0f);
            Vector2 textureBottomLeft = new Vector2(0.0f, 0.0f);
            Vector2 textureBottomRight = new Vector2(1.0f, 0.0f);

            vertexes[0] = new VertexPositionNormalTexture(topLeft, normal, textureTopLeft);
            vertexes[1] = new VertexPositionNormalTexture(bottomLeft, normal, textureBottomLeft);
            vertexes[2] = new VertexPositionNormalTexture(topRight, normal, textureTopRight);
            vertexes[3] = new VertexPositionNormalTexture(bottomRight, normal, textureBottomRight);

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
                graphics.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertexes, 0, 2);
            }
        }

    }
}
