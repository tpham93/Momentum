using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;


    class World
    {

        List<Object> worldObjects;

        private Level level;

        public World()
        {
            worldObjects = new List<object>();
            level = new Level();
            worldObjects = level.generateLevel(LevelID.LEVEL0);

        }
        public void update(GameTime gameTime)
        {
            level.update(gameTime);
        }
        public void draw(RenderWindow win)
        {
            Console.WriteLine(worldObjects.Count);
            foreach (Objects obj in worldObjects)
            {
                obj.draw(win);
            }
        }


    }
    public enum Block
    {

    }


