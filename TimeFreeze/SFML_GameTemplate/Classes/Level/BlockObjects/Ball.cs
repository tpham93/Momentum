using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

    class Ball : Objects
    {

        private Sprite sprite;
        public Ball(Vector2f position)
            :base(new CircleShapeSAT(Constants.TILESIZE,position,true))
        {
            this.Position = position;
            sprite = new Sprite(Objects.objektTextures[2], new IntRect(0, 0, Constants.TILESIZE, Constants.TILESIZE));
            sprite.Position = position;


        }
        public override void update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void draw(List<RenderTexture> targets, RenderStates state)
        {
            targets.ElementAt(0).Draw(sprite, state);
        }

        public override void initialize()
        {
            throw new NotImplementedException();
        }
    
}
