using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SparkleParticle : AbstractParticle
{
    public SparkleParticle(Vector2f startPos, Vector2f halfDir, Vector2f dir, float speed)
    {
        this.sprite = new Sprite(Assets.sparkle);
        this.sprite.Position = startPos;
        this.sprite.Origin = new Vector2f(32, 32);


        this.speed = speed;

        this.lifetime = 3.0 * InGame.random.NextDouble();
        this.maxLifeTime = 3.0 * InGame.random.NextDouble();

        Vector2f secondHalfDir1 = new Vector2f(halfDir.Y, -halfDir.X);
        Vector2f secondHalfDir2 = new Vector2f(-halfDir.Y, halfDir.X);

        if (Help.Dot(dir, secondHalfDir1) < 0)
            this.direction = Help.lerp(halfDir, secondHalfDir2, (float)InGame.random.NextDouble());
        
        else
            this.direction = Help.lerp(halfDir, secondHalfDir1, (float)InGame.random.NextDouble());
         

        this.sprite.Scale = new Vector2f(0.4f, 0.4f);
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
