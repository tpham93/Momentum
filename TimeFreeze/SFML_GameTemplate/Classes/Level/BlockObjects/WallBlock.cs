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
        {
            this.Position = position;
            
            objSprite = new Sprite(Objects.objektTextures[1], new IntRect(0 ,0, 16, 16));
            objSprite.Position = position;
        }

        public override void update(GameTime gameTime)
        {
           
        }

        public override void draw( List<RenderTexture> targets, RenderStates state)
        {
            targets.ElementAt(0).Draw(objSprite, state);
        }



        public override void initialize()
        {
            throw new NotImplementedException();
        }
    }

