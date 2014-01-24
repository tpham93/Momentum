using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LevelChooser : IGameState
{


    public LevelChooser()
    {
        //lade den Level-Ordner 
        DirectoryInfo levelInfos = new DirectoryInfo("Content/Level");
        FileInfo[] test = levelInfos.GetFiles();

        for (int i = 0; i < test.Length; i++)
            Console.WriteLine(test[i].ToString());
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
        
    }
}

