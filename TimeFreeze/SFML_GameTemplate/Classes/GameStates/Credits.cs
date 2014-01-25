using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;


class Credits : IGameState
{

    Text credits = new Text("Shader: \nKai \n \nGame base: \nGerd \n \nKollision and interaction of something (Gameplay): \nTuan \n \nTextures: \nJarek \n \nTheNewbie: \nTobias ", Assets.font);
    Texture acagamics = new Texture("Content/logo.png");
    Sprite logo;
    public void Initialize()
    {
        credits.Color = Game.AcaOrange;
        credits.Position = new Vector2f(10, 60);
        credits.Scale = new Vector2f(0.75f, 0.75f);
        logo = new Sprite(acagamics);
        logo.Position = new Vector2f(400, 70);
        logo.Scale = new Vector2f(0.5f, 0.5f);
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
        targets[0].Draw(credits);
        targets[0].Draw(logo);
    }
}
