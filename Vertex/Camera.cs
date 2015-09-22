using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vertex
{
    static class Camera
    {

        //Matrizes World, View e Projection
        static public Matrix World, View, Projection;

        /// <summary>
        /// Inicializa os componentes da camara
        /// </summary>
        /// <param name="graphics">Instância de GraphicsDevice</param>
        static public void Initialize(GraphicsDevice graphics)
        {
            //Inicializar as matrizes world, view e projection
            World = Matrix.Identity;
            View = Matrix.CreateLookAt(new Vector3(0, 1, 5), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45),
                graphics.Viewport.Width / graphics.Viewport.Height,
                0.01f,
                200f);

            //Criar e definir o resterizerState a utilizar para desenhar a geometria
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.CullClockwiseFace;
            rasterizerState.MultiSampleAntiAlias = true;
            graphics.RasterizerState = rasterizerState;
        }

    }
}
