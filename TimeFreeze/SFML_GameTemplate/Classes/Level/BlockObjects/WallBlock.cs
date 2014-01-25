using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


    class WallBlock : Objects
    {
        private Sprite objSprite;

        public WallBlock(Vector2f position)
            : base(new PolygonShapeSAT(new Vector2i(Constants.TILESIZE, Constants.TILESIZE), position, false))
        {
            objSprite = new Sprite(Objects.objektTextures[1], new IntRect(0, 0, Constants.TILESIZE, Constants.TILESIZE));
            objSprite.Position = position;
        }

        public override void update(GameTime gameTime)
        {
           
        }

        public override void draw( List<RenderTexture> targets)
        {
            targets.ElementAt(0).Draw(objSprite);
        }



        public override void initialize()
        {
            throw new NotImplementedException();
        }
    }

