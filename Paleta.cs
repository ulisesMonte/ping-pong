using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tp_pong
{
    public class Paleta
    {
        public Vector2 Position { get; set; }
        public int Width { get; } = 20; //ancho
        public int Height { get; } = 100; //largo
        public Color Color { get; set; } = Color.White;

        private Texture2D _texture;


        public Paleta(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color);
        }

        public void Update(KeyboardState keyboardState, Keys upKey, Keys downKey, int screenHeight)
        {
            if (keyboardState.IsKeyDown(upKey))
            {
                Position = new Vector2(Position.X, Position.Y - 5);
            }

            if (keyboardState.IsKeyDown(downKey))
            {
                Position = new Vector2(Position.X, Position.Y + 5);
            }

            // la paleta no se salga de los límites de la pantalla
            if (Position.Y < 0)
            {
                Position = new Vector2(Position.X, 0);
            }

            if (Position.Y + Height > screenHeight)
            {
                Position = new Vector2(Position.X, screenHeight - Height);
            }
        }
    }
}