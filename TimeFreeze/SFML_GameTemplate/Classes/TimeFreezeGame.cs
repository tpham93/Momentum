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

    Text infoText;
    float fps;



    public TimeFreezeGame()
        : base(Constants.WINDOWWIDTH, Constants.WINDOWHEIGHT, Constants.WINDOWNAME)
    {
        List<Keyboard.Key> keys = new List<Keyboard.Key>();
        keys.Add(Keyboard.Key.Escape);
        keys.Add(Keyboard.Key.W);
        keys.Add(Keyboard.Key.A);
        keys.Add(Keyboard.Key.S);
        keys.Add(Keyboard.Key.D);
        keys.Add(Keyboard.Key.Return);
        keys.Add(Keyboard.Key.Space);

        keys.Add(Keyboard.Key.P);

        keys.Add(Keyboard.Key.Up);
        keys.Add(Keyboard.Key.Down);
        keys.Add(Keyboard.Key.Left);
        keys.Add(Keyboard.Key.Right);

        Input.init(keys);

        Assets.font = new Font("Content/Font/PRIMELEC.ttf");
        Objects.loadContent();
        ShaderManager.initialize();

        handleNewGameState();


    }

    public override void update(GameTime gameTime, RenderWindow window)
    {
        currentGameState = gameState.Update(gameTime, window);

        if (Input.isClicked(Keyboard.Key.Escape))
            if (currentGameState == EGameState.MainMenu)
                currentGameState = 0;
            else
            currentGameState = EGameState.MainMenu;

        if (Input.isClicked(Keyboard.Key.Right))
            currentGameState++;

        else if (Input.isClicked(Keyboard.Key.Left))
            currentGameState--;

        if (currentGameState != prevGameState)
            handleNewGameState();
    }

    public override void draw(GameTime gameTime, List<RenderTexture> target)
    {
        fps = 1.0f / (float)gameTime.ElapsedTime.TotalSeconds;

        

        window.Clear(Color.Black);

        gameState.Draw(gameTime, target);
        infoText.DisplayedString = ""+currentGameState;
        infoText.DisplayedString += " " + fps;

        target.ElementAt(0).Draw(infoText);
    }

    public override void loadContent(ContentManager content)
    {
        infoText = new Text("", Assets.font);

        
        
    }


    private void handleNewGameState()
    {

        contentManager.disposeAll();

        switch (currentGameState)
        {
            case EGameState.None:
                window.Close();
                break;

            case EGameState.MainMenu:
                gameState = new MainMenu();
                break;

            case EGameState.InGame:
                gameState = new InGame();
                break;
            case EGameState.Credits:
                gameState = new Credits();
                break;
            case EGameState.LevelChooser:
                gameState = new LevelChooser();
                break;

        }

        gameState.LoadContent(contentManager);
        gameState.Initialize();

        prevGameState = currentGameState;
    }
}
