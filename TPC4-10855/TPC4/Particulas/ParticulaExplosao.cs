using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPC4.Particulas
{
    public class ParticulaExplosao
    {
        //Propriedades da particula
        public Vector3 posicao;
        float velocidadeMedia;
        float perturbacao;
        Vector3 direcao;
        private float totalTimePassed;

        //Array de vértices da particula
        private VertexPositionColor[] vertexes;

        public ParticulaExplosao(Vector3 posicao, float velocidadeMedia, float perturbacao, Random random)
        {
            //Inicializar o array de vértices, sendo que cada particula tem dois vértices
            vertexes = new VertexPositionColor[2];

            //Inicilizar propriedades
            this.posicao = posicao;
            this.velocidadeMedia = velocidadeMedia;
            this.perturbacao = perturbacao;

            //Criar os vértices da particula
            vertexes[0] = new VertexPositionColor(this.posicao, Color.Red);
            vertexes[1] = new VertexPositionColor(this.posicao - new Vector3(0, 0.01f, 0), Color.Yellow);

            //Calcular a direção da particula
            direcao = Vector3.Up;
            float angulo = (float)random.NextDouble() * MathHelper.TwoPi;
            direcao.X = (float)random.NextDouble() * (float)Math.Cos(angulo);
            direcao.Z = (float)random.NextDouble() * (float)Math.Sin(angulo);
            direcao.Normalize();
            direcao *= (float)random.NextDouble() * velocidadeMedia + perturbacao;
        }

        public void Update(Random random, GameTime gameTime)
        {
            //Atualizar a posição da particula
            posicao += direcao;
            totalTimePassed += (float)gameTime.ElapsedGameTime.Milliseconds / 4096.0f;
            posicao.Y -= totalTimePassed * totalTimePassed * velocidadeMedia * 7f; //Gravidade

            //Atualizar vértices
            vertexes[0].Position = posicao;
            vertexes[1].Position = posicao - new Vector3(0, 0.05f, 0);
        }

        public void Draw(GraphicsDevice graphics, BasicEffect efeito)
        {

            //World, View, Projection
            efeito.World = Camera.World;
            efeito.View = Camera.View;
            efeito.Projection = Camera.Projection;

            foreach (EffectPass pass in efeito.CurrentTechnique.Passes)
            {
                pass.Apply();

                //Desenhar as primitivas
                graphics.DrawUserPrimitives(PrimitiveType.LineList, vertexes, 0, 1);
            }
        }
    }
}
