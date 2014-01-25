using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


    class Level
    {
        private bool isLevelDark = false;

        public bool IsLevelDark{ get { return isLevelDark; } }

        public List<Object> generateLevel(LevelID id)
        {
            List<Object> baseLevel = new List<Object>();
            isLevelDark = false;

            Console.WriteLine(id);
            //load base Image
            Image baseLevelImage = new Image("Content/Level/map"+(int)id+".png");

            for (uint x = 0; x < baseLevelImage.Size.X; x++)
            {
                for (uint y = 0; y < baseLevelImage.Size.Y; y++)
                {
                    if ((Assets.colorWall).Equals(baseLevelImage.GetPixel(x, y)))
                        baseLevel.Add(new WallBlock(new Vector2f(Assets.worldOffSet.X+(x*Assets.baseBlockSize.X),Assets.worldOffSet.Y+( y*Assets.baseBlockSize.Y))));
                    if ((Assets.colorGoal).Equals(baseLevelImage.GetPixel(x, y)))
                        baseLevel.Add(new Goal(new Vector2f(Assets.worldOffSet.X + (x * Assets.baseBlockSize.X), Assets.worldOffSet.Y + (y * Assets.baseBlockSize.Y))));
                
                }
            }
            //Load Map File
            using (StreamReader sr = new StreamReader("Content/Level/map" + (int)id + ".txt"))
            {
                
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    String[] ls =line.Split(':');
                    if(int.Parse(ls[0])==0)
                        baseLevel.Add(new Ball(new Vector2f((float)int.Parse(ls[1]), (float)int.Parse(ls[2]))));
                    else if (int.Parse(ls[0]) == -1)
                        isLevelDark = true;
                    else if (int.Parse(ls[0]) == 1)
                        baseLevel.Add(new Hourglass(new Vector2f((float)int.Parse(ls[1]), (float)int.Parse(ls[2]))));

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
        LEVEL5,
        LEVEL6,
        LEVEL7,
        LEVEL8,
        LEVEL9,

        LEVELNUM


    }

