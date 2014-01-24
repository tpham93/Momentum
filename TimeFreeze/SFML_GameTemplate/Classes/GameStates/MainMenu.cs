using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MainMenu : IGameState
{
    Text lvlselection = new Text("Levelauswahl", Assets.font);
    Text credits = new Text("Mitwirkende", Assets.font);
    Text end = new Text("Ende", Assets.font);
    int current = 0;
    
    //zum testen
    private World world;

    public MainMenu()
    {
        world = new World();
    }

    public void Initialize()
    {
        lvlselection.Color = Color.Red;
        lvlselection.Position = new Vector2f(10, 60);
        lvlselection.Scale = new Vector2f(2, 2);
        credits.Color = Color.White;
        credits.Position = new Vector2f(10, 120);
        credits.Scale = new Vector2f(1, 1);
        end.Color = Color.White;
        end.Position = new Vector2f(10, 180);
        end.Scale = new Vector2f(1, 1);
    }

    public void LoadContent(ContentManager manager)
    {
      
    }

    public EGameState Update(GameTime gameTime)
    {
        world.update(gameTime);


        if (Input.isClicked(Keyboard.Key.S))
        {
            current = (current + 1) % 3;
            changeColor();
        }
        if (Input.isClicked(Keyboard.Key.W))
        {
            current = (current + 2) % 3;
            changeColor();
        }
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

        return EGameState.MainMenu;
    }

    public void Draw(GameTime gameTime, RenderWindow window)
    {
        window.Clear(Color.Black);
        window.Draw(lvlselection);
        window.Draw(credits);
        window.Draw(end);
        world.draw(window);
    }

    public void changeColor()
    {
        switch (current)
        {
            case 0: 
                lvlselection.Color = Color.Red;
                lvlselection.Scale = new Vector2f(2, 2);
                credits.Color = Color.White;
                credits.Scale = new Vector2f(1, 1);
                end.Color = Color.White;
                end.Scale = new Vector2f(1, 1);
                break;
            case 1: 
                lvlselection.Color = Color.White;
                lvlselection.Scale = new Vector2f(1, 1);
                credits.Color = Color.Red;
                credits.Scale = new Vector2f(2, 2);
                end.Color = Color.White;
                end.Scale = new Vector2f(1, 1);
                break;
            case 2: 
                lvlselection.Color = Color.White;
                lvlselection.Scale = new Vector2f(1, 1);
                credits.Color = Color.White;
                credits.Scale = new Vector2f(1, 1);
                end.Color = Color.Red;
                end.Scale = new Vector2f(2, 2);
                break;
        }
    }
}
