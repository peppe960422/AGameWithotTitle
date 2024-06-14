using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame
{
    internal class Brick : Ding
    {
        internal const int Height = 30;
        internal const int Width = 30;

        
        internal  Rectangle Rect
        {
            get
            {

                return new Rectangle(this.X, this.Y, Width, Height);
            }
        }


        public Brick(Point pos) : base(pos)
        {

        }


        public Brick(Point pos,Texture2D texture) : base(pos)
        {
           
            this.texture = texture;
        }

     
    }
    sealed internal class GrassBrick : GroundBrick
    {
        internal Rectangle RectGrass()
        {

            return new Rectangle(posizione.X, posizione.Y, Width, 6);

        }
        internal Color ColoreErba()
        {
            return Color.Green;
        }
        public GrassBrick(Point pos, Texture2D texture) : base(pos,texture)
        {


        }
        public GrassBrick(Point pos) : base(pos)
        {

        }



    }
     internal class GroundBrick : Brick
    {
      
        public GroundBrick(Point pos, Texture2D texture) : base(pos, texture)
        {


        }
        internal override Color colore { get { return Color.Beige; } }
        public GroundBrick(Point pos) : base(pos)
        {

        }


    }
    sealed internal class SpikeBrick : Brick
    {


        public SpikeBrick(Point pos, Texture2D texture) : base(pos, texture)
        {
        }
        public SpikeBrick(Point pos) : base(pos)
        {

        }






}




    sealed internal class WaterBrick : Brick
    {

        public WaterBrick(Point pos, Texture2D texture) : base(pos, texture)
        {


        }

       internal override Color colore { get { return Color.DarkCyan; } }
        public WaterBrick(Point pos) : base(pos)
        {

        }





    }

    sealed internal class Baum : Ding
    {



        public Baum(Point pos) : base(AggiustaY(pos)) { }
        internal override Texture2D texture
        {
            get
            {
                if (posizione.X % 90 == 0) { return this.textureArray[0][0]; }
                else if (posizione.X % 60 == 0) { return this.textureArray[0][1]; }
                else { return this.textureArray[0][2]; };
            }
        }
       internal  Rectangle BaumSpriets()
        {
           if (posizione.X % 90 == 0) { return new Rectangle(posizione.X, posizione.Y, 127, 98); }
                else if (posizione.X % 60 == 0) { return new Rectangle(posizione.X, posizione.Y, 80, 80); }
                else { return new Rectangle(posizione.X, posizione.Y, 87, 106); } ;
        
        
        }

        private static Point AggiustaY(Point pos)
        {
          

            if(pos.X % 90 == 0)
            {
                pos.Y = pos.Y - 67;

                return pos;
            }
            else if (pos.X % 60 == 0)
            {
                pos.Y = pos.Y - 50;

                return pos;
            }
            else
            {
                pos.Y = pos.Y - 75;

                return pos;
            };
        }
       

        }
    }
