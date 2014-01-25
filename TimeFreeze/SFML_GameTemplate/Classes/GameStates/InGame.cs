using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class InGame : IGameState
{

    List<Object> worldObjects;

    private Level level;
    Sprite floor;

    public static LevelID levelId = LevelID.LEVEL0;

    public InGame(LevelID id)
    {
        worldObjects = new List<object>();
        level = new Level();
        worldObjects = level.generateLevel(id);
        floor = new Sprite(Objects.objektTextures[0], new IntRect(0,0,16,16));

    }

    public void Initialize()
    {
       
    }

    public void LoadContent(ContentManager manager)
    {
      
    }

    public EGameState Update(GameTime gameTime)
    {
        level.update(gameTime);

        return EGameState.InGame;
    }

    public void Draw(GameTime gameTime, RenderWindow window)
    {

        //draw floor
        for (uint x = 0; x < Constants.WINDOWWIDTH / 16; x++)
        {
            for (uint y = 0; y < Constants.WINDOWHEIGHT / 16; y++)
            {
                floor.Position = new SFML.Window.Vector2f(x * 16, y * 16);
                window.Draw(floor);
            }
        }



        foreach (Objects obj in worldObjects)
        {
            obj.draw(window);
        }
        
    }
}