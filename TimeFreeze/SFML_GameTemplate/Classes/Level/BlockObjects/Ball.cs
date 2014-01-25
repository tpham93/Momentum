using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

    class Ball : Objects
    {

        //private Sprite sprite;
        public Ball(Vector2f position)
            :base(new CircleShapeSAT(Constants.TILESIZE/2,position,true))
        {
            this.Position = position;
            sprite = new Sprite(Objects.objektTextures[2], new IntRect(0, 0, Constants.TILESIZE, Constants.TILESIZE));
            sprite.Origin = new Vector2f(Constants.TILESIZE / 2, Constants.TILESIZE / 2);
            
        }
        public override void update(GameTime gameTime)
        {
            Vector2f speed = new Vector2f(Input.currentMousePos.X - Position.X,Input.currentMousePos.Y - Position.Y);
            speed /= new Vector2(speed).Length();
            Position += speed * 5;
            sprite.Position = Position + new Vector2f(Constants.TILESIZE / 2, Constants.TILESIZE / 2);
        }

        public override void initialize()
        {
            throw new NotImplementedException();
        }
    
}
