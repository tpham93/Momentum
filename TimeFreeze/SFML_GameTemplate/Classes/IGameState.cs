using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


enum EGameState {None, MainMenu, InGame, EGameStateCount };

interface IGameState
{
    void Initialize();

    void LoadContent(ContentManager manager);

    EGameState Update(GameTime gameTime);

    void Draw(GameTime gameTime, RenderWindow window);
}

