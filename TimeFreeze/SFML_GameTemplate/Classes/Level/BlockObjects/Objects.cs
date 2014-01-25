using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


    abstract class Objects
    {
        public static Texture[] objektTextures;
        
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

        public abstract void draw(List<RenderTexture> targets, RenderStates state);

        public static void loadContent()
        {
            objektTextures = new Texture[7];            objektTextures[0] = new Texture("Content/Block/floor.png");
            objektTextures[1] = new Texture("Content/Block/wall.png");
            objektTextures[2] = new Texture("Content/Block/ball.png");
            objektTextures[3] = new Texture("Content/Block/goal.png");
            objektTextures[4] = new Texture("Content/Block/floor_02.png");
            objektTextures[5] = new Texture("Content/Block/floor_03.png");
            objektTextures[6] = new Texture("Content/Items/hourglass_large.png");

        }

        public abstract void initialize();

    
}
