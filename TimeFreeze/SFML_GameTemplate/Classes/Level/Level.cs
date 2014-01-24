using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


    class Level
    {
        public List<Object> generateLevel(LevelID id)
        {
            List<Object> baseLevel = new List<Object>();


            Image baseLevelImage = new Image("Content/Level/map"+(int)id+".png");

            for (uint x = 0; x < baseLevelImage.Size.X; x++)
            {
                for (uint y = 0; y < baseLevelImage.Size.Y; y++)
                {
                    if ((Assets.colorWall).Equals(baseLevelImage.GetPixel(x, y)))
                        baseLevel.Add(new WallBlock(new Vector2f(Assets.worldOffSet.X+(x*Assets.baseBlockSize.X),Assets.worldOffSet.Y+( y*Assets.baseBlockSize.Y))));


                    
                }
            }


            return baseLevel;

        }

        public void update(GameTime time)
        {
            //TODO
        }


    }

    public enum LevelID
    {
        LEVEL0,
        LEVEL1,
        LEVEL2,
        LEVEL3,
        LEVEL4,

        LEVELNUM


    }

