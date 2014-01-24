using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LevelChooser : IGameState
{
    int chooseIndex = 0;
    Text[] levelName;

    public LevelChooser()
    {
        //lade den Level-Ordner 
        DirectoryInfo levelInfos = new DirectoryInfo("Content/Level");
        FileInfo[] test = levelInfos.GetFiles();
        levelName = new Text[test.Length];

        for (int i = 0; i < test.Length; i++)
        {
            levelName[i] = new Text("" + test[i].ToString(), Assets.font);
            levelName[i].Color = Color.White;
            levelName[i].Position = new Vector2f(Constants.WINDOWWIDTH / 3 , (Constants.WINDOWHEIGHT / 3) + i * 50);
        }
        for (int i = 0; i < test.Length; i++)
            Console.WriteLine(levelName[i].DisplayedString);
    }

    public void Initialize()
    {
        
    }

    public void LoadContent(ContentManager manager)
    {
        
    }

    public EGameState Update(GameTime gameTime)
    {


        return EGameState.LevelChooser;
    }

    public void Draw(GameTime gameTime, RenderWindow window)
    {
        foreach (Text t in levelName)
            window.Draw(t);
    }
}

