﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

    class Ball : Objects
    {

        private Sprite sprite;
        public Ball(Vector2f position)
        {
            this.Position = position;
            sprite = new Sprite(Objects.objektTextures[2], new IntRect(0, 0, 16, 16));
            sprite.Position = position;


        }
        public override void update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void draw(SFML.Graphics.RenderWindow window)
        {
            window.Draw(sprite);
        }

        public override void initialize()
        {
            throw new NotImplementedException();
        }
    
}
