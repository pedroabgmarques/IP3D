using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vertex
{
    class PyramidIndices
    {

        private VertexPositionColor[] vertexes;
        private short[] indices;

        public PyramidIndices()
        {
            vertexes = new VertexPositionColor[5];

            vertexes[0] = new VertexPositionColor(new Vector3(1, 0, 0), Color.Red);
            vertexes[1] = new VertexPositionColor(new Vector3(0, 0, -1), Color.Red);
            vertexes[2] = new VertexPositionColor(new Vector3(-1, 0, 0), Color.Red);
            vertexes[3] = new VertexPositionColor(new Vector3(0, 0, 1), Color.Red);
            vertexes[4] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Red);

            indices = new short[9];
            indices[0] = 0;
            indices[1] = 4;
            indices[2] = 1;
            indices[3] = 4;
            indices[4] = 2;
            indices[5] = 4;
            indices[6] = 3;
            indices[7] = 4;
            indices[8] = 0;

            /*
            
            Gerar estes indices programaticamente:
            
            for(int i = 0; i <= n; i++){
               ind[2i] = (i % n);
               ind[2i+1] = 4
            }
              
            */

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
                graphics.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertexes, 0, 5, indices, 0, 7);
            }
        }
    }
}
