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
    abstract internal class Ding
    {
        public Point posizione;

        virtual internal Texture2D texture { get; set; }

        virtual internal List<Texture2D[]> textureArray { get; set; }

        public virtual void Update(GameTime g)
        {
         


        }
        public int X
        {
            get { return posizione.X; }
            set
            {
                Point p = new Point(posizione.X, posizione.Y);
                p.X = value;
                this.posizione = p;
            
            
            }
        }


        public int Y
        {
            get { return posizione.Y; }
            set
            {
                Point p = new Point(posizione.X, posizione.Y);
                p.Y = value;
                this.posizione = p;
            }
        }


        
        virtual internal Color colore { get { return Color.White; } }

        public Ding(Point pos)
        {
            this.posizione = pos;
        }
    }
}
