using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple_Camera
{
    static class Camera
    {

        public static Matrix World, View, Projection, rotacao;
        private static Vector3 pos, dir, target, baseVector;
        private static float vel;
        static float yaw, pitch, roll;
        static float grausPorPixel;

        static public void Initialize(GraphicsDevice graphics)
        {
            World = Matrix.Identity;
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90),
                graphics.Viewport.AspectRatio,
                0.1f,
                100f);
            pos = new Vector3(0, 0, 5);
            dir = new Vector3(0, 0, -1);
            target = pos + dir;
            baseVector = new Vector3(1, 0, 0);
            vel = 0.001f;
            grausPorPixel = 0.01f / 20f;
            yaw = 0;
            pitch = 0;
            roll = 0;
            View = Matrix.CreateLookAt(pos, target, Vector3.Up);
        }

        static public void Frente(GameTime gameTime)
        {
            pos += vel * dir * gameTime.ElapsedGameTime.Milliseconds;
            UpdateViewMatrix();
        }

        static public void Tras(GameTime gameTime)
        {
            pos -= vel * dir * gameTime.ElapsedGameTime.Milliseconds;
            UpdateViewMatrix();
        }

        static public void RodarDir(int pixels)
        {
            yaw = grausPorPixel * pixels;
            rotacao = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);
            AplicarRotacao();
        }

        static public void RodarEsq(int pixels)
        {
            yaw = grausPorPixel * pixels;
            rotacao = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);
            AplicarRotacao();
        }

        static private void AplicarRotacao()
        {
            dir = Vector3.Transform(dir, rotacao);
            UpdateViewMatrix();
        }

        static private void UpdateViewMatrix()
        {
            target = pos + dir;
            View = Matrix.CreateLookAt(pos, target, Vector3.Up);
        }



    }
}
