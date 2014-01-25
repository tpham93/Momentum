using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

    class Ball : Objects
    {
        Sprite spr;
        int freq = 0;
        //private Sprite sprite;
        public Ball(Vector2f position,int freq)
            :base(new CircleShapeSAT(Constants.TILESIZE/2,position,true))
        {
            this.Position = position;
            sprite = new Sprite(Objects.objektTextures[2], new IntRect(0, 0, Constants.TILESIZE, Constants.TILESIZE));
            sprite.Origin = new Vector2f(Constants.TILESIZE / 2, Constants.TILESIZE / 2);
            this.freq = freq;
            spr = new Sprite();
            spr.Texture = Assets.lightCircle;
            spr.Origin = new Vector2f(32, 32);
            spr.Position = this.Position;
            spr.Scale = new Vector2f(3f, 3f);
            
        }
        public override void update(GameTime gameTime)
        {
            Position += Velocity;
            sprite.Position = Position + new Vector2f(Constants.TILESIZE / 2, Constants.TILESIZE / 2);
            spr.Position = this.Position;
        }

        public override void initialize()
        {
            throw new NotImplementedException();
        }
        public override void draw(List<RenderTexture> targets, RenderStates state, GameTime time)
        {

            spr.Scale = new Vector2f((float)Math.Pow(Math.Sin(time.TotalTime.TotalSeconds + (freq)), 2) / 2 + 0.5f, (float)Math.Pow(Math.Sin(time.TotalTime.TotalSeconds + (freq)), 2) / 2 + 0.5f);

            spr.Color = Help.lerp(Assets.AcaOrange, Assets.AcaDarkOrange, (float)Math.Pow(Math.Sin(time.TotalTime.TotalSeconds), 2));




            sprite.Position = Position;

            targets.ElementAt(0).Draw(sprite, state);
            targets.ElementAt(1).Draw(spr);
        }

        public override Objects.BlockType getType()
        {
            return BlockType.BALL;
        }
    }
