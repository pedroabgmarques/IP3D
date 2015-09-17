using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testes_Iniciais
{
    class GameModel : GameComponent
    {
        public Model modelo;
        public Vector3 position;
        public float angle;

        public GameModel(Game game, Model modelo, Vector3 position, float angle)
            : base(game)
        {
            this.modelo = modelo;
            this.position = position;
            this.angle = angle;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position -= new Vector3(0.1f, 0, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position += new Vector3(0.1f, 0, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position += new Vector3(0, 0.1f, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position -= new Vector3(0, 0.1f, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position += new Vector3(0, 0, 0.1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                position -= new Vector3(0, 0, 0.1f);
            }

            angle += 0.03f;
        }
    }
}
