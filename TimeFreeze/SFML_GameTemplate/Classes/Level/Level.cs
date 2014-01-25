using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;


    class Level
    {
        public List<Object> generateLevel(LevelID id)
        {
            List<Object> baseLevel = new List<Object>();

            Image baseLevelImage = new Image("Content/Level/map"+id+".png");

            for (uint x = 0; x < baseLevelImage.Size.X; x++)
            {
                for (uint y = 0; y < baseLevelImage.Size.Y; y++)
                {
                   //if((Assets.ColorFloor).Equals(baseLevelImage.GetPixel(x,y)))
                        //baseLevel.Add(new WallBlock(
                }
            }


            return baseLevel;

        }


    }

    public enum LevelID
    {
        LEVEL1,
        LEVEL2,
        LEVEL3,
        LEVEL4,

        LEVELNUM


    }

