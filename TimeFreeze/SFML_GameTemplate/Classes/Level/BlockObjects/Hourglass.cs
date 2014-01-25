using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

class Hourglass : Objects
{

    private int num;
    private Text numT;
    //private Sprite sprite;
    public Hourglass(Vector2f position, int num)
        : base(new PolygonShapeSAT(new Vector2i(2 * Constants.TILESIZE, 2 * Constants.TILESIZE), position, false))
    {
        this.Position = position;
        sprite = new Sprite(Objects.objektTextures[6], new IntRect(0, 0, 2 * Constants.TILESIZE, 2 * Constants.TILESIZE));
        sprite.Origin = new Vector2f(Constants.TILESIZE, Constants.TILESIZE);

        numT = new Text(num.ToString(), Assets.font);
        numT.Position = position;
        numT.Scale = new Vector2f(0.7f,0.7f);
        
        numT.Color = Color.Black;

        this.num = num;


    }
    public override void update(GameTime gameTime)
    {
        //throw new NotImplementedException();
    }



    public override void initialize()
    {
        throw new NotImplementedException();
    }
    public override void draw(List<RenderTexture> targets, GameTime time)
    {

        sprite.Position = Position;
        targets.ElementAt(0).Draw(sprite);
        targets.ElementAt(0).Draw(numT);
    }

    //public override void draw(List<RenderTexture> targets, RenderStates state)
    //{
    //    targets.ElementAt(0).Draw(sprite, state);
    //}

    public override Objects.BlockType getType()
    {
        return BlockType.HOURGLAS;
    }

    public int getNum()
    {
        return num;
    }
}
