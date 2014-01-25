using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


    class WallBlock : Objects
    {
        //private Sprite sprite;

        public WallBlock(Vector2f position)
            : base(new PolygonShapeSAT(new Vector2i(Constants.TILESIZE, Constants.TILESIZE), position, false))
        {
            sprite = new Sprite(Objects.objektTextures[1], new IntRect(0, 0, Constants.TILESIZE, Constants.TILESIZE));
            sprite.Origin = new Vector2f(Constants.TILESIZE / 2, Constants.TILESIZE / 2);
        }

        public override void update(GameTime gameTime)
        {
           
        }

        //public override void draw( List<RenderTexture> targets, RenderStates state)
        //{
        //    targets.ElementAt(0).Draw(sprite, state);
        //}



        public override void initialize()
        {
            throw new NotImplementedException();
        }

        public override Objects.BlockType getType()
        {
            return BlockType.WALL;
        }
    }

