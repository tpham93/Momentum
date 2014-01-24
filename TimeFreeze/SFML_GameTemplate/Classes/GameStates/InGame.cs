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

    public static LevelID levelId = LevelID.LEVEL0;

    public InGame(LevelID id)
    {
        worldObjects = new List<object>();
        level = new Level();
        worldObjects = level.generateLevel(id);
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
        
        foreach (Objects obj in worldObjects)
        {
            obj.draw(window);
        }
    }
}