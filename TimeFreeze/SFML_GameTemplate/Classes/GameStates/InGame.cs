using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class InGame : IGameState
{
    public InGame()
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
        return EGameState.InGame;
    }

    public void Draw(GameTime gameTime, RenderWindow window)
    {
     
    }
}