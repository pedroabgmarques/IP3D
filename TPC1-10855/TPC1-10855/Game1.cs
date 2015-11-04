﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TPC1_10855
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        //Seed para numeros aleatórios
        Random random;

        //Efeito a utilizar para desenhar a geometria
        private BasicEffect efeito;

        //Lista de polígonos a desenhar
        List<Poligono> poligonos;

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

            //Inicializa o efeito
            efeito = new BasicEffect(GraphicsDevice);

            //Inicializa o gerador de números aleatórios
            random = new Random();

            //Inicializa a câmara
            Camera.Initialize(GraphicsDevice);

            //Gera a geometria do eixo 3D
            Create3DAxis.Initialize(GraphicsDevice);

            //Inicializa a lista de polígonos
            poligonos = new List<Poligono>();

            int contadorH = 0, contadorV = 0;
            //Gerar os polígonos e inseri-los numa lista
            for (int i = 3; i < 360; i++)
            {
                //if (360 % i == 0)
                {
                    Poligono pol = new Poligono(GraphicsDevice, random, i, new Vector3(-6 + (contadorH * 3), 0, -6 + (contadorV * 3)));
                    poligonos.Add(pol);
                    contadorH++;
                    if (contadorH >= 5)
                    {
                        contadorH = 0;
                        contadorV++;
                    }
                }
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Desenhar o eixo 3D
            Create3DAxis.Draw(GraphicsDevice, efeito);

            //Desenhar os polígonos
            foreach (Poligono poligono in poligonos)
            {
                poligono.Draw(GraphicsDevice, efeito);
            }
            

            base.Draw(gameTime);
        }
    }
}
