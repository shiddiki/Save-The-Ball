using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using System.IO.IsolatedStorage;

namespace RapidBall
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch wood;
        KeyboardState oldState;

        //sound
        SoundEffect soundEngine1,soundEngine2;
        SoundEffectInstance soundEngineInstance1, soundEngineInstance2;
        SoundEffect soundHyperspaceActivation;
        float aspectRatio;
        Game1 myModel;

        public static string hiscr = "";
        public static int col = 0, aa = 0, time = 1, prev = 0,loseplay=0,scoreshi=0,changelava=1;
        public static int velR = 1, velL = 1,die=0;
        public static int wood1 = 0, wood2 = 2, wood3 = 0, wood4 = 0, wood5 = 0, wood6 = 0,wood7=0,wood8=0,wood9=0, ps=8,thcome=4;
        //for scoring
        public static int wtch = 0, notch = 1, score = 0, timemod = 50,thorn1,thorn2,thorn3,thtimemod=80,thtime=0,thnum=0,thcollide=0,timemodprev=0;
        public static int HighScore=0;
        public static float ws = (float)3.00;
        //pause
        public static int pauseON=0, pauseOFF=0,life;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //ontent = new ResourceContentManager(game.Services, Resource1.ResourceManager);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsFixedTimeStep = false;

            // TODO: Add your initialization logic here

            // open isolated storage, and load data from the savefile if it exists.
            #if WINDOWS_PHONE
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
            #else
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain())
            #endif
            {
                if (savegameStorage.FileExists("Save_The_Ball"))
                {
                    using (IsolatedStorageFileStream fs = savegameStorage.OpenFile("Save_The_Ball", System.IO.FileMode.Open))
                    {
                        if (fs != null)
                        {
                            // Reload the saved high-score data.
                            byte[] saveBytes = new byte[4];
                            int count = fs.Read(saveBytes, 0, 4);
                            if (count > 0)
                            {
                                HighScore = System.BitConverter.ToInt32(saveBytes, 0);
                            }
                        }
                    }
                }
            }

            base.Initialize();
            oldState = Keyboard.GetState();
        }


        
            
        // This is a texture we can render.
        Texture2D myTexture;
        Texture2D myTexture1;
        Texture2D myTexture2;
        Texture2D myTexture3;
        Texture2D myTexture4;
        Texture2D myTexture5;
        Texture2D myTexture6;
        Texture2D myTexture7;
        Texture2D myTexture8;
        Texture2D myTexture9;
        Texture2D background;
        Texture2D boundup1;
        Texture2D boundup2;
        Texture2D bounddw;
        Texture2D gmover1;
        Texture2D gmover2;
        Texture2D gmover3;

        SoundEffect drop;
        SoundEffect lose;
        // Set the coordinates to draw the sprite at.
        Vector2 spritePosition = new Vector2(0,40);
        Vector2 woodPos1 = new Vector2(0, 500);
        Vector2 woodPos2 = new Vector2(300, 500);
        Vector2 woodPos3 = new Vector2(554, 500);
        Vector2 woodPos4 = new Vector2(0, 500);
        Vector2 woodPos5 = new Vector2(300, 500);
        Vector2 woodPos6 = new Vector2(554, 500);
        Vector2 woodPos7 = new Vector2(0, 500);
        Vector2 woodPos8 = new Vector2(300, 500);
        Vector2 woodPos9 = new Vector2(554, 500);
        Vector2 buppos = new Vector2(0,0);
        
        Vector2 budos=new Vector2(0,470);

        Vector2 gmpos= Vector2.Zero;
        

        // Store some information about the sprite's motion.
        Vector2 spriteSpeed = new Vector2(1000.0f, 1000.0f);
        Vector2 woodSpeed = new Vector2(50.0f, 50.0f);
        SpriteFont Font1;
        String xp, yp;
        Vector2 scorePosition = new Vector2(30, 30);
        Vector2 lifepos = new Vector2(700, 30);

        //for background
        private Vector2 screenposb, originb, texturesizeb;
        private Texture2D mytextureb;
        private int screenheightb;
        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            mytextureb = backgroundTexture;
            screenheightb = device.Viewport.Height;
            int screenwidth = device.Viewport.Width;
            // Set the origin so that we're drawing from the 
            // center of the top edge.
            originb = new Vector2(mytextureb.Width / 2, 0);
            // Set the screen position to the center of the screen.
            screenposb = new Vector2(screenwidth / 2, screenheightb / 2*-1);
            // Offset to draw the second texture, when necessary.
            texturesizeb = new Vector2(0, mytextureb.Height);
        }


        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {


            // TODO: use this.Content to load your game content here
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            wood = new SpriteBatch(GraphicsDevice);
            myTexture = Content.Load<Texture2D>("gm");
            myTexture1 = Content.Load<Texture2D>("gm1");
            myTexture2 = Content.Load<Texture2D>("gm1_2");
            myTexture3 = Content.Load<Texture2D>("gm1_3");
            myTexture4 = Content.Load<Texture2D>("gm1");
            myTexture5 = Content.Load<Texture2D>("gm1_2");
            myTexture6 = Content.Load<Texture2D>("gm1_3");
            myTexture7 = Content.Load<Texture2D>("gm_5");
            myTexture8 = Content.Load<Texture2D>("gm_5");
            myTexture9 = Content.Load<Texture2D>("gm_5");
             background = Content.Load<Texture2D>("cloud");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Load(GraphicsDevice, background);

            boundup1 = Content.Load<Texture2D>("lava1");
            boundup2 = Content.Load<Texture2D>("lava2");
            //bounddw = Content.Load<Texture2D>("lava");

            gmover1=Content.Load<Texture2D>("gov1");
            gmover2 = Content.Load<Texture2D>("gov2");
            gmover3 = Content.Load<Texture2D>("gov3");
            Font1 = Content.Load<SpriteFont>("SpriteFont1");

            //sound
            drop = Content.Load<SoundEffect>("drop");
            lose = Content.Load<SoundEffect>("lose");
            soundEngine1 = Content.Load<SoundEffect>("drop");
            soundEngine2 = Content.Load<SoundEffect>("lose");
            soundEngineInstance1 = soundEngine1.CreateInstance();
            soundEngineInstance2 = soundEngine2.CreateInstance();
            soundHyperspaceActivation =
                Content.Load<SoundEffect>("drop");

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            hiscr = "   High Score: " + HighScore;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        public int collide()
        {

            
            for (int i = 1; i <= 9; i++)
            {
                Vector2 wodi = Vector2.Zero;

                if (wood1 == 2 && i == 1)
                    wodi = woodPos1;

                else if (wood2 == 2 && i == 2)
                    wodi = woodPos2;
                else if (wood3 == 2 && i == 3)
                    wodi = woodPos3;
                else if (wood4 == 2 && i == 4)
                    wodi = woodPos4;
                else if (wood5 == 2 && i == 5)
                    wodi = woodPos5;
                else if (wood6 == 2 && i == 6)
                    wodi = woodPos6;
                else if (wood7 == 2 && i == 7)
                    wodi = woodPos7;
                else if (wood8 == 2 && i == 8)
                    wodi = woodPos8;
                else if (wood9 == 2 && i == 9)
                    wodi = woodPos9;

                if(wodi!=Vector2.Zero)
                {
                float x1 = spritePosition.X;
                float y1 = spritePosition.Y;

                float x2 = (x1 + myTexture.Width);
                float y2 = y1 + myTexture.Height;
                float wx1, wx2;
                if (i > 6)
                {
                     wx1 = wodi.X+15;
                     wx2 = wx1 + myTexture7.Width-30;
                }
                else {
                     wx1 = wodi.X + 24;
                     wx2 = wx1 + myTexture1.Width - 48;
                
                }
                float wy1 = wodi.Y;

                if (i >= 7)
                    wy1 += 4;
                
                float wy2 = wy1;

                BoundingBox b1 = new BoundingBox(new Vector3(x1, y1, 0),
                    new Vector3(x2, y2, 0));
                BoundingBox b2 = new BoundingBox(new Vector3(wx1, wy1, 0),
                    new Vector3(wx2, wy2, 0));


                if (b1.Intersects(b2))
                {
                    
                    col = 1;
                    if (i >= 7)
                    {
                        thcollide = 1;
                        spritePosition.Y = wodi.Y;
                    }
                    else
                    spritePosition.Y = wodi.Y - 64;

                    

                    /////////////////////////////////////////////SCORING
                    if (notch == 1 && wtch == 0)
                    {
                        soundEngineInstance1.Play();
                       // score++;
                        wtch = 1;
                        notch = 0;


                       /* if (score % 2 == 0 && score != 0 && ws<=4)
                        {
                            ws += (float)0.5;
                            if(ps<9)
                            {
                                ps+=2;
                            }
                            timemod -= 10;
                            

                            if (ws > 4)
                                thcome = 2;
                        }*/
                    }
                    
                }

                else
                {
                    b2 = new BoundingBox(new Vector3(wodi.X, wodi.Y, 0),
                        new Vector3(wodi.X+myTexture1.Width, wodi.Y+4, 0));
                    b1 = new BoundingBox(new Vector3(x1, y1 + myTexture.Height/2, 0),
                    new Vector3(x2, y2, 0));

                    if (b1.Intersects(b2))
                        col = 2;
                    else
                        col = 0;
                }
                if (col != 0)
                    break;
            }

            }



            return col;

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here


             //for backgrnd
             float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
              //Update(elapsed * 100);
             
             // TODO: Add your game logic here.
             screenposb.Y -= elapsed*10;
             screenposb.Y = screenposb.Y % mytextureb.Height;
             

            //spritePosition=spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //task from keyboard
            UpdateInput();

            // Move the sprite by speed, scaled by elapsed time.
            if(die<=4 && pauseON==0)
            {
                float noball = spritePosition.X;
            if (wood1 == 2)
            {
                woodPos1.Y = woodPos1.Y - ws;
                if (woodPos1.Y < -40)
                {
                    wood1 = 0;
                    woodPos1 = new Vector2(0, 500);
                }

            }
            if (wood2 == 2)
            {
                woodPos2.Y = woodPos2.Y - ws;
                if (woodPos2.Y < -40)
                {
                    wood2 = 0;
                    woodPos2 = new Vector2(300, 500);
                }
            }
            if (wood3 == 2)
            {
                woodPos3.Y = woodPos3.Y - ws;
                if (woodPos3.Y < -40)
                {
                    wood3 = 0;
                    woodPos3 = new Vector2(550, 500);
                }
            }
            if (wood4 == 2)
            {
                woodPos4.Y = woodPos4.Y - ws;
                if (woodPos4.Y < -40)
                {
                    wood4 = 0;
                    woodPos4 = new Vector2(0, 500);
                }
            }
            if (wood5 == 2)
            {
                woodPos5.Y = woodPos5.Y - ws;
                if (woodPos5.Y < -40)
                {
                    wood5 = 0;
                    woodPos5 = new Vector2(300, 500);
                }
            }
            if (wood6 == 2)
            {
                woodPos6.Y = woodPos6.Y - ws;
                if (woodPos6.Y < -40)
                {
                    wood6 = 0;
                    woodPos6 = new Vector2(550, 500);
                }
            }
            if (wood7 == 2)
            {
                woodPos7.Y = woodPos7.Y - ws;
                if (woodPos7.Y < -40)
                {
                    wood7 = 0;
                    woodPos7 = new Vector2(550, 500);
                    thnum--;
                }
            }
            if (wood8 == 2)
            {
                woodPos8.Y = woodPos8.Y - ws;
                if (woodPos8.Y < -40)
                {
                    wood8 = 0;
                    woodPos8 = new Vector2(550, 500);
                    thnum--;
                }
            }
            if (wood9 == 2)
            {
                woodPos9.Y = woodPos9.Y - ws;
                if (woodPos9.Y < -40)
                {
                    wood9 = 0;
                    woodPos9 = new Vector2(550, 500);
                    thnum--;
                }
            }


             
            // woodPos.X = 300;

            //collision

            if (col == 1)
                //spritePosition.Y = woodPos1.Y-59;

                if (col == 2)
                    spritePosition.X = noball;

            int MaxX =
                graphics.GraphicsDevice.Viewport.Width - myTexture.Width;
            int MinX = 0;
            int MaxY =
                graphics.GraphicsDevice.Viewport.Height;// -myTexture.Height; //total lost of ball
            int MinY = 0;


            int woX = graphics.GraphicsDevice.Viewport.Width - myTexture.Width;
            int woY = graphics.GraphicsDevice.Viewport.Height - myTexture.Height;



            collide();

            // Check for bounce.
            if (col == 0)
            {
                notch = 1;
                wtch = 0;
                spritePosition.Y = spritePosition.Y + 4;

                spritePosition.X = noball;

               

                if (spritePosition.Y > MaxY + 20)
                {
                    spriteSpeed.Y *= +1;
                    spritePosition.Y = 0;
                }

                else if (spritePosition.Y < MinY)
                {
                    // spriteSpeed.Y *= -1;
                    spritePosition.Y = MinY;
                }
            }
            // else if (col == 1)
            //spritePosition.Y = woodPos1.Y - 61;
            else if (col == 2)
            {
                spritePosition.X = noball;
                spritePosition.Y = spritePosition.Y + 2;
                spritePosition.X = noball;
                velR = 0;
                velL = 0;
            }

            if (col == 0)
            {
                if (velR > 0)
                    spritePosition.X += 2;
                else if (velL > 0)
                    spritePosition.X -= 2;

                

            }
            if (spritePosition.X > MaxX)
            {
                // spriteSpeed.X *= -1;
                spritePosition.X = MaxX;
            }


            else if (spritePosition.X < MinX)
            {
                // spriteSpeed.X *= -1;
                spritePosition.X = MinX;
            }
            xp = spritePosition.X.ToString();
            yp = spritePosition.Y.ToString();

            if (spritePosition.Y <= boundup1.Height-4  || spritePosition.Y <= boundup2.Height-4  || spritePosition.Y >= 550 || thcollide == 1)
            {
                die++;

                
                Thread.Sleep(1000);

                restart();
                if(die==5)
                {
                    soundEngineInstance2.Play();
                    if (score > HighScore)
                    {
                        scoreshi = 1;
                        HighScore = score;
                    }
            }
        }
        }
            
             
            base.Update(gameTime);
        }

        public void restart()
        {
            spritePosition = new Vector2(0, 40);
            col = 0; aa = 0; time = 1; prev = 0;
            if (ws > 4)
                thcome = 2;
            else
            thcome = 4;

            velR = 1; velL = 1; //left corner speed or right?
            wood1 = 0; wood2 = 2; wood3 = 0; wood4 = 0; wood5 = 0; wood6 = 0; wood7 = 0; wood8 = 0; wood9 = 0; //which wood is on screen
            
            wtch = 0; notch = 1;//free all or touched
            thorn1 = 0; thorn2 = 0; thorn3 = 0;//which thorn on screen
            //thtimemod = 120; 
            thtime = 1; thnum = 0; thcollide = 0; //thorn variables
           // ws=(float)3.00;
           //timemod = 80;

            loseplay = 0;
            woodPos1 = new Vector2(0, 500);
            woodPos2 = new Vector2(300, 500);
            woodPos3 = new Vector2(550, 500);
            woodPos4 = new Vector2(0, 500);
            woodPos5 = new Vector2(300, 500);
            woodPos6 = new Vector2(550, 500);
            woodPos7 = new Vector2(0, 500);
            woodPos8 = new Vector2(300, 500);
            woodPos9 = new Vector2(550, 500);
        
        }
        
        /// <summary>
        /// 
        /// key board comtrol
        /// </summary>
        private void UpdateInput()
        {
            KeyboardState newState = Keyboard.GetState();
            Game1 cg;

            // Is the SPACE key down?
            if (newState.IsKeyDown(Keys.Right))
            {
                pauseON = 0;
                // If not down last update, key has just been pressed.

                /*if (!oldState.IsKeyDown(Keys.Space))
                {
                    backColor =
                        new Color(backColor.R, backColor.G, (byte)~backColor.B);
                }
                  */
                if (col == 1)
                    spritePosition.X = spritePosition.X + ps+1;
                else
                    spritePosition.X = spritePosition.X + ps;

                velR = 5;
                velL = 0;
            }
            else if (oldState.IsKeyDown(Keys.Left))
            {
                pauseON = 0;
                // Key was down last update, but not down now, so
                // it has just been released.
                if (col == 1)
                    spritePosition.X = spritePosition.X - (ps+1);
                else
                    spritePosition.X = spritePosition.X - ps;

                velR = 0;
                velL = 5;
            }
            else if (oldState.IsKeyDown(Keys.Enter))
            {
                
                if(pauseON==0)
                Thread.Sleep(1000);
                pauseON = 1;
            }

            

            if (die > 4)
            {
                if (newState.IsKeyDown(Keys.Enter))
                {
                    pauseON = 0;
                    restart();
                score = 0;
                die = 0;
                hiscr = "   High Score: " + HighScore;
                ws = (float)3.00;
                timemod = 50;
                thtimemod = 80;
                thcome = 4;
                ps = 8;
                time = 1;
                thtime = 1;
                thcome = 4;
                }
            }
            // Update saved state.
            oldState = newState;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

           // graphics.GraphicsDevice.Clear(Color.Black);

            
                // Draw the sprite.
                spriteBatch.Begin();
                 life = 5 - die;

                 Draw(spriteBatch);
                 spriteBatch.End();
                /* //draw bckgrnd
                 if (screenposb.Y < screenheightb)
                 {
                     spriteBatch.Draw(mytextureb, screenposb, null,
                          Color.White, 0, originb, 1, SpriteEffects.None, 0f);
                 }
                 // Draw the texture a second time, behind the first,
                 // to create the scrolling illusion.
                 spriteBatch.Draw(mytextureb, screenposb - texturesizeb, null,
                      Color.White, 0, originb, 1, SpriteEffects.None, 0f);
                 */
                 spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

                 spriteBatch.DrawString(Font1, "Score:" + score + hiscr, scorePosition, Color.Firebrick);
                 
                if (die <= 4)
                    {
                 //spriteBatch.Draw(bounddw, budos, Color.White);
                // spriteBatch.Draw(boundup, buppos, Color.White);
                 

                 spriteBatch.Draw(myTexture, spritePosition, Color.White);
                 //spriteBatch.Draw(myTexture, spritePosition, Color.White);
                // spriteBatch.Draw(myTexture, spritePosition, Color.White);
                
                    if ((int)gameTime.ElapsedGameTime.TotalSeconds % 2 == 0)
                {
                    
                        if(pauseON==0)
                        {
                            time++;
                        thtime++;
                        }
                    if (time == 131)
                        time = 1;
                    if (thtime == 1023)
                        thtime = 1;
                }

                    if ((wood1 + wood2 + wood3 + wood5 + wood6 + wood4 + wood7 + wood8 + wood9 < 10) && time >= timemod&&pauseON==0)
                {
                    time = 1;
                    Random rd = new Random();
                        

                    while (aa == prev)
                    {
                        if (thnum < 1 && thtime > thtimemod && (wood1 + wood2 + wood3 + wood5 + wood6 + wood4 > thcome))
                        {
                            aa = rd.Next(7, 10);
                                
                            //timemod-=10;
                            
                        }
                        else
                        {
                            aa = rd.Next(1, 7);
                            
                                
                            
                        }

                        if (prev % 3 == aa % 3&&(aa<7))
                            aa = prev;

                        if (aa == 1 && wood1 != 0)
                            aa = prev;
                        if (aa == 2 && wood2 != 0)
                            aa = prev;
                        if (aa == 3 && wood3 != 0)
                            aa = prev;
                        if (aa == 4 && wood4 != 0)
                            aa = prev;
                        if (aa == 5 && wood5 != 0)
                            aa = prev;
                        if (aa == 6 && wood6 != 0)
                            aa = prev;
                        if (aa == 7 && wood7 != 0)
                            aa = prev;
                        if (aa == 8 && wood8 != 0)
                            aa = prev;
                        if (aa == 9 && wood9 != 0)
                            aa = prev;


                    }
                   

                    if (aa > 6)
                    {
                        thnum++;
                        thtime = 1;
                    }



                    if (aa != prev)     ////////////////////////////////////////scoring
                    {

                        
                            
                            score++;
                            


                            if (score % 50 == 0 && score != 0 && ws <= 4.5)
                            {
                                ws += (float)0.5;
                                if (ps < 10)
                                {
                                    ps++;
                                }
                                timemod -= 3;
                                thtimemod -= 10;


                                if (ws > 4)
                                    thcome = 2;
                            }
                        
                    
                    }




                    prev = aa;
                    if (aa == 1)
                    {

                        wood1 = 2;
                    }
                    else if (aa == 2)
                    {

                        wood2 = 2;
                    }
                    else if (aa == 3)
                    {

                        wood3 = 2;
                    }
                    else if (aa == 4)
                    {

                        wood4 = 2;
                    }
                    else if (aa == 5)
                    {

                        wood5 = 2;

                    }
                    else if (aa == 6)
                    {

                        wood6 = 2;

                    }
                    else if (aa == 7)
                    {

                        wood7 = 2;

                    }
                    else if (aa == 8)
                    {

                        wood8 = 2;

                    }
                    else if (aa == 9)
                    {

                        wood9 = 2;

                    }

                }

                  

                //spriteBatch.Draw(myTexture, spritePosition, Color.White);
                //spriteBatch.Draw(myTexture, spritePosition, Color.White);
                //spriteBatch.Draw(myTexture, spritePosition, Color.White);
                    

                
                if (wood1 == 2)
                {
                    
                    spriteBatch.Draw(myTexture1, woodPos1, Color.White);

                }
                if (wood2 == 2)
                {
                    
                    spriteBatch.Draw(myTexture2, woodPos2, Color.White);

                }
                if (wood3 == 2)
                {
                    
                    spriteBatch.Draw(myTexture3, woodPos3, Color.White);

                }
                if (wood4 == 2)
                {
                    
                    spriteBatch.Draw(myTexture4, woodPos4, Color.White);

                }
                if (wood5 == 2)
                {
                    
                    spriteBatch.Draw(myTexture5, woodPos5, Color.White);

                }
                if (wood6 == 2)
                {
                    
                    spriteBatch.Draw(myTexture6, woodPos6, Color.White);

                }
                if (wood7 == 2)
                {
                    
                    spriteBatch.Draw(myTexture7, woodPos7, Color.White);

                }
                if (wood8 == 2)
                {
                    
                    spriteBatch.Draw(myTexture8, woodPos8, Color.White);

                }
                if (wood9 == 2)
                {
                    
                    spriteBatch.Draw(myTexture9, woodPos9, Color.White);

                }

               // spriteBatch.Draw(bounddw, budos, Color.White);
                if (time % 20 == 0)
                {
                    if(changelava==1)
                        changelava=2;
                    else
                        changelava=1;
                
                }
                    if(changelava==1)
                    spriteBatch.Draw(boundup1, buppos, Color.White);
                else 
                    spriteBatch.Draw(boundup2, buppos, Color.White);

                
               
          }
          else
          {
              if (scoreshi == 1)
              {
                  hiscr = " Congratulations!! Highest Score!!!";
                  scoreshi = 0;
              }
              pauseON = 1;
              
              spriteBatch.Draw(gmover1, gmpos, Color.White);
              

              spriteBatch.DrawString(Font1, "Score:" + score + hiscr, scorePosition, Color.Firebrick);
              spriteBatch.DrawString(Font1, "Score:" + score + hiscr, scorePosition, Color.Firebrick);
              spriteBatch.DrawString(Font1, "Score:" + score + hiscr, scorePosition, Color.Firebrick);
          }
          

                //

                spriteBatch.DrawString(Font1, "Life:" + life, lifepos, Color.Red);
        

                spriteBatch.End();




                base.Draw(gameTime);
                
            }

        public void Draw(SpriteBatch batch)
        {
            // Draw the texture, if it is still onscreen.
            if (screenposb.Y < screenheightb)
            {
                batch.Draw(mytextureb, screenposb, null,
                Color.White, 0, originb, 1, SpriteEffects.None, 0f);
            }
            // Draw the texture a second time, behind the first,
            // to create the scrolling illusion.
            batch.Draw(mytextureb, screenposb + texturesizeb, null,         //- when up to down
                 Color.White, 0, originb, 1, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// 
        /// saves high score
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnExiting(object sender, System.EventArgs args)
        {
            // Save the game state (in this case, the high score).
        #if WINDOWS_PHONE
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();
            
        #else
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain();
        #endif

            // open isolated storage, and write the savefile.
            IsolatedStorageFileStream fs = null;
            using (fs = savegameStorage.CreateFile("Save_The_Ball"))
            {
                if (fs != null)
                {
                    // just overwrite the existing info for this example.
                    byte[] bytes = System.BitConverter.GetBytes(HighScore);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            base.OnExiting(sender, args);
        }

        }
    }

