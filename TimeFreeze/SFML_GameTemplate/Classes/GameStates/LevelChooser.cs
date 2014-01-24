using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;


class LevelChooser : IGameState
{
    public void Initialize()
    {
        
    }

    public void LoadContent(ContentManager manager)
    {
        
    }

    public EGameState Update(GameTime gameTime)
    {
        if (Input.isClicked(Keyboard.Key.Escape))
            return EGameState.MainMenu;

        return EGameState.LevelChooser;
    }

    public void Draw(GameTime gameTime, SFML.Graphics.RenderWindow window)
    {
        
    }
}

