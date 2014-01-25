using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Game
{
    public RenderWindow     window;
    public List<RenderTexture> renderTargets;

    public Sprite windowTexture;

    public GameTime         gameTime; 
    public ContentManager   contentManager;

    public static readonly Color    CornflowerBlue = new Color(101, 156, 239);
    public static readonly Color    AcaOrange = new Color(255,144,1);

    static int wheelDelta;

    public Sprite lightSprite;

    public Game(int width, int height, String title)
    {

        renderTargets = new List<RenderTexture>();
        renderTargets.Add(new RenderTexture((uint)width,(uint)height));
        renderTargets.Add(new RenderTexture((uint)width, (uint)height));
        renderTargets.Add(new RenderTexture((uint)width, (uint)height));


        window = new RenderWindow(new VideoMode((uint)width, (uint)height), title);

        window.Closed += closeHandler;
        window.MouseWheelMoved += mouseWheelHandler;
        
        window.SetVerticalSyncEnabled(false);
        window.SetFramerateLimit(60);

        contentManager = new ContentManager();
        gameTime = new GameTime();
    }


    private void mouseWheelHandler(object sender, MouseWheelEventArgs e)
    {
        wheelDelta = e.Delta;
    }

    private void closeHandler(object sender, EventArgs e)
    {
        window.Close();
    }

    public void run()
    {
        gameTime.Start();
        loadContent(contentManager);

        
        lightSprite = new Sprite();
        lightSprite.Texture = Assets.lightCircle;
        lightSprite.Scale = new Vector2f(4, 4);
        lightSprite.Origin = new Vector2f(32, 32);
        lightSprite.Position = new Vector2f(300, 300);
        lightSprite.Color = Color.White;


        while (window.IsOpen())
        {
            
            wheelDelta = 0;
            window.DispatchEvents();
            Input.update();
            gameTime.Update();

            lightSprite.Color = Help.lerp(Color.Red, Color.Yellow, (float)Math.Pow(Math.Sin(gameTime.TotalTime.TotalSeconds), 2));
            Vector2i mousePos = Input.currentMousePos;
            lightSprite.Position = new Vector2f(mousePos.X, mousePos.Y);
            update(gameTime, window);


            for (int i = 0; i < renderTargets.Count; i++)
                if (i == 2)
                    renderTargets[i].Clear(Color.Transparent);
                else
                    renderTargets[i].Clear();


            draw(gameTime, renderTargets);
            renderTargets[1].Draw(lightSprite);

            foreach (RenderTexture texture in renderTargets)
                texture.Display();


            if (InGame.isLevelDark)
            {
                RenderStates s = (ShaderManager.getRenderState(EShader.Dark));
                s.Shader.SetParameter("Texture1", renderTargets[1].Texture);
                renderTargets[0].Draw(new Sprite(renderTargets.ElementAt(0).Texture), s);
            }
            else
                renderTargets[0].Draw(new Sprite(renderTargets.ElementAt(0).Texture));

            if (InGame.isPaused && Math.Sin(gameTime.TotalTime.TotalMilliseconds*2) > 0.1)
            {
                RenderStates s = ShaderManager.getRenderState(EShader.Grayscale);
                s.Shader.SetParameter("time", (float)gameTime.TotalTime.TotalMilliseconds / 2);
                window.Draw(new Sprite(renderTargets[0].Texture),s);
            }
            else
                window.Draw(new Sprite(renderTargets[0].Texture));

            //draw interface
            window.Draw(new Sprite(renderTargets.ElementAt(2).Texture));
           
            window.Display();
        }
    }
    
    public abstract void update(GameTime gameTime, RenderWindow window);

    public abstract void draw(GameTime gameTime, List<RenderTexture> RenderTextures);

    public abstract void loadContent(ContentManager content);

    public static bool mouseWheelUp()
    {
        return wheelDelta == 1;
    }

    public static bool mouseWheelDown()
    {
        return wheelDelta == -1;
    }

}

