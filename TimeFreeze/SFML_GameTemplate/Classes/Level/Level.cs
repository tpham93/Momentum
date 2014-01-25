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
        public struct Leveldata
        {
            public List<Objects> staticObj;
            public List<Objects> movableObj;

            public Leveldata(List<Objects> stObj, List<Objects> mvObj)
            {
                this.staticObj = stObj;
                this.movableObj = mvObj;
            }
        }

        public Leveldata generateLevel(LevelID id)
        {
            List<Objects> baseLevelStatic = new List<Objects>();
            List<Objects> baseLevelMovable = new List<Objects>();
            isLevelDark = false;

            Console.WriteLine(id);
            //load base Image
            Image baseLevelImage = new Image("Content/Level/map"+(int)id+".png");

            for (uint x = 0; x < baseLevelImage.Size.X; x++)
            {
                for (uint y = 0; y < baseLevelImage.Size.Y; y++)
                {
                    if ((Assets.colorWall).Equals(baseLevelImage.GetPixel(x, y)))
                        baseLevelStatic.Add(new WallBlock(new Vector2f(Assets.worldOffSet.X+(x*Assets.baseBlockSize.X),Assets.worldOffSet.Y+( y*Assets.baseBlockSize.Y))));
                    if ((Assets.colorGoal).Equals(baseLevelImage.GetPixel(x, y)))
                        baseLevelStatic.Add(new Goal(new Vector2f(Assets.worldOffSet.X + (x * Assets.baseBlockSize.X), Assets.worldOffSet.Y + (y * Assets.baseBlockSize.Y))));
                
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
                        baseLevelMovable.Add(new Ball(new Vector2f((float)int.Parse(ls[1]), (float)int.Parse(ls[2]))));
                    else if (int.Parse(ls[0]) == -1)
                        isLevelDark = true;
                    else if (int.Parse(ls[0]) == 1)
                        baseLevelMovable.Add(new Hourglass(new Vector2f((float)int.Parse(ls[1]), (float)int.Parse(ls[2]))));

                }
            }



            return new Leveldata(baseLevelStatic, baseLevelMovable);

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

