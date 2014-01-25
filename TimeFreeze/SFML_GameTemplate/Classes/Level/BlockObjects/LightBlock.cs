using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


    class LightBlock : Objects
    {
        Sprite spr;

        public LightBlock(Vector2f position, bool movable)
            :base(new CircleShapeSAT(Constants.TILESIZE/2,position,movable))
        {

            this.sprite = new Sprite(Objects.objektTextures[7], new IntRect(0, 0, 16, 16));
            sprite.Position = position;
            sprite.Origin = new Vector2f(Constants.TILESIZE / 2, Constants.TILESIZE / 2);
            spr = new Sprite();
            spr.Texture = Assets.lightCircle;
            spr.Origin = new Vector2f(32, 32) ;
            spr.Position = this.Position;
            spr.Scale = new Vector2f(3f, 3f);
            
           
            
        }

        public override void update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void initialize()
        {
            //throw new NotImplementedException();
        }
        public override void draw(List<RenderTexture> targets, RenderStates state, GameTime time)
        {


            spr.Color = Help.lerp(Assets.AcaOrange, Color.Yellow, (float)Math.Pow(Math.Sin(time.TotalTime.TotalSeconds),2));

            

            
            sprite.Position = Position;
            
            targets.ElementAt(0).Draw(sprite, state);
            targets.ElementAt(1).Draw(spr);
        }
    }

