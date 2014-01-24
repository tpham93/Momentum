using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace SFML_GameTemplate.Classes.Level.BlockObjects
{
    abstract class Objects
    {
        private Vector2f position;

        public Vector2f Position
        {
            get { return position; }
            set { position = value; }
        }
        private float rotation;

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }


        public override void update(GameTime gameTime)
        {

        }

        public override void draw(GameTime gameTime, RenderWindow window)
        {

        }

        public override void loadContent(ContentManager content)
        {

        }

        public override void initialize()
        {

        }

    }
}
