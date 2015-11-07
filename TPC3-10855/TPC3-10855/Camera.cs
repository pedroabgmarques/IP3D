using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPC3_10855
{
    static class Camera
    {

        //Matrizes World, View e Projection
        static public Matrix World, View, Projection;

        //Posição da camara
        static private Vector3 position;

        //Rotação horizontal
        static float leftrightRot = 0f;
        //Rotação vertical
        static float updownRot = 0f;
        //Velocidade da rotação
        const float rotationSpeed = 0.3f;
        //Velocidade do movimento com o rato
        const float moveSpeed = 5f;
        //Estado do rato
        static private MouseState originalMouseState;
        //BoundingFrustum da camâra
        static public BoundingFrustum frustum;
        //Tamanho do "mundo"
        static public int worldSize = 700;
        //Near e far plane
        static public float nearPlane = 0.1f;
        static public float farPlane = worldSize / 3;

        static private KeyboardState keyStateAnterior;

        //RasterizerStates para solid / wireframe
        static RasterizerState rasterizerStateSolid;
        static RasterizerState rasterizerStateWireFrame;

        //Desenhar normais do terreno
        static public bool drawNormals = false;

        /// <summary>
        /// Inicializa os componentes da camara
        /// </summary>
        /// <param name="graphics">Instância de GraphicsDevice</param>
        static public void Initialize(GraphicsDevice graphics)
        {
            //Posição inicial da camâra
            position = new Vector3(0, 3, 15);
            //Inicializar as matrizes world, view e projection
            World = Matrix.Identity;
            UpdateViewMatrix();
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45),
                graphics.Viewport.AspectRatio,
                nearPlane,
                farPlane);

            //Criar e definir o resterizerState a utilizar para desenhar a geometria
            RasterizerState rasterizerState = new RasterizerState();
            //Desenha todas as faces, independentemente da orientação
            rasterizerState.CullMode = CullMode.None;
            //rasterizerState.FillMode = FillMode.WireFrame;
            rasterizerState.MultiSampleAntiAlias = true;
            graphics.RasterizerState = rasterizerState;

            //Coloca o rato no centro do ecrã
            Mouse.SetPosition(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);

            //Criar e definir os resterizerStates a utilizar para desenhar a geometria
            //SOLID
            rasterizerStateSolid = new RasterizerState();
            rasterizerStateSolid.CullMode = CullMode.None;
            rasterizerStateSolid.MultiSampleAntiAlias = true;
            rasterizerStateSolid.FillMode = FillMode.Solid;
            rasterizerStateSolid.SlopeScaleDepthBias = 0.1f;
            graphics.RasterizerState = rasterizerStateSolid;

            //WIREFRAME
            rasterizerStateWireFrame = new RasterizerState();
            rasterizerStateWireFrame.CullMode = CullMode.None;
            rasterizerStateWireFrame.MultiSampleAntiAlias = true;
            rasterizerStateWireFrame.FillMode = FillMode.WireFrame;   

            originalMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Calcula e atualiza a ViewMatrix para cada frame, consoante a posição e rotação da camâra
        /// </summary>
        static private void UpdateViewMatrix()
        {
            //Cálculo da matriz de rotação
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            //Target
            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = position + cameraRotatedTarget;
            //Cálculo do vector Up
            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);
            Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);
            //Matriz View
            View = Matrix.CreateLookAt(position, cameraFinalTarget, cameraRotatedUpVector);
            //Atualiza o frustum
            frustum = new BoundingFrustum(View * Projection);
        }

        /// <summary>
        /// Implementa os controlos da camâra
        /// </summary>
        /// <param name="amount">Tempo decorrido desde o ultimo update</param>
        /// <param name="graphics">Instância de graphicsDevice</param>
        static private void ProcessInput(float amount, GraphicsDevice graphics)
        {
            //Movimento do rato
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState != originalMouseState)
            {
                float xDifference = currentMouseState.X - originalMouseState.X;
                float yDifference = currentMouseState.Y - originalMouseState.Y;
                leftrightRot -= rotationSpeed * xDifference * amount;
                updownRot -= rotationSpeed * yDifference * amount;
                try
                {
                    Mouse.SetPosition(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
                }
                catch (Exception)
                {
                    //Impede de dar erro quando se sai do programa
                }
                UpdateViewMatrix();
            }

            //Controlos do teclado
            Vector3 moveVector = new Vector3(0, 0, 0);
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
                moveVector += new Vector3(0, 0, -1);
            if (keyState.IsKeyDown(Keys.S))
                moveVector += new Vector3(0, 0, 1);
            if (keyState.IsKeyDown(Keys.D))
                moveVector += new Vector3(1, 0, 0);
            if (keyState.IsKeyDown(Keys.A))
                moveVector += new Vector3(-1, 0, 0);
            if (keyState.IsKeyDown(Keys.Q))
                moveVector += new Vector3(0, 1, 0);
            if (keyState.IsKeyDown(Keys.E))
                moveVector += new Vector3(0, -1, 0);
            AddToCameraPosition(moveVector * amount);

            if (keyState.IsKeyDown(Keys.O) && !keyStateAnterior.IsKeyDown(Keys.O))
            {
                if (graphics.RasterizerState == rasterizerStateSolid)
                {
                    graphics.RasterizerState = rasterizerStateWireFrame;
                }
                else
                {
                    graphics.RasterizerState = rasterizerStateSolid;
                }
            }

            if (keyState.IsKeyDown(Keys.N) && !keyStateAnterior.IsKeyDown(Keys.N))
            {
                drawNormals = !drawNormals;
            }

            keyStateAnterior = keyState;
        }

        /// <summary>
        /// Atualiza a posição da camâra
        /// </summary>
        /// <param name="vectorToAdd"></param>
        static private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            position += moveSpeed * rotatedVector;
            UpdateViewMatrix();
        }

        /// <summary>
        /// Atualiza os parâmetros da camâra
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        /// <param name="graphics">Instância de graphicsDevice</param>
        static public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            ProcessInput(timeDifference, graphics);
        }


    }
}
