using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos3D___Tank
{
    class Tank
    {

        Model tank;
        Matrix World;

        public Tank(ContentManager content, string modelo)
        {
            tank = content.Load<Model>(modelo);
            World = Matrix.Identity;
        }

        public void Update()
        {
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Left))
            {
                World *= Matrix.CreateTranslation(new Vector3(-0.1f, 0, 0));
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                World *= Matrix.CreateTranslation(new Vector3(+0.1f, 0, 0));
            }
            if (kb.IsKeyDown(Keys.Up))
            {
                World *= Matrix.CreateTranslation(new Vector3(0, 0, -0.1f));
            }
            if (kb.IsKeyDown(Keys.Down))
            {
                World *= Matrix.CreateTranslation(new Vector3(0, 0, +0.1f));
            }
        }

        public void Draw(BasicEffect efeito)
        {
            foreach (ModelMesh mesh in tank.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = World;
                    effect.View = efeito.View;
                    effect.Projection = efeito.Projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }

    }
}
