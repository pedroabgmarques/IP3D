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

        //Efeito e textura das caixas
        BasicEffect efeito;
        Texture2D textura;

        //Efeito e textura dos planos
        BasicEffect efeitoPlano;
        Texture2D texturaPlano;

        //Efeito da piramide
        BasicEffect efeitoPiramide;

        //Lista de geometrias a desenhar
        Geometry geometria;
        List<Geometry> geometrias;

        List<Create3DPlane> listaPlanos3D;

        TringleStripPlane triangleStrip;

        VariableVertexes variableVertexes;

        Piramide piramide;

        PyramidIndices pyramidIndices;

        PyramidBuffers pyramidBuffers;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
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

            listaPlanos3D = new List<Create3DPlane>();

            triangleStrip = new TringleStripPlane(Vector3.Zero, 5, Vector3.Left);

            variableVertexes = new VariableVertexes(Vector3.Zero, 1, 5);

            pyramidIndices = new PyramidIndices();

            pyramidBuffers = new PyramidBuffers(GraphicsDevice, random, 8);

            geometrias = new List<Geometry>();

            for (int i = 0; i < 10000; i++)
            {
                //Inicializar a geometria
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

            efeito = new BasicEffect(GraphicsDevice);
            textura = Content.Load<Texture2D>("box_texture");

            efeitoPlano = new BasicEffect(GraphicsDevice);
            texturaPlano = Content.Load<Texture2D>("logo_ipca");

            efeitoPiramide = new BasicEffect(GraphicsDevice);

            //Textura
            efeito.TextureEnabled = true;
            efeito.Texture = textura;

            //Iluminação
            //efeito.EnableDefaultLighting();
            efeito.LightingEnabled = true;
            efeito.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
            efeito.DirectionalLight0.DiffuseColor = Color.White.ToVector3();
            efeito.DirectionalLight0.Direction = Vector3.Forward;
            efeito.DirectionalLight0.Enabled = true;
            efeito.PreferPerPixelLighting = true;
            //efeito.EmissiveColor = Color.White.ToVector3() * 0.1f;
            efeito.SpecularColor = Color.Black.ToVector3();

            //Fog
            efeito.FogEnabled = true;
            efeito.FogColor = Color.Black.ToVector3();
            efeito.FogStart = Camera.nearPlane;
            efeito.FogEnd = Camera.farPlaneShort - 10;

            //----------

            //Textura
            efeitoPlano.TextureEnabled = true;
            efeitoPlano.Texture = texturaPlano;

            //Iluminação
            //efeito.EnableDefaultLighting();
            efeitoPlano.LightingEnabled = true;
            efeitoPlano.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);
            efeitoPlano.DirectionalLight0.DiffuseColor = Color.White.ToVector3();
            efeitoPlano.DirectionalLight0.Direction = Vector3.Forward;
            efeitoPlano.DirectionalLight0.Enabled = true;
            efeitoPlano.PreferPerPixelLighting = true;

            //Fog
            efeitoPlano.FogEnabled = true;
            efeitoPlano.FogColor = Vector3.Zero;
            efeitoPlano.FogStart = Camera.nearPlane;
            efeitoPlano.FogEnd = Camera.farPlaneLong;

            efeitoPlano.World = Matrix.Identity;

            //--------
            efeitoPiramide.TextureEnabled = false;
            efeitoPiramide.LightingEnabled = false;
            efeitoPiramide.VertexColorEnabled = true;

            listaPlanos3D.Add(new Create3DPlane(new Vector3(0, 0, -Camera.worldSize / 2), Camera.worldSize / 2, Vector3.Backward));

            triangleStrip = new TringleStripPlane(new Vector3(0, 0, -50), 5, Vector3.Backward);

            piramide = new Piramide(360);
            
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

            efeito.View = Camera.View;
            efeito.Projection = Camera.Projection;

            //Desenhar 3DAxis
            Create3DAxis.Draw(Camera.World, Camera.View, Camera.Projection, GraphicsDevice, Matrix.Identity);

            //Desenhar a geometria (visíveis do ponto de vista da camera)
            foreach (Geometry geometria in geometrias.FindAll(m => Camera.frustum.Contains(m.boundingSphere) != ContainmentType.Disjoint))
            {
                geometria.Draw(Camera.World, Camera.View, Camera.Projection, GraphicsDevice, ref efeito, random);
                Create3DAxis.Draw(Camera.World, Camera.View, Camera.Projection, GraphicsDevice, efeito.World);
            }

            foreach (Create3DPlane plano in listaPlanos3D)
            {
                plano.Draw(Camera.World, Camera.View, Camera.LongProjection(GraphicsDevice), GraphicsDevice, efeitoPlano);
            }

            triangleStrip.Draw(Camera.World, Camera.View, Camera.LongProjection(GraphicsDevice), GraphicsDevice, efeitoPlano);

            //piramide.Draw(Camera.World, Camera.View, Camera.Projection, GraphicsDevice, efeitoPiramide);

            //variableVertexes.Draw(Camera.World, Camera.View, Camera.Projection, GraphicsDevice, efeitoPiramide);

            //pyramidIndices.Draw(Camera.World, Camera.View, Camera.Projection, GraphicsDevice, efeitoPiramide);
            
            pyramidBuffers.Draw(Camera.World, Camera.View, Camera.Projection, GraphicsDevice, efeitoPiramide);
            
            base.Draw(gameTime);
        }
    }
}
