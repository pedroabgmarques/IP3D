using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Modelos3D___Cubo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        BasicEffect efeito;
        Cubo cubo;
        Create3DPlane plano;
        BasicEffect efeitoPlano;
        Texture2D texturaPlano;

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
            cubo = new Cubo(Content, "Cube");
            plano = new Create3DPlane(new Vector3(0, -0.51f, 0), 10, Vector3.Up);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            texturaPlano = Content.Load<Texture2D>("logo_ipca");

            efeito = new BasicEffect(GraphicsDevice);

            efeitoPlano = new BasicEffect(GraphicsDevice);
            efeitoPlano.TextureEnabled = true;
            efeitoPlano.EnableDefaultLighting();
            efeitoPlano.Texture = texturaPlano;
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

            Camera.Update(gameTime, GraphicsDevice);

            cubo.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            efeito.World = Matrix.Identity;
            efeito.View = Camera.View;
            efeito.Projection = Camera.Projection;

            plano.Draw(GraphicsDevice, efeitoPlano);
            
            cubo.Draw(efeito);

            base.Draw(gameTime);
        }
    }
}
