using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SparkleParticle : AbstractParticle
{

    Sprite lightSprite;

    static Color[] colArr = {Color.Red, Color.Yellow, Color.Green, Color.Magenta, Color.White, Color.Cyan };

    int colIndex;

    public SparkleParticle(Vector2f startPos, Vector2f halfDir, Vector2f dir, float speed)
    {

        initStuff();

        this.sprite.Position = startPos;

        this.speed = speed;

        Vector2f secondHalfDir1 = new Vector2f(halfDir.Y, -halfDir.X);
        Vector2f secondHalfDir2 = new Vector2f(-halfDir.Y, halfDir.X);

        if (Help.Dot(dir, secondHalfDir1) < 0)
            this.direction = Help.lerp(halfDir, secondHalfDir2, (float)InGame.random.NextDouble());
        
        else
            this.direction = Help.lerp(halfDir, secondHalfDir1, (float)InGame.random.NextDouble());

         
    }

    public SparkleParticle(Vector2f startPos)
    {
        this.sprite.Position = startPos;


        Vector2f help = new Vector2f();

      //  this.direction = new Vector2f() - startPos;


    }

    private void initStuff()
    {
        lightSprite = new Sprite();
        lightSprite.Texture = Assets.lightCircle;
        lightSprite.Origin = new Vector2f(32, 32);
        lightSprite.Scale = new Vector2f(1.1f, 1.1f);

        colIndex = InGame.random.Next(colArr.Length);

        this.sprite = new Sprite(Assets.sparkle);
        this.sprite.Origin = new Vector2f(32, 32);



        this.lifetime = 3.0 * InGame.random.NextDouble();
        this.maxLifeTime = 3.0 * InGame.random.NextDouble();

        this.sprite.Scale = new Vector2f(0.4f, 0.4f);
    }

    public override void draw(GameTime gametime, List<RenderTexture> targets)
    {
        targets[0].Draw(sprite);
        targets[1].Draw(lightSprite);
    }

    public override void update(GameTime gameTime)
    {
        Color tmpColor = Help.fade(Help.lerp(Color.White, colArr[colIndex], Math.Min((float)(this.lifetime / this.maxLifeTime), 1.0f)), (float)Math.Min(lifetime, 1.0));
        
        Vector2f tmpScale = new Vector2f(Math.Min((float)lifetime, 1.1f), Math.Min((float)lifetime,1.1f));
        Vector2f tmpScale2 = new Vector2f(Math.Min((float)lifetime, 0.4f), Math.Min((float)lifetime, 0.4f));

        this.sprite.Position += this.direction * this.speed * (float)Math.Min(lifetime, 1.0);
        this.lightSprite.Position = sprite.Position;

        this.sprite.Color = tmpColor;
        this.lightSprite.Color = tmpColor;

        this.sprite.Scale = tmpScale2;
        this.lightSprite.Scale = tmpScale;

        
        this.lifetime -= gameTime.ElapsedTime.TotalSeconds;

    }
}
