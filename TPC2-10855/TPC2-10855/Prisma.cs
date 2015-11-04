using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPC2_10855
{
    class Prisma
    {
        //Array de vértices
        private VertexPositionColorTexture[] vertexes;
        //Arrays de índices
        private short[] indices;
        private short[] indicesTopo;
        private short[] indicesBase;

        //Buffers
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer, indexBufferTopo, indexBufferBase;
        
        //World do objeto
        Matrix objectWorld;
    
        float rotacao;

        int nLados;

        public Prisma(GraphicsDevice graphics, Random random, Vector3 posicao, int nLados, float radius, float altura)
        {
            //Inicializar propriedades
            rotacao = 0;
            objectWorld = Matrix.Identity;

            this.nLados = nLados;
            //O tamanho do array de vertices é o número de lados * 2 + 2 (vértice do topo e da base) + 2 (vértices para enrolar a textura, sobrepostos ao 1º e 2º)
            vertexes = new VertexPositionColorTexture[nLados * 2 + 2 + 2];

            //Posição do vértice do topo
            Vector3 topo = new Vector3(posicao.X, posicao.Y + 1 * altura, posicao.Z);
            //Posição do vértice da base
            Vector3 baseVector = new Vector3(posicao.X, posicao.Y - 1 * altura, posicao.Z);

            //Guarda o ângulo de cada vértice
            float angulo = 0;
            //Diferença de ângulo para cada ponto
            float step = MathHelper.TwoPi / nLados;

            //Criar os vértices do lado prisma
            for (int i = 0; i < nLados; i++)
            {
                vertexes[2 * i] = new VertexPositionColorTexture(new Vector3(
                    posicao.X + (float)Math.Cos(angulo) * radius,
                    posicao.Y + 1 * altura,
                    posicao.Z + (float)-Math.Sin(angulo) * radius),
                    new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)),
                    new Vector2((float)i / nLados, 0));

                vertexes[2 * i + 1] = new VertexPositionColorTexture(new Vector3(
                    posicao.X + (float)Math.Cos(angulo) * radius,
                    posicao.Y - 1 * altura,
                    posicao.Z + (float)-Math.Sin(angulo) * radius),
                    new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)),
                    new Vector2((float)i / nLados, 1));
                //Incrementar o ângulo para o próximo ponto
                angulo += step;
            }
            //Vértices de topo e base
            vertexes[nLados * 2] = new VertexPositionColorTexture(topo, Color.Black, Vector2.Zero);
            vertexes[nLados * 2 + 1] = new VertexPositionColorTexture(baseVector, Color.Black, Vector2.Zero);

            //Vértices extras para enrolar a textura
            vertexes[nLados * 2 + 2] = new VertexPositionColorTexture(new Vector3(
                    posicao.X + (float)Math.Cos(angulo) * radius,
                    posicao.Y + 1 * altura,
                    posicao.Z + (float)-Math.Sin(angulo) * radius),
                    new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)),
                    new Vector2(1, 0));
            vertexes[nLados * 2 + 3] = new VertexPositionColorTexture(new Vector3(
                    posicao.X + (float)Math.Cos(angulo) * radius,
                    posicao.Y - 1 * altura,
                    posicao.Z + (float)-Math.Sin(angulo) * radius),
                    new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)),
                    new Vector2(1, 1));

            //Inicializar o array de índices dos lados
            indices = new short[2 * nLados + 2];

            //Gerar os índices dos lados
            for (int i = 0; i < nLados * 2; i++)
            {
                indices[i] = (short)i;
            }

            //indices extra para fechar a textura
            indices[2 * nLados] = (short)(nLados * 2 + 2);
            indices[2 * nLados + 1] = (short)(nLados * 2 + 3);

            //Array de indices do topo
            indicesTopo = new short[2 * nLados + 2];
            for (int i = 0; i < nLados + 1; i++)
            {
                indicesTopo[2 * i] = (short)(2 * i);
                indicesTopo[2 * i + 1] = (short)(2 * nLados);
            }
            indicesTopo[nLados * 2] = 0;
            indicesTopo[nLados * 2 + 1] = (short)(nLados * 2);

            //Array de indices do topo
            indicesBase = new short[2 * nLados + 2];
            for (int i = 0; i < nLados + 1; i++)
            {
                indicesBase[2 * i] = (short)(2 * i + 1);
                indicesBase[2 * i + 1] = (short)(2 * nLados + 1);
            }
            indicesBase[nLados * 2] = 1;
            indicesBase[nLados * 2 + 1] = (short)(nLados * 2 + 1);

            //Enviar os buffers para a memória de GPU
            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColorTexture), vertexes.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColorTexture>(vertexes);

            indexBuffer = new IndexBuffer(graphics, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData<short>(indices);

            indexBufferTopo = new IndexBuffer(graphics, typeof(short), indicesTopo.Length, BufferUsage.WriteOnly);
            indexBufferTopo.SetData<short>(indicesTopo);

            indexBufferBase = new IndexBuffer(graphics, typeof(short), indicesBase.Length, BufferUsage.WriteOnly);
            indexBufferBase.SetData<short>(indicesBase);

        }

        public void Update()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Up)){
                objectWorld *= Matrix.CreateTranslation(Vector3.Transform(Vector3.Forward / 10, Matrix.CreateRotationY(rotacao)));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                objectWorld *= Matrix.CreateTranslation(Vector3.Transform(Vector3.Backward / 10, Matrix.CreateRotationY(rotacao)));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotacao += 0.02f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotacao -= 0.02f;
            }
        }

        public void Draw(GraphicsDevice graphics, BasicEffect efeito)
        {

            //World, View, Projection
            efeito.World = Matrix.CreateRotationY(rotacao) * objectWorld;
            efeito.View = Camera.View;
            efeito.Projection = Camera.Projection;

            efeito.CurrentTechnique.Passes[0].Apply();

            //Definir os buffers a utilizar
            graphics.SetVertexBuffer(vertexBuffer);
            graphics.Indices = indexBuffer;

            //Desenhar as primitivas
            efeito.TextureEnabled = true;
            efeito.CurrentTechnique.Passes[0].Apply();
            graphics.DrawUserIndexedPrimitives(PrimitiveType.TriangleStrip, vertexes, 0, vertexes.Length, indices, 0, nLados * 2);

            efeito.TextureEnabled = false;
            efeito.CurrentTechnique.Passes[0].Apply();
            graphics.DrawUserIndexedPrimitives(PrimitiveType.TriangleStrip, vertexes, 0, vertexes.Length, indicesTopo, 0, indicesTopo.Length - 2);
            graphics.DrawUserIndexedPrimitives(PrimitiveType.TriangleStrip, vertexes, 0, vertexes.Length, indicesBase, 0, indicesBase.Length - 2);
           
        }
    }
}
