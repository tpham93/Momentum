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

        protected Shape2DSAT shape;
        protected Sprite sprite;

        private Vector2f velocity;

        public Vector2f Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Shape2DSAT Shape
        {
            get { return shape; }
        }

        public Vector2f Position
        {
            get { return shape.Position; }
            set { shape.Position = value; }
        }

        public float Rotation
        {
            get { return shape.Rotation; }
            set { shape.Rotation = value; }
        }

        protected Objects(Shape2DSAT shape)
        {
            this.shape = shape;
        }

        public abstract void update(GameTime gameTime);

        public virtual void draw(List<RenderTexture> targets, RenderStates state, GameTime time)
        {
            
            sprite.Position = Position;
            targets.ElementAt(0).Draw(sprite, state);
        }

        public static void loadContent()
        {
            objektTextures = new Texture[8];
            objektTextures[0] = new Texture("Content/Block/floor.png");
            objektTextures[1] = new Texture("Content/Block/wall.png");
            objektTextures[2] = new Texture("Content/Block/ball.png");
            objektTextures[3] = new Texture("Content/Block/goal.png");
            objektTextures[4] = new Texture("Content/Block/floor_02.png");
            objektTextures[5] = new Texture("Content/Block/floor_03.png");
            objektTextures[6] = new Texture("Content/Items/hourglass_large.png");
            objektTextures[7] = new Texture("Content/Block/lightstone.png");

        }

        public abstract void initialize();

        public abstract BlockType getType();

        public enum BlockType
        {
            BALL,
            GOAL,
            HOURGLAS,
            WALL,
            LIGHTBLOCK


        }
}
