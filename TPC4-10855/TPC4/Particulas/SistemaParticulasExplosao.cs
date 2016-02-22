using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPC4.Particulas
{
    public class SistemaParticulasExplosao
    {
        //Porpriedades do sistema
        //Lista de particulas
        List<ParticulaExplosao> particulas;
        int nParticulas;

        public SistemaParticulasExplosao(int nParticulas)
        {
            //Inicializar propriedades
            this.nParticulas = nParticulas;
            particulas = new List<ParticulaExplosao>();
        }

        public void inserirExplosao(Random random, Vector3 posicao)
        {
            //Criar um determinado numero de particulas na posição em que se dá a explosão
            float velocidadeMedia = 0.07f;
            float perturbacao = 0.05f;

            for (int i = 0; i < nParticulas; i++)
            {
                particulas.Add(new ParticulaExplosao(posicao, velocidadeMedia, perturbacao, random));
            }
            
        }

        public void Update(Random random, GameTime gameTime)
        {
            //Atualizar as particulas deste sistema
            foreach (ParticulaExplosao particula in particulas)
            {
                particula.Update(random, gameTime);
            }

            //Verificar as particulas que devem morrer
            matarParticulas(random);
        }

        private void matarParticulas(Random random)
        {
            //Encontrar todas as particulas que passaram o nível do plano
            List<ParticulaExplosao> listaRemover = particulas.FindAll(x => x.posicao.Y < 0);
            
            //Remover as particulas que devem morrer
            foreach (ParticulaExplosao particula in listaRemover)
            {
                particulas.Remove(particula);
            }
        }

        public void Draw(GraphicsDevice graphics, BasicEffect efeito)
        {
            //Desenhar as particulas deste sistema 
            foreach (ParticulaExplosao particula in particulas)
            {
                particula.Draw(graphics, efeito);
            }
        }
    }
}
