using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TimeFreezeGame : Game
{
    public TimeFreezeGame()
        : base(800, 600, "name")
    {
        List<Keyboard.Key> keys = new List<Keyboard.Key>();
        keys.Add(Keyboard.Key.Escape);

        Input.init(keys);

    }

    public override void update(GameTime gameTime)
    {

    }

    public override void draw(GameTime gameTime, RenderWindow window)
    {
       
    }

    public override void loadContent(ContentManager content)
    {
       
    }

}
