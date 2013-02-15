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

/*
 * To-Do
 * -Add Tutorial for Equivalent Fractions
 * -Add Audio and Instructions
 * -Acquire Art Assets
 * -Add Difficulty levels
 * -Format GUI nicely
 * -Add sound to game
 * -Add another game or add more features
 * -Debug and Test
 * -Comment and Format Code
 */
namespace TestGame
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        //Sprite Batches
        SpriteBatch spriteBatch;
        SpriteBatch spriteBatchMainMenu;

        //Random Generator
        Random gen = new Random();

        //Textures
        private Texture2D background;
        private Texture2D shuttle;
        private Texture2D shuttle2;
        private Texture2D shuttle3;
        private Texture2D shuttle4;
        private Texture2D button;

        //Font
        private SpriteFont font;

        //Screen (0 = Main Menu, 1 = Game)
        private int Screennum = 0;

        //Main Menu
            //Button 1 pos
            private int MMButtonPos1x = 300;
            private int MMButtonPos1y = 40;
                       
            //Button 2 Pos
             private int MMButtonPos2x = 300;
             private int MMButtonPos2y = 340;
            
            //Exit Button Pos
             private int MMButtonPos3x = 490;
             private int MMButtonPos3y = 370;
            
            //Exit Text
             private int ExitPosx = 500;
             private int ExitPosy = 400;

            //Button Text
             private int ButText1x = 300;
             private int ButText1y = 50;
             private int ButText2x = 300;
             private int ButText2y = 360;

        //Width and Length of button picture
        private int Bwidth = 100;
        private int Blength = 100;

        //Score
        private int Score = -1;

        //button with correct answer
        private int AnsNum = 1;

        //Change Problems
        private bool changeProbs = true;

        //Multipler for question
        private int Qmul = 1;

        //Numerator and Denomenator of the question 
        private int Q1n = 0;
        private int Q1d = 0;

        //Numerator and Denomenator of the text on the buttons
        private int A1n = 0;
        private int A1d = 0;
        private int A2n = 0;
        private int A2d = 0;
        private int A3n = 0;
        private int A3d = 0;
        private int A4n = 0;
        private int A4d = 0;
        private int A5n = 0;
        private int A5d = 0;
        private int A6n = 0;
        private int A6d = 0;
        private int A7n = 0;
        private int A7d = 0;
        private int A8n = 0;
        private int A8d = 0;

        //X and Y Positions for Button with Correct Answer
        private int Ax = 0;
        private int Ay = 0;

        //X and Y Positions for Button
        private int ButtonPos1x = 50;
        private int ButtonPos1y = 280;
        private int ButtonPos2x = 150;
        private int ButtonPos2y = 280;
        private int ButtonPos3x = 250;
        private int ButtonPos3y = 280;
        private int ButtonPos4x = 350;
        private int ButtonPos4y = 280;
        private int ButtonPos5x = 50;
        private int ButtonPos5y = 380;
        private int ButtonPos6x = 150;
        private int ButtonPos6y = 380;
        private int ButtonPos7x = 250;
        private int ButtonPos7y = 380;
        private int ButtonPos8x = 350;
        private int ButtonPos8y = 380;

        //X and Y Positions for Text
        private int AnsTextPos1x = 75;
        private int AnsTextPos1y = 310;
        private int AnsTextPos2x = 175;
        private int AnsTextPos2y = 310;
        private int AnsTextPos3x = 275;
        private int AnsTextPos3y = 310;
        private int AnsTextPos4x = 375;
        private int AnsTextPos4y = 310;
        private int AnsTextPos5x = 75;
        private int AnsTextPos5y = 410;
        private int AnsTextPos6x = 175;
        private int AnsTextPos6y = 410;
        private int AnsTextPos7x = 275;
        private int AnsTextPos7y = 410;
        private int AnsTextPos8x = 375;
        private int AnsTextPos8y = 410;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Allows Mouse to be Visible
            this.IsMouseVisible = true;

            //Set the height and width of the screen
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
        }

        //Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }

        //Load Content
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchMainMenu = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background");
            shuttle = Content.Load<Texture2D>("shuttle");
            shuttle2 = Content.Load<Texture2D>("shuttle2");
            shuttle3 = Content.Load<Texture2D>("shuttle3");
            shuttle4 = Content.Load<Texture2D>("shuttle4");
            button = Content.Load<Texture2D>("Button");

            font = Content.Load<SpriteFont>("Font");
        }

        //Unload
        protected override void UnloadContent()
        {
           
        }

        //Update
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Gets Mouse State
            MouseState mouseState = Mouse.GetState();
            
            //Main Menu
            if (Screennum == 0)
            {
                if (((mouseState.X > MMButtonPos2x) && (mouseState.X < MMButtonPos2x + Blength)) && ((mouseState.Y > MMButtonPos2y) && (mouseState.Y < MMButtonPos2y + Bwidth)) && (mouseState.LeftButton == ButtonState.Pressed))
                {
                    Screennum = 1;
                    //Used to prevent multiple clicking and spamming score
                    Thread.Sleep(50);
                }
            }

            //Game Screen
            if (Screennum == 1)
            {
                //Changes the shuttle picture based on score
                if (Score == 3)
                    shuttle = shuttle2;
                if (Score == 7)
                    shuttle = shuttle3;
                if (Score == 10)
                    shuttle = shuttle4;

                //Changes the question
                if (changeProbs)
                {
                    //Adds 1 to Score
                    Score += 1;

                    //Generate which Answer button will hold the answer
                    AnsNum = gen.Next(8) + 1;

                    //Generates the Question
                    Q1n = gen.Next(30) + 1;
                    Q1d = gen.Next(30) + 1;

                    //Generates the multiplier for how the question and answer relates
                    Qmul = gen.Next(5) + 1;

                    //Generates all 8 answers, and the correct answer
                    if (AnsNum == 1)
                    {
                        A1n = (Q1n * Qmul);
                        A1d = (Q1d * Qmul);
                        Ax = ButtonPos1x;
                        Ay = ButtonPos1y;
                    }
                    else
                    {
                        A1n = gen.Next(99) + 1;
                        A1d = gen.Next(99) + 1;
                    }
                    if (AnsNum == 2)
                    {
                        A2n = (Q1n * Qmul);
                        A2d = (Q1d * Qmul);
                        Ax = ButtonPos2x;
                        Ay = ButtonPos2y;
                    }
                    else
                    {
                        A2n = gen.Next(99) + 1;
                        A2d = gen.Next(99) + 1;
                    }
                    if (AnsNum == 3)
                    {
                        A3n = (Q1n * Qmul);
                        A3d = (Q1d * Qmul);
                        Ax = ButtonPos3x;
                        Ay = ButtonPos3y;
                    }
                    else
                    {
                        A3n = gen.Next(99) + 1;
                        A3d = gen.Next(99) + 1;
                    }
                    if (AnsNum == 4)
                    {
                        A4n = (Q1n * Qmul);
                        A4d = (Q1d * Qmul);
                        Ax = ButtonPos4x;
                        Ay = ButtonPos4y;
                    }
                    else
                    {
                        A4n = gen.Next(99) + 1;
                        A4d = gen.Next(99) + 1;
                    }
                    if (AnsNum == 5)
                    {
                        A5n = (Q1n * Qmul);
                        A5d = (Q1d * Qmul);
                        Ax = ButtonPos5x;
                        Ay = ButtonPos5y;
                    }
                    else
                    {
                        A5n = gen.Next(99) + 1;
                        A5d = gen.Next(99) + 1;
                    }
                    if (AnsNum == 6)
                    {
                        A6n = (Q1n * Qmul);
                        A6d = (Q1d * Qmul);
                        Ax = ButtonPos6x;
                        Ay = ButtonPos6y;
                    }
                    else
                    {
                        A6n = gen.Next(99) + 1;
                        A6d = gen.Next(99) + 1;
                    }
                    if (AnsNum == 7)
                    {
                        A7n = (Q1n * Qmul);
                        A7d = (Q1d * Qmul);
                        Ax = ButtonPos7x;
                        Ay = ButtonPos7y;
                    }
                    else
                    {
                        A7n = gen.Next(99) + 1;
                        A7d = gen.Next(99) + 1;
                    }
                    if (AnsNum == 8)
                    {
                        A8n = (Q1n * Qmul);
                        A8d = (Q1d * Qmul);
                        Ax = ButtonPos8x;
                        Ay = ButtonPos8y;
                    }
                    else
                    {
                        A8n = gen.Next(99) + 1;
                        A8d = gen.Next(99) + 1;
                    }
                    changeProbs = false;
                }

                //Mouse Click
                if (((mouseState.X > Ax) && (mouseState.X < Ax + Blength)) && ((mouseState.Y > Ay) && (mouseState.Y < Ay + Bwidth)) && (mouseState.LeftButton == ButtonState.Pressed))
                {
                    changeProbs = true;
                    //Used to prevent multiple clicking and spamming score
                    Thread.Sleep(50);
                }
                if (mouseState.LeftButton == ButtonState.Pressed && !((mouseState.X > Ax) && (mouseState.X < Ax + Blength)) && ((mouseState.Y > Ay) && (mouseState.Y < Ay + Bwidth)))
                    changeProbs = false;
            }
            //Updates Game
            base.Update(gameTime);
        }

        //Draw Method                                               
        protected override void Draw(GameTime gameTime)
        {
            //Default Background  
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (Screennum == 0)
            {
                //Main Menu
                spriteBatchMainMenu.Begin();
                    //background
                    spriteBatchMainMenu.Draw(background, new Rectangle(0, 0, 640, 480), Color.White);

                    //Buttons
                    spriteBatchMainMenu.Draw(button, new Vector2(MMButtonPos1x, MMButtonPos1y), Color.White);
                    spriteBatchMainMenu.Draw(button, new Vector2(MMButtonPos2x, MMButtonPos2y), Color.White);

                    //Button 1 Text
                    spriteBatchMainMenu.DrawString(font, "Tutorial", new Vector2(ButText1x, ButText1y), Color.Black);

                    //Button 2 Text
                    spriteBatchMainMenu.DrawString(font, "Game", new Vector2(ButText2x, ButText2y), Color.Black);

                    //Exit
                    spriteBatchMainMenu.Draw(button, new Vector2(MMButtonPos3x, MMButtonPos3y), Color.White);

                    //Exit Text
                    spriteBatchMainMenu.DrawString(font, "Exit", new Vector2(ExitPosx, ExitPosy), Color.Black);


                spriteBatchMainMenu.End();
            }

            if (Screennum == 1)
            {
                // Sprite Batch for Game
                spriteBatch.Begin();

                //background
                spriteBatch.Draw(background, new Rectangle(0, 0, 640, 480), Color.White);

                //Shuttle
                spriteBatch.Draw(shuttle, new Vector2(25, 20), Color.White);

                //Question Button
                spriteBatch.Draw(button, new Vector2(450, 20), Color.White);

                //Queston Text
                spriteBatch.DrawString(font, Q1n + "/" + Q1d, new Vector2(475, 50), Color.Black);

                //Answer Buttons
                spriteBatch.Draw(button, new Vector2(ButtonPos1x, ButtonPos1y), Color.White);
                spriteBatch.Draw(button, new Vector2(ButtonPos2x, ButtonPos2y), Color.White);
                spriteBatch.Draw(button, new Vector2(ButtonPos3x, ButtonPos3y), Color.White);
                spriteBatch.Draw(button, new Vector2(ButtonPos4x, ButtonPos4y), Color.White);
                spriteBatch.Draw(button, new Vector2(ButtonPos5x, ButtonPos5y), Color.White);
                spriteBatch.Draw(button, new Vector2(ButtonPos6x, ButtonPos6y), Color.White);
                spriteBatch.Draw(button, new Vector2(ButtonPos7x, ButtonPos7y), Color.White);
                spriteBatch.Draw(button, new Vector2(ButtonPos8x, ButtonPos8y), Color.White);

                //Draws  Answer Text
                spriteBatch.DrawString(font, A1n + "/" + A1d, new Vector2(AnsTextPos1x, AnsTextPos1y), Color.Black);
                spriteBatch.DrawString(font, A2n + "/" + A2d, new Vector2(AnsTextPos2x, AnsTextPos2y), Color.Black);
                spriteBatch.DrawString(font, A3n + "/" + A3d, new Vector2(AnsTextPos3x, AnsTextPos3y), Color.Black);
                spriteBatch.DrawString(font, A4n + "/" + A4d, new Vector2(AnsTextPos4x, AnsTextPos4y), Color.Black);
                spriteBatch.DrawString(font, A5n + "/" + A5d, new Vector2(AnsTextPos5x, AnsTextPos5y), Color.Black);
                spriteBatch.DrawString(font, A6n + "/" + A6d, new Vector2(AnsTextPos6x, AnsTextPos6y), Color.Black);
                spriteBatch.DrawString(font, A7n + "/" + A7d, new Vector2(AnsTextPos7x, AnsTextPos7y), Color.Black);
                spriteBatch.DrawString(font, A8n + "/" + A8d, new Vector2(AnsTextPos8x, AnsTextPos8y), Color.Black);

                //Draws Score
                spriteBatch.DrawString(font, "Score: " + Score, new Vector2(26, 60), Color.Black);

                //Mouse Test
                spriteBatch.DrawString(font, "Test: " + changeProbs, new Vector2(26, 21), Color.Black);

                //Draws Answer to the Question
                spriteBatch.DrawString(font, "Answer: " + AnsNum, new Vector2(26, 40), Color.Black);

                spriteBatch.End();
            }
            
            //Draw
            base.Draw(gameTime);
        }
    }
}
