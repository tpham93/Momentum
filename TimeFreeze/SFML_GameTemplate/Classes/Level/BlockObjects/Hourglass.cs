using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

class Hourglass : Objects
{

    //private Sprite sprite;
    public Hourglass(Vector2f position)
        : base(new PolygonShapeSAT(new Vector2i(2 * Constants.TILESIZE, 2 * Constants.TILESIZE), position, false))
    {
        this.Position = position;
        sprite = new Sprite(Objects.objektTextures[6], new IntRect(0, 0, 2 * Constants.TILESIZE, 2 * Constants.TILESIZE));
        sprite.Origin = new Vector2f(Constants.TILESIZE, Constants.TILESIZE);


    }
    public override void update(GameTime gameTime)
    {
        //throw new NotImplementedException();
    }



    public override void initialize()
    {
        throw new NotImplementedException();
    }


    //public override void draw(List<RenderTexture> targets, RenderStates state)
    //{
    //    targets.ElementAt(0).Draw(sprite, state);
    //}
}
