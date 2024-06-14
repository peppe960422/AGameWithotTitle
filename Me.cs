using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    internal class Me : LebendeObj
    {
        internal int width { get; set; }
        internal int height { get; set; }

        bool SpaceKey { get; set; }


        //internal int HoSaltato {  get; set; }   

        internal Texture2D[] RunSpritesRight { get; set; }
        internal Texture2D[] RunSpritesLeft { get; set; }


        internal Texture2D[] IdleSprites { get; set; }

        internal Texture2D[] JumpSprite { get; set; }
        bool moveRight { get; set; }
        bool moveLeft { get; set; }

        internal override Rectangle Rect
        {
            get
            {

                return new Rectangle(this.X, this.Y - 30, width, height);
            }
        }

        public Me(Point pos) : base(pos)
        {
            this.width = 38;
            this.height = 60;
            this.moveRight = false;
            this.moveLeft = false;
            this.jump = false;

        }


        public void LoadSprites(Texture2D[] RunRight, Texture2D[] RunLeft, Texture2D[] Idle, Texture2D[] Jump)


        {
            RunSpritesRight = new Texture2D[RunRight.Length];
            RunSpritesLeft = new Texture2D[RunLeft.Length];

            IdleSprites = new Texture2D[Idle.Length];
            JumpSprite = new Texture2D[Jump.Length];


            this.RunSpritesRight = RunRight;
            this.RunSpritesLeft = RunLeft;
            this.IdleSprites = Idle;
            this.JumpSprite = Jump;


        }

        public void UpdatePlayer(GameTime g, KeyboardState currentKeyState, KeyboardState previousKeyState)
        {
            base.Update(g);




            if ((currentKeyState.IsKeyDown(Keys.Space))& !previousKeyState.IsKeyDown(Keys.Space) && collideDown && !jump)
            {


                jump = true;
                this.velocitaY = -17;
                SpaceKey = true;
                //HoSaltato = 20;


            }
            else 
            {
                jump = false;
            }
            if (currentKeyState.IsKeyDown(Keys.Right) && !collideRecht)
            {
                moveRight = true;
                this.X += 3;
            }
            if (currentKeyState.IsKeyDown(Keys.Left) && !collideLinks)
            {
                moveLeft = true;
                this.X -= 3;
            }
            if (currentKeyState.IsKeyDown(Keys.Right) && jump && currentKeyState.IsKeyDown(Keys.Space) && !collideRecht)
            {
                this.Y -= 1;
                this.X += 1;
                /*HoSaltato = 20;*/
            }
            if (currentKeyState.IsKeyDown(Keys.Left) && jump && currentKeyState.IsKeyDown(Keys.Space) && !collideLinks)
            {

                this.Y -= 1;
                this.X -= 1;
                /* HoSaltato = 20;*/
            }
            if (currentKeyState.IsKeyUp(Keys.Right))
            {
                moveRight = false;
            }
            if (currentKeyState.IsKeyUp(Keys.Left))
            {
                moveLeft = false;
            }
            if (!collideDown && !jump)
            {
                this.velocitaY += 1;
                jump = true;
            }
            if (collideDown && !jump) { this.velocitaY = 0; jump = false; }
            this.Y += velocitaY;
            //HoSaltato--;

        }
        internal override void Draw(SpriteBatch s, int frame)
        {

            if (moveRight)
            {
                s.Draw(texture: RunSpritesRight[frame],

                      destinationRectangle: Rect,
                      color: Color.White
              );
            }
            else if (moveLeft)
            {

                s.Draw(texture: RunSpritesLeft[frame],
                      destinationRectangle: Rect,

                      color: Color.White

               ); ;

            }

            else if (jump)
            {
                s.Draw(texture: JumpSprite[frame],
                    destinationRectangle: Rect,
                      color: Color.White
            );

            }

            else
            {
                s.Draw(texture: IdleSprites[frame],

                      destinationRectangle: Rect,
                      color: Color.White


             );
            }


        }




    }
}

