using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


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


        public abstract void update(GameTime gameTime);

        public abstract void draw(RenderWindow window);

        public abstract void loadContent(ContentManager content);

        public abstract void initialize();

    
}
