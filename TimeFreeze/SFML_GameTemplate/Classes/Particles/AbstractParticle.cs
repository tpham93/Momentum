using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class AbstractParticle
{
    public Sprite sprite;
    public Vector2f direction;
    public float speed;

    public double lifetime;

    public abstract void draw(GameTime gametime, List<RenderTexture> targets);
    public abstract void update(GameTime gameTime);



}