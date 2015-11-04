using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vertex
{
    class VariableVertexes
    {

        private VertexPositionColor[] vertexes;
        private int nLados;
        
        VertexBuffer buffer;

        public VariableVertexes(Vector3 position, float size, int nLados)
        {
            this.nLados = nLados;
            vertexes = new VertexPositionColor[10];

            float graus = 0;
            float step = MathHelper.ToRadians(360 / nLados);

            Vector3 topo = new Vector3(0, 1, 0);

            Vector3 v0 = new Vector3(1, 0, 0);
            Vector3 v1 = topo;
            Vector3 v2 = new Vector3(0, 0, -1);
            Vector3 v3 = topo;
            Vector3 v4 = new Vector3(-1, 0, 0);
            Vector3 v5 = topo;
            Vector3 v6 = new Vector3(0, 0, 1);
            Vector3 v7 = topo;
            Vector3 v8 = v0;
            Vector3 v9 = topo;

            vertexes[0] = new VertexPositionColor(v0, Color.Red);
            vertexes[1] = new VertexPositionColor(v1, Color.Red);
            vertexes[2] = new VertexPositionColor(v2, Color.Red);
            vertexes[3] = new VertexPositionColor(v3, Color.Green);
            vertexes[4] = new VertexPositionColor(v4, Color.Green);
            vertexes[5] = new VertexPositionColor(v5, Color.Green);
            vertexes[6] = new VertexPositionColor(v6, Color.Blue);
            vertexes[7] = new VertexPositionColor(v7, Color.Blue);
            vertexes[8] = new VertexPositionColor(v8, Color.Blue);
            vertexes[9] = new VertexPositionColor(v9, Color.Blue);
            
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
                graphics.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertexes, 0, 8);
            } 
        }

    }
}
