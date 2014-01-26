using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;


class Credits : IGameState
{

    Text credits = new Text("Visual effects and basic idear: \n   Kai  \nGame structure, Tutorial: \n   Gerd  \nCollision and interaction (Gameplay): \n   Tuan  \nTextures and a little bit of everything: \n   Jarek  \nTextures and Leveldesign: \n   Tobias ", Assets.font);
    Texture acagamics = new Texture("Content/logo.png");
    Sprite logo;


    List<AbstractParticle> particles = new List<AbstractParticle>();

    Sprite mouseLight;

    Text cr = new Text("Credits", Assets.font);
    Text srcHeader = new Text("Sources", Assets.font);
    Text aca = new Text("Acagamics e.V.", Assets.font);
    Text sources = new Text("http://www.freesound.org/people/fins/sounds/171670/ - success\nLicensed under Creative Commons: By Attribution 0\nhttp://creativecommons.org/publicdomain/zero/1.0/\n------- \nPeace of mind Kevin MacLeod (incompetech.com)\nLicensed under Creative Commons: By Attribution 3.0\nhttp://creativecommons.org/licenses/by/3.0/\n-------\nhttp://www.bfxr.net/ - other sounds\n-------\nhttp://www.1001freefonts.com/primary_elector.font -font", Assets.font);

    public void Initialize()
    {
        cr.Color = Game.AcaOrange;
        cr.Position = new Vector2f(10, 15);
        cr.Scale = new Vector2f(1, 1);
        aca.Color = Game.AcaOrange;
        aca.Position = new Vector2f(510, 470);
        aca.Scale = new Vector2f(1, 1);
        srcHeader.Color = Color.Green;
        srcHeader.Position = new Vector2f(10, 350);
        srcHeader.Scale = new Vector2f(1, 1);
        credits.Color = Game.AcaOrange;
        credits.Position = new Vector2f(10, 60);
        credits.Scale = new Vector2f(0.75f, 0.75f);
        sources.Color = Color.Green;
        sources.Position = new Vector2f(10, 390);
        sources.Scale = new Vector2f(0.5f, 0.5f);
        logo = new Sprite(acagamics);
        logo.Position = new Vector2f(400, 70);
        logo.Scale = new Vector2f(0.5f, 0.5f);


        mouseLight = new Sprite(Assets.lightCircle);
        mouseLight.Scale = new Vector2f(8.0f, 8.0f);
        mouseLight.Origin = new Vector2f(32, 32);

        InGame.isLevelDark = true;
    }

    public void LoadContent(ContentManager manager)
    {
        
    }

    public EGameState Update(GameTime gameTime, RenderWindow window)
    {

        particles.Add(new SparkleParticle(new Vector2f((float)InGame.random.NextDouble() * Constants.WINDOWWIDTH,
                                                    (float)InGame.random.NextDouble() * Constants.WINDOWHEIGHT)));

        //   particles.Add(new SparkleParticle(new Vector2f((float)(Constants.WINDOWWIDTH / 3 + InGame.random.NextDouble() * 2 * Constants.WINDOWWIDTH / 3), Constants.WINDOWHEIGHT / 4)));

        mouseLight.Position = new Vector2f(Input.currentMousePos.X, Input.currentMousePos.Y);

        float scaleHelp = (float)Math.Pow(Math.Sin(gameTime.TotalTime.TotalSeconds), 2);

        mouseLight.Color = Help.lerp(Color.White, Assets.AcaOrange, scaleHelp);

        scaleHelp = 8.0f + scaleHelp * 2.0f;

        mouseLight.Scale = new Vector2f(scaleHelp, scaleHelp);


        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].update(gameTime);

            if (particles[i].lifetime <= 0)
                particles.Remove(particles[i]);
        }

        if (Input.isClicked(Keyboard.Key.Escape))
            return EGameState.MainMenu;

        return EGameState.Credits;
    }

    public void Draw(GameTime gameTime, List<RenderTexture> targets)
    {
        targets[2].Draw(cr);
        targets[0].Draw(aca);
        targets[2].Draw(srcHeader);
        targets[2].Draw(sources);
        targets[2].Draw(credits);
        targets[0].Draw(logo);

        foreach (AbstractParticle p in particles)
            p.draw(gameTime, targets);

        targets[1].Draw(mouseLight);
    }
}
