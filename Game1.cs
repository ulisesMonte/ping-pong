using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml.Linq;

namespace tp_pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private bool pause;
        private KeyboardState _previousKeyboardState;

        private Paleta paleta1;
        private Paleta paleta2;
        private Pelota ball;
        private Texture2D paletaTexture;
        private Texture2D ballTexture;
        private enum GameState
        {
            Playing,
            Paused
        }
        private GameState _currentGameState;

        SpriteFont fuente;
        Texture2D fondo;
        //Song musica;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();
            IsMouseVisible = true;
            pause = false;
            _previousKeyboardState = Keyboard.GetState();

        }

        protected override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

           
            // Crear texturas para las paletas y la pelota
            paletaTexture = new Texture2D(GraphicsDevice, 1, 1);
            paletaTexture.SetData(new[] { Color.White });

            ballTexture = new Texture2D(GraphicsDevice, 1, 1);
            ballTexture.SetData(new[] { Color.White });

            // Inicializar las paletas y la pelota
            fondo = Content.Load<Texture2D>("Canchita");
            fuente = Content.Load<SpriteFont>("File");
            Pelota.sonidoRebote = Content.Load<SoundEffect>("ball-dragon");
            Pelota.sonidoGol = Content.Load<SoundEffect>("gol-messi_");
            paleta1 = new Paleta(paletaTexture, new Vector2(50, _graphics.PreferredBackBufferHeight / 2 - 50));
            paleta2 = new Paleta(paletaTexture, new Vector2(_graphics.PreferredBackBufferWidth - 70, _graphics.PreferredBackBufferHeight / 2 - 50));
            ball = new Pelota(ballTexture, new Vector2(_graphics.PreferredBackBufferWidth / 2 - 10, _graphics.PreferredBackBufferHeight / 2 - 10));
            

            Pelota.spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentGameState = GameState.Playing;

        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Obtener el estado del teclado
            var keyboardState = Keyboard.GetState();

            switch (_currentGameState)
            {
                
                case GameState.Playing:
                    
                    if (keyboardState.IsKeyDown(Keys.P) && !_previousKeyboardState.IsKeyDown(Keys.P))
                    {
                        _currentGameState = GameState.Paused;
                    }
                    if (!pause)
                    {
                        // Actualizar las paletas
                        paleta1.Update(keyboardState, Keys.W, Keys.S, _graphics.PreferredBackBufferHeight);
                        paleta2.Update(keyboardState, Keys.Up, Keys.Down, _graphics.PreferredBackBufferHeight);


                        // Actualizar la pelota
                        ball.Update(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, paleta1, paleta2);


                        base.Update(gameTime);

                    }

                    break;
                case GameState.Paused:
                    // Mostrar mensaje de pausa
                    if (keyboardState.IsKeyDown(Keys.P) && !_previousKeyboardState.IsKeyDown(Keys.P))
                    {
                        _currentGameState = GameState.Playing;
                    }
                    break;
            }
                      
        }
       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Pelota.spriteBatch.Begin();
            _spriteBatch.Begin();
            switch (_currentGameState)
            {
                
                case GameState.Playing:
                    _spriteBatch.Draw(fondo, new Rectangle(0, 0, 1280, 720), Color.White);
                    paleta2.Draw(_spriteBatch);
                    paleta1.Draw(_spriteBatch);
                    ball.Draw(_spriteBatch);
                    Pelota.spriteBatch.DrawString(fuente, Pelota.player1_score.ToString(), new Vector2(500, 50), Color.White); //marcador jugador derecho
                    Pelota.spriteBatch.DrawString(fuente, Pelota.player2_score.ToString(), new Vector2(720, 50), Color.White); //marcador jugador derecho
                    break;
                case GameState.Paused:
                    _spriteBatch.Draw(fondo, new Rectangle(0, 0, 1280, 720), Color.White);
                    paleta2.Draw(_spriteBatch);
                    paleta1.Draw(_spriteBatch);
                    ball.Draw(_spriteBatch);
                    Pelota.spriteBatch.DrawString(fuente, Pelota.player1_score.ToString(), new Vector2(500, 50), Color.White); //marcador jugador derecho
                    Pelota.spriteBatch.DrawString(fuente, Pelota.player2_score.ToString(), new Vector2(720, 50), Color.White); //marcador jugador derecho
                    DrawPause();
                    break;
                
            }

            

            _spriteBatch.End();
            Pelota.spriteBatch.End();
            base.Draw(gameTime);
        }
        
        private void DrawPause()
        {
            // Dibujar pantalla de pausa
            _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), "Juego en Pausa, Presionar P", new Vector2(1000 / 2 - 150, 700 / 2), Color.Green);
        }


    }
}