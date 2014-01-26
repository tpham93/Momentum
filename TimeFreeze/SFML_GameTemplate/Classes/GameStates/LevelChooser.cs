using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LevelChooser : IGameState
{
    int chooseIndex = 0;
    Text[] levelName;
    Text[] shownLevelName;

    List<AbstractParticle> particles = new List<AbstractParticle>();
    Sprite mouseLight;

    public LevelChooser()
    {
        //lade den Level-Ordner 
        DirectoryInfo levelInfos = new DirectoryInfo(Constants.LEVELPATH);
        FileInfo[] test = levelInfos.GetFiles();

        levelName = new Text[test.Length];
        shownLevelName = new Text[levelName.Length / 2];

        for (int i = 0; i < test.Length; i++)
        {
            levelName[i] = new Text("" + test[i].ToString(), Assets.font);
            if (i % 2 == 0)
            {
                shownLevelName[i / 2] = new Text("Level " + (int)((i / 2) + 1), Assets.font);
                
                if (i / 2 < 10)
                    shownLevelName[i / 2].Position = new Vector2f(Constants.WINDOWWIDTH / 2 - shownLevelName[i / 2].GetGlobalBounds().Width, 120 + (i / 2) * 50);
                else
                    shownLevelName[i / 2].Position = new Vector2f(Constants.WINDOWWIDTH  / 2 - shownLevelName[i / 2].GetGlobalBounds().Width, 120 + (i / 2) * 50);

                shownLevelName[i / 2].Origin = new Vector2f(shownLevelName[i / 2].GetGlobalBounds().Width / 2, shownLevelName[i / 2].GetGlobalBounds().Height / 2);
            }
        }

        
        shownLevelName[0].Color = Color.Red;
        shownLevelName[0].Scale = new Vector2f(1.1f, 1.1f);

    }

    public void Initialize()
    {
        mouseLight = new Sprite(Assets.lightCircle);
        mouseLight.Scale = new Vector2f(8.0f, 8.0f);
        mouseLight.Origin = new Vector2f(32, 32);
        
    }

    public void LoadContent(ContentManager manager)
    {
        
    }

    public EGameState Update(GameTime gameTime, RenderWindow window)
    {

        if (Input.isClicked(Keyboard.Key.Escape))
        {
            Assets.nock.Play();
            return EGameState.MainMenu;

        }

        for (int i = 0; i < shownLevelName.Length; i++)//Text t in shownLevelName)
        {
            Text t = shownLevelName[i];

            if (Input.isInside(t.GetGlobalBounds()))
            {
                chooseIndex = i;

                if (Input.leftClicked())
                {
                    Assets.nock.Play();
                    changeToLevel();
                    return EGameState.InGame;
                }

                break;
            }
        }

        if (Input.isClicked(Keyboard.Key.S))
            chooseIndex = (chooseIndex + 1) % (levelName.Length / 2);


        else if (Input.isClicked(Keyboard.Key.W))
            chooseIndex = (chooseIndex + (levelName.Length - 1)) % (levelName.Length / 2);

        

        if (Input.isClicked(Keyboard.Key.Return))
        {
           changeToLevel();
            
            return EGameState.InGame;
        }
        changeColor(gameTime);



        particles.Add(new SparkleParticle(new Vector2f((float)InGame.random.NextDouble() * Constants.WINDOWWIDTH,
                                                            (float)InGame.random.NextDouble() * Constants.WINDOWHEIGHT)));

        //   particles.Add(new SparkleParticle(new Vector2f((float)(Constants.WINDOWWIDTH / 3 + InGame.random.NextDouble() * 2 * Constants.WINDOWWIDTH / 3), Constants.WINDOWHEIGHT / 4)));

        mouseLight.Position = new Vector2f(Input.currentMousePos.X, Input.currentMousePos.Y);

        float scaleHelp = (float)Math.Pow(Math.Sin(gameTime.TotalTime.TotalSeconds), 2);

        mouseLight.Color = Help.lerp(Color.White, Assets.AcaOrange, scaleHelp);

        scaleHelp = 8.0f + scaleHelp * 4.0f;

        mouseLight.Scale = new Vector2f(scaleHelp, scaleHelp);


        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].update(gameTime);

            if (particles[i].lifetime <= 0)
                particles.Remove(particles[i]);
        }

        return EGameState.LevelChooser;
    }
    private void changeColor(GameTime time)
    {
        resetAllColor();
        shownLevelName[chooseIndex].Color = new Color(Color.Red);
        float help = 1.0f + (float)Math.Pow(Math.Sin(time.TotalTime.TotalSeconds * 2), 2) * 0.5f;
        shownLevelName[chooseIndex].Scale = new Vector2f(help, help);

    }
    private void resetAllColor()
    {
        foreach (Text t in shownLevelName)
        {
            t.Color = new Color(Color.White);
            t.Scale = new Vector2f(1f, 1f);
        }
    }
    public void Draw(GameTime gameTime, List<RenderTexture> targets)
    {
        foreach (Text t in shownLevelName)
            targets.ElementAt(0).Draw(t);

        foreach (AbstractParticle p in particles)
            p.draw(gameTime, targets);

        targets[1].Draw(mouseLight);
    }

    private void changeToLevel()
    {
        String mapID = levelName[chooseIndex * 2].DisplayedString.Replace("map", "").Replace(".png", "").Trim();

        InGame.levelId = (LevelID)int.Parse(mapID);

    }
}

