using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TimeFreezeGame : Game
{

    EGameState currentGameState = EGameState.MainMenu;
    EGameState prevGameState;

    IGameState gameState;

    public TimeFreezeGame()
        : base(800, 600, "name")
    {
        List<Keyboard.Key> keys = new List<Keyboard.Key>();
        keys.Add(Keyboard.Key.Escape);

        Input.init(keys);

    }

    public override void update(GameTime gameTime)
    {
        currentGameState = gameState.Update(gameTime);
    }

    public override void draw(GameTime gameTime, RenderWindow window)
    {
        gameState.Draw(gameTime, window);
    }

    public override void loadContent(ContentManager content)
    {
       
    }


    private void handleNewGameState()
    {



    }
}
