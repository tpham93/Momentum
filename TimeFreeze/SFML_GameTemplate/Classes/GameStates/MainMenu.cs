using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MainMenu : IGameState
{
    public MainMenu()
    {
    }

    public void Initialize()
    {
     
    }

    public void LoadContent(ContentManager manager)
    {
      
    }

    public EGameState Update(GameTime gameTime)
    {
        return EGameState.MainMenu;
    }

    public void Draw(GameTime gameTime, RenderWindow window)
    {
        
    }
}
