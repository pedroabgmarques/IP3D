using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vertex
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Array de VertexPositionColor para guardar a lista de vértices e respetivas cores
        VertexPositionColor[] vertexList;

        //Necessário para desenhar as primitivas
        VertexBuffer vertexBuffer;

        //Matrizes World, View e Projection
        Matrix World, View, Projection;

        //Shader utilizado na renderização
        BasicEffect basicEffect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //3 vértices, 1 triângulo
            vertexList = new VertexPositionColor[3];

            //Inicializar as matrizes world, view e projection
            World = Matrix.CreateTranslation(0, 0, 0);
            View = Matrix.CreateLookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 
                GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height, 
                0.01f, 
                100f);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Criar um basicEffect que vamos usar para renderizar o triângulo
            basicEffect = new BasicEffect(GraphicsDevice);

            vertexList[0] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Red);
            vertexList[1] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Green);
            vertexList[2] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Blue);

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertexList.GetLength(0), BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertexList);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Desenhar o triângulo
            basicEffect.World = World;
            basicEffect.View = View;
            basicEffect.Projection = Projection;
            basicEffect.VertexColorEnabled = true;

            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
            }

            base.Draw(gameTime);
        }
    }
}
