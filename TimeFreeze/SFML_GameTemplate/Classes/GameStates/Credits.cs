using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;


class Credits : IGameState
{
    public void Initialize()
    {
        
    }

    public void LoadContent(ContentManager manager)
    {
        
    }

    public EGameState Update(GameTime gameTime, RenderWindow window)
    {
        if (Input.isClicked(Keyboard.Key.Escape))
            return EGameState.MainMenu;

        return EGameState.Credits;
    }

    public void Draw(GameTime gameTime, List<RenderTexture> targets)
    {
        
    }
}
