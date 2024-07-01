using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;

namespace tp_pong
{
    public class Pelota
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public int puntos1 = 0;
        public int puntos2 = 0; //contador goles

        public static SoundEffect sonidoRebote;
        public static SoundEffect sonidoGol;

        public static SpriteBatch spriteBatch;
        public static int player1_score, player2_score;

        public int Size { get; } = 20; //tamaño
        public Color Color { get; set; } = Color.White;

        private Texture2D _texture;

        public Pelota(Texture2D texture, Vector2 position)
        {
            _texture = texture; // Inicialización de la textura
            Position = position;
            Velocity = new Vector2(8, 8); // Velocidad inicial de la pelota
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, Size, Size), Color);
        }

        public void Update(int screenWidth, int screenHeight, Paleta paddle1, Paleta paddle2)
        {
            Position += Velocity;

            // Rebote en los bordes superior e inferior
            if (Position.Y <= 0 || Position.Y + Size >= screenHeight) //altura de la pantalla
            {
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
                sonidoRebote.CreateInstance().Play();
            }

            // Colisión con las paletas
            if (CheckCollision(paddle1) || CheckCollision(paddle2))
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
               
            }

            // Salida por los bordes izquierdo 
            if (Position.X <= 0)
            {
                sonidoGol.CreateInstance().Play();
                player2_score += 1; //gol lado derecho
                // Resetear posición
                Position = new Vector2(screenWidth / 2 - Size / 2, screenHeight / 2 - Size / 2);
                // Velocidad próximo movimiento
                Velocity = new Vector2(8, 8);
            }
            // Salida por los bordes derecho
            if (Position.X + Size >= screenWidth)
            {
                sonidoGol.CreateInstance().Play();
                // Puntuación            
                player1_score += 1; //gol lado izq
                // Resetear posición
                Position = new Vector2(screenWidth / 2 - Size / 2, screenHeight / 2 - Size / 2);
                // Velocidad próximo movimiento
                Velocity = new Vector2(8 * (Position.X <= 0 ? 1 : -1), 8);
            }
        }
      
        private bool CheckCollision(Paleta paddle)
        {
            return Position.X < paddle.Position.X + paddle.Width &&
                   Position.X + Size > paddle.Position.X &&
                   Position.Y < paddle.Position.Y + paddle.Height &&
                   Position.Y + Size > paddle.Position.Y;
        }
    }
}