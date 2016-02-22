using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TPC4.Particulas;

namespace TPC4
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        BasicEffect efeitoPlano, efeitoBasico;
        Texture2D texturaPlano;
        Create3DPlane plano;
        Random random;
        SistemaParticulasChuva particulasChuva;
        SistemaParticulasExplosao particulasExplosao;
        KeyboardState kbStateAnterior;
        int tamanhoPlano;

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
            //Define o tamanho do plano, sendo que o raio da nuvem depende deste tamanho
            tamanhoPlano = 25;
            //Inicializar a camara
            Camera.Initialize(GraphicsDevice);
            //Seed random
            random = new Random();

            //Inicializar o sistema de partículas da chuva
            particulasChuva = new SistemaParticulasChuva(random, tamanhoPlano/2, 1000, 20);
            //Inicilizar o sistema de particulas das explosões
            particulasExplosao = new SistemaParticulasExplosao(800);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            //Inicializar plano
            texturaPlano = Content.Load<Texture2D>("logo_ipca");
            plano = new Create3DPlane(new Vector3(0, 0, 0), tamanhoPlano, Vector3.Up);

            //Efeitos
            efeitoPlano = new BasicEffect(GraphicsDevice);
            efeitoPlano.TextureEnabled = true;
            efeitoPlano.EnableDefaultLighting();
            efeitoPlano.Texture = texturaPlano;

            efeitoBasico = new BasicEffect(GraphicsDevice);
            efeitoBasico.VertexColorEnabled = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            //Destruir a textura do plano
            texturaPlano.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Permite sair do jogo
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Atualiza a camara
            Camera.Update(gameTime, GraphicsDevice);

            //Carregando no espaço, é criada uma explosão algures dentro dos limites do plano
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !kbStateAnterior.IsKeyDown(Keys.Space))
            {
                particulasExplosao.inserirExplosao(random, new Vector3(random.Next(-tamanhoPlano/2, tamanhoPlano/2), 
                    0,
                    random.Next(-tamanhoPlano / 2, tamanhoPlano / 2)));
            }

            //Atualizar sistemas de partículas
            particulasChuva.Update(random);
            particulasExplosao.Update(random, gameTime);

            //Guardar o estado anterior do teclado
            kbStateAnterior = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            //Desenhar o plano
            plano.Draw(GraphicsDevice, efeitoPlano);

            //Desenhar os sistemas de partículas
            particulasChuva.Draw(GraphicsDevice, efeitoBasico);
            particulasExplosao.Draw(GraphicsDevice, efeitoBasico);

            base.Draw(gameTime);
        }
    }
}
