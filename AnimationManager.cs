using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    internal class AnimationManager
    {
       
       int numFrames;
       internal int ActiveFrame {  get; set; }  
        int counter;
        int interval;
        Texture2D[] texture;

     

        public int GetFrame(){

            return ActiveFrame;



        } 
        public AnimationManager(  Texture2D[] Frames ) 
        {
            this.texture = Frames;
            this.numFrames =  Frames.Length; 
           
           
            interval = 5;
        
        }

        internal void Update ()
        {

            counter++;
            if ( counter > interval) 
            {
                counter= 0; NextFrame(); 
            }

        }

        private void NextFrame()
        {
            ActiveFrame++;
            if (ActiveFrame >= numFrames)
            {

                ActiveFrame = 0;

            }
        }
    }
}
