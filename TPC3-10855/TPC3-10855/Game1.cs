using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TPC3_10855
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Prisma prisma;
        Random random;
        BasicEffect efeitoPrisma;
        BasicEffect efeito3DAxis;
        BasicEffect efeitoPlano;
        Texture2D texturaPrisma;
        Texture2D texturaPlano;
        Create3DPlane plano;

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
            Camera.Initialize(GraphicsDevice);
            Create3DAxis.Initialize(GraphicsDevice);
            DebugShapeRenderer.Initialize(GraphicsDevice);
            random = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            texturaPrisma = Content.Load<Texture2D>("kerbal_space_program");
            texturaPlano = Content.Load<Texture2D>("logo_ipca");

            efeito3DAxis = new BasicEffect(GraphicsDevice);
            efeito3DAxis.VertexColorEnabled = true;

            efeitoPrisma = new BasicEffect(GraphicsDevice);
            efeitoPrisma.TextureEnabled = true;
            efeitoPrisma.LightingEnabled = true;
            efeitoPrisma.EnableDefaultLighting();
            efeitoPrisma.Texture = texturaPrisma;

            efeitoPlano = new BasicEffect(GraphicsDevice);
            efeitoPlano.TextureEnabled = true;
            efeitoPlano.EnableDefaultLighting();
            efeitoPlano.Texture = texturaPlano;

            //Configurar aqui os parametros do prima e do plano
            int nLados = 8;
            int radius = 1;
            int altura = 2;
            int dimensaoPlano = 20;

            prisma = new Prisma(GraphicsDevice, random, Vector3.Zero, nLados, radius, altura);
            plano = new Create3DPlane(new Vector3(0, -altura - 0.001f, 0), dimensaoPlano, Vector3.Up);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            texturaPlano.Dispose();
            texturaPrisma.Dispose();
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

            Camera.Update(gameTime, GraphicsDevice);

            prisma.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Create3DAxis.Draw(GraphicsDevice, efeito3DAxis);

            prisma.Draw(GraphicsDevice, efeitoPrisma);

            plano.Draw(GraphicsDevice, efeitoPlano);

            DebugShapeRenderer.Draw(gameTime, Camera.View, Camera.Projection);

            base.Draw(gameTime);
        }
    }
}
