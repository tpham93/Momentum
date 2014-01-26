using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MainMenu : IGameState
{
    Text gameName = new Text(Constants.WINDOWNAME, Assets.font);
    Text[] buttonTexts = new Text[3];

    int current = 0;
    


    public MainMenu()
    {
        
    }

    public void Initialize()
    {
        buttonTexts[0] = new Text("Start", Assets.font);
        buttonTexts[0].Color = Color.Red;
        buttonTexts[0].Position = new Vector2f(Constants.WINDOWWIDTH / 2 - buttonTexts[0].GetGlobalBounds().Width / 2, 120);
        buttonTexts[0].Scale = new Vector2f(2, 2);

        buttonTexts[1] = new Text("Credits", Assets.font);
        buttonTexts[1].Color = Color.White;
        buttonTexts[1].Position = new Vector2f(Constants.WINDOWWIDTH / 2 - buttonTexts[1].GetGlobalBounds().Width / 2, 170);
        buttonTexts[1].Scale = new Vector2f(1, 1);

        buttonTexts[2] = new Text("Quit", Assets.font);
        buttonTexts[2].Color = Color.White;
        buttonTexts[2].Position = new Vector2f(Constants.WINDOWWIDTH / 2 - buttonTexts[2].GetGlobalBounds().Width / 2, 220);
        buttonTexts[2].Scale = new Vector2f(1, 1);

        gameName.Scale = new Vector2f(1.5f, 1.5f);
        gameName.Position = new Vector2f(Constants.WINDOWWIDTH / 2 - gameName.GetGlobalBounds().Width / 2, 0 );


    }

    public void LoadContent(ContentManager manager)
    {
      
    }

    public EGameState Update(GameTime gameTime, RenderWindow window)
    {

        for (int i = 0; i < buttonTexts.Length; i++)
        {
            if (Input.isInside(buttonTexts[i].GetGlobalBounds()))
            {
                current = i;

                if (Input.leftClicked())
                {
                    switch (current)
                    {
                        case 0:
                            return EGameState.LevelChooser;

                        case 1:
                            return EGameState.Credits;
                            
                        case 2:
                            return EGameState.None;
                    }
                }
                  
            }

        
        }


        if (Input.isClicked(Keyboard.Key.S))
            current = (current + 1) % 3;

        if (Input.isClicked(Keyboard.Key.W))
            current = (current + 2) % 3;
        

        if(Input.isClicked(Keyboard.Key.Return))
            switch (current)
            {
                case 0: 
                    return EGameState.LevelChooser;

                case 1: 
                    return EGameState.Credits;

                case 2: 
                    return EGameState.None;
            }

        if (Input.isClicked(Keyboard.Key.Escape))
            return EGameState.None;

        changeColor(gameTime);

        return EGameState.MainMenu;
    }

    public void Draw(GameTime gameTime, List<RenderTexture> targets)
    {

        targets.ElementAt(0).Clear(Color.Black);

        foreach (Text t in buttonTexts)
            targets[0].Draw(t);

        targets[0].Draw(gameName);

    }

    public void changeColor(GameTime time)
    {
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            if (i == current)
            {
                float help = 1.0f + (float)Math.Pow(Math.Sin(time.TotalTime.TotalSeconds * 2), 2) * 0.5f;
                buttonTexts[i].Color = Color.Red;
                buttonTexts[i].Scale = new Vector2f(help,help);

            }

            else
            {
                buttonTexts[i].Color = Color.White;
                buttonTexts[i].Scale = new Vector2f(1, 1);
              
            }

       //     buttonTexts[i].Origin = new Vector2f(buttonTexts[i].GetGlobalBounds().Width / 2, buttonTexts[i].GetGlobalBounds().Height / 2);
        }

    }
}
