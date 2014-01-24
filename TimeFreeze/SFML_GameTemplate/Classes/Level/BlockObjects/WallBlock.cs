using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;


    class WallBlock : Objects
    {

        public WallBlock(Vector2f position)
        {
            this.Position = position;
        }

        public override void update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void draw( SFML.Graphics.RenderWindow window)
        {
            Console.WriteLine("male male kuchen");
        }

        public override void loadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void initialize()
        {
            throw new NotImplementedException();
        }
    }

