using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LevelChooser : IGameState
{

    String[] names;
    FileInfo[] infos;

    public LevelChooser()
    {
        DirectoryInfo levelInfos = new DirectoryInfo("Content/Level/");

        foreach (FileInfo i in levelInfos.GetFiles())
            System.Console.WriteLine(i.ToString());
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

