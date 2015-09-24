using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Vertex
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random;

        //Lista de geometrias a desenhar
        Geometry geometria;
        List<Geometry> geometrias;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 700;
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

            //Inicializar a camara
            Camera.Initialize(GraphicsDevice);

            //Seed random
            random = new Random();

            //3DAxis
            Create3DAxis.Initialize(GraphicsDevice);

            geometrias = new List<Geometry>();

            for (int i = 0; i < 350; i++)
            {
                //Inicializara geometria
                geometria = new Geometry(GraphicsDevice, random);
                geometrias.Add(geometria);
            }
                

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

            //Carregar o efeito a utilizar para desenhar a geometria
            foreach (Geometry geometria in geometrias)
            {
                geometria.LoadContent(GraphicsDevice, Content);
            }
            

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

            //Atualizar camera
            Camera.Update(gameTime, GraphicsDevice);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Desenhar 3DAxis
            Create3DAxis.Draw(Camera.World, Camera.View, Camera.Projection, GraphicsDevice);

            //Desenhar a geometria
            foreach (Geometry geometria in geometrias)
            {
                geometria.Draw(Camera.World, Camera.View, Camera.Projection, GraphicsDevice);
            }
            
            base.Draw(gameTime);
        }
    }
}
