using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    abstract internal class LebendeObj : Ding
    {
        internal int velocitaY { get; set; }
        internal bool collideRecht {  get; set; }
        internal bool collideLinks { get; set; }
        internal bool collideDown { get; set; } 
        internal int Frame {  get; set; }
        internal bool jump { get; set; }
        virtual internal Rectangle Rect {
                   get
            {

                return new Rectangle(this.X, this.Y, 30, 30);
            }
        }
        public LebendeObj(Point pos) : base(pos)
        {
            
        }
        virtual internal void Draw(SpriteBatch s , int frame )
        {





        }   
        virtual internal int CorreggiY(Brick YbricK)
        {
            int myYpos = 0;
            if (YbricK is GrassBrick )
            {
                 myYpos = YbricK.Y - 30;
            }
            else
            {
                myYpos = YbricK.Y - 60;

            }
           
            
           
            return myYpos;
        }
           
        virtual internal void ICollide(Ding[,] mappa)
        {
            collideRecht = false;
            collideLinks = false;
            collideDown = false;
            Parallel.For(0, mappa.GetLength(0), i =>
            {

                Parallel.For(0, mappa.GetLength(1), j =>
                   {
                       if (mappa[i, j] is GroundBrick || mappa[i, j] is GrassBrick)
                       {
                           Brick brick = (Brick)mappa[i, j];

                           if ((brick.Rect.Left >= this.Rect.Right && brick.Rect.Left - 5 <= this.Rect.Right) && this.Y >= brick.Rect.Bottom - 30 && this.Y <= brick.Rect.Top + 30)
                           {
                               collideRecht = true;
                           }

                           if ((brick.Rect.Right <= this.Rect.Left && brick.Rect.Right + 5 >= this.Rect.Left) && this.Y >= brick.Rect.Bottom - 30 && this.Y <= brick.Rect.Top + 30)
                           {
                               collideLinks = true;
                           }

                           if ((brick.Rect.Top >= this.Rect.Bottom - 5 && brick.Rect.Top <= this.Rect.Bottom + 5) && this.X + 10 >= brick.Rect.Left && this.X + 10 <= brick.Rect.Right)
                           {
                               collideDown = true;
                               this.Y = CorreggiY(brick);
                               jump = false;
                           }

                       }


                   });


            });





        }
       
    }
}
