using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SparkleParticle : AbstractParticle
{
    public SparkleParticle(Vector2f startPos, Vector2f direction)
    {
        this.sprite = new Sprite(Assets.sparkle);
        this.sprite.Position = startPos;
        this.sprite.Origin = new Vector2f(32, 32);

        this.direction = new Vector2f(direction.X, direction.Y);
        this.speed = (float)InGame.random.NextDouble() * 5.0f;

        this.lifetime = 3.0 * InGame.random.NextDouble();
        this.maxLifeTime = 3.0 * InGame.random.NextDouble();

        this.sprite.Scale = new Vector2f(0.5f, 0.5f);
    }

    public override void draw(GameTime gametime, List<RenderTexture> targets)
    {
        targets[0].Draw(sprite);
    }

    public override void update(GameTime gameTime)
    {

        this.sprite.Position += this.direction * this.speed;
        this.sprite.Color = Help.lerp(Color.Transparent, Color.White, Math.Min((float)(this.lifetime / this.maxLifeTime), 1.0f));
        

        this.lifetime -= gameTime.ElapsedTime.TotalSeconds;

    }
}
