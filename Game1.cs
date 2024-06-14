using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace MyGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D Spikes;
        Texture2D Brick;
        Texture2D[] Baeume = new Texture2D[3];
        Texture2D[] PlayerRunFrames = new Texture2D[8];
        Texture2D[] PlayerIdleFrames = new Texture2D[8];
        Texture2D[] PlayerJumpFrames = new Texture2D[8];
        Texture2D[] PlayerRunLeftFrames = new Texture2D[8];
        AnimationManager _animationManagerRun;
        AnimationManager _animationManagerJump;
        AnimationManager _animationManagerIdle;
        internal KeyboardState keyboardState;
        internal KeyboardState previousState;
        int MappeCounter = 0; 
        Me player;
        bool inizio = true;
        internal List<char[,]> Charmappe;
       

        internal List <Ding[,]> Mappa;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            _graphics.PreferredBackBufferWidth = 2000;
            _graphics.PreferredBackBufferHeight = 1200;
            _graphics.ApplyChanges();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            previousState = Keyboard.GetState();



        }
        protected void LoadSprites(string rootPath, Texture2D[] texturearray)
        {

            for (int k = 0; k < texturearray.Length; k++)
            {
                string number;
                number = (k + 1).ToString();


                string t_pfad =rootPath + number;
                texturearray[k] = Content.Load<Texture2D>(t_pfad);

            }


        }

        protected override void LoadContent()
        {
            Mappa = new List<Ding[,]>();
            Map map = new Map();
            Charmappe = map.ReadMap();
            for (int k = 0; (k < Charmappe.Count); k++)
            {
                Mappa.Add(map.GenerateMap(Charmappe[k]));

            }
           
            Spikes = Content.Load<Texture2D>("Spikes");
            Brick = Content.Load<Texture2D>("VoidBrick");
           
            LoadSprites("Baum", Baeume);
            LoadSprites("SoldierRun", PlayerRunFrames);
            LoadSprites("Soldier/Soldier_1/Idle", PlayerIdleFrames);
            LoadSprites("Left", PlayerRunLeftFrames);
           

        

            for (int k = 0; k < PlayerJumpFrames.Length; k++)
            {



                string pfad2 = "SoldierRun1";
                PlayerJumpFrames[k] = Content.Load<Texture2D>(pfad2);

            }

            Parallel.For(0, Mappa.Count, m => {


                for (int i = 0; i < Mappa[m].GetLength(0); i++)
                {
                    for (int j = 0; j < Mappa[m].GetLength(1); j++)
                    {


                        if (Mappa[m][i, j] is SpikeBrick)
                        {
                            Mappa[m][i, j].texture = Spikes;
                        }


                        else if (Mappa[m][i, j] is Baum)
                        {
                            Texture2D[] baeumeTexture = new Texture2D[3];
                            Mappa[m][i, j].textureArray = new List<Texture2D[]>();
                            Mappa[m][i, j].textureArray.Add(baeumeTexture);


                            for (int b = 0; b < Baeume.Length; b++)
                            {
                                Mappa[m][i, j].textureArray[0][b] = Baeume[b];



                            }

                        }
                        else if (Mappa[m][i, j] is Me)
                        {
                            if (inizio)
                            {
                                player = new Me(new Point(Mappa[m][i, j].X, Mappa[m][i, j].Y));


                                player.Frame = 0;
                                inizio = false;

                            }
                            // Corri a destra
                            player.LoadSprites(PlayerRunFrames, PlayerRunLeftFrames, PlayerIdleFrames, PlayerJumpFrames);
                            _animationManagerRun = new AnimationManager(PlayerRunFrames);
                            ;
                        }
                        else if (Mappa[m][i, j] is GrassBrick) { Mappa[m][i, j].texture = Brick; }
                        else if (Mappa[m][i, j] is WaterBrick) { Mappa[m][i, j].texture = Brick; }
                        else if (Mappa[m][i, j] is GroundBrick) { Mappa[m][i, j].texture = Brick; }


                        else
                        {


                        }
                    }
                }
                });


                    // TODO: use this.Content to load your game content here
             
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
         
                player.ICollide(Mappa[MappeCounter]);
                keyboardState = Keyboard.GetState();
                _animationManagerRun.Update();
                player.UpdatePlayer(gameTime, keyboardState, previousState);
                previousState = keyboardState;
                if (player.X > 1900 && MappeCounter <= Mappa.Count - 1)
                {
                    player.X = 30;
                    MappeCounter++;

                }
                if (player.X < 0 && MappeCounter>0)
            {
                player.X = 1870 ; MappeCounter--;
            }
            

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred,samplerState: SamplerState.PointClamp );

            for (int i = 0; i < Mappa[MappeCounter].GetLength(0); i++)
            {
                for (int j = 0; j < Mappa[MappeCounter].GetLength(1); j++)
                {
                    if (Mappa[MappeCounter][i, j] is Baum baum)
                    {



                        _spriteBatch.Draw(texture: baum.texture, destinationRectangle: baum.BaumSpriets(), color: Mappa[MappeCounter][i, j].colore);


                    }


                    else if (Mappa[MappeCounter][i, j] is GrassBrick grassBrick)
                    {

                        _spriteBatch.Draw(texture: grassBrick.texture,
                                          position: new Vector2(grassBrick.X, grassBrick.Y),
                                          color: grassBrick.colore);
                        _spriteBatch.Draw(texture: grassBrick.texture,
                                          destinationRectangle: new Rectangle(grassBrick.X, grassBrick.Y, 30, 6),
                                          color: grassBrick.ColoreErba());
                    }
                    else if (Mappa[MappeCounter][i, j] is GroundBrick)
                    {
                        _spriteBatch.Draw(texture: Mappa[MappeCounter][i, j].texture, position: new Vector2(Mappa[MappeCounter][i, j].X, Mappa[MappeCounter][i, j].Y), color: Mappa[MappeCounter][i, j].colore);
                    }
                    else if (Mappa[MappeCounter][i, j] is SpikeBrick)
                    {
                        _spriteBatch.Draw(texture: Mappa[MappeCounter][i, j].texture, position: new Vector2(Mappa[MappeCounter][i, j].X, Mappa[MappeCounter][i, j].Y), color: Mappa[MappeCounter][i, j].colore);
                    }
                    else if (Mappa[MappeCounter][i, j] is WaterBrick)
                    {
                        _spriteBatch.Draw(texture: Mappa[MappeCounter][i, j].texture, position: new Vector2(Mappa[MappeCounter][i, j].X, Mappa[MappeCounter][i, j].Y), color: Mappa[MappeCounter][i, j].colore);
                    }

                

                    else
                    {


                    }

                }
            }
            player.Draw(_spriteBatch, _animationManagerRun.GetFrame());
            
                    _spriteBatch.End();
                    // TODO: Add your drawing code here

                    base.Draw(gameTime);
                }
            }
        }

