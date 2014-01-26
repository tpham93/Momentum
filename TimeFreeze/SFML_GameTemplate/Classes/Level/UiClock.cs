using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UiClock
{
    Sprite body;

    Sprite bodyInner;

  //  Sprite watchHandMin;
    Sprite watchHandSec;

    Sprite body2;

    Sprite bodyInner2;

 //   Sprite watchHandMin2;
    Sprite watchHandSec2;

    Color color         = new Color(255, 255, 255, 200);
    Color col2          = new Color(255,255,255,250);

    Color col3 = new Color(255,255,255,255);

    Vector2f ori        = new Vector2f(160,160);
    Vector2f scale      = new Vector2f(0.25f, 0.25f);
    Vector2f pos;

    public UiClock()
    {
        pos = new Vector2f(Constants.WINDOWWIDTH/ 2, 0);

        bodyInner = new Sprite(Assets.clockBodyInner);
        bodyInner.Origin = ori;
        bodyInner.Scale = scale;
        bodyInner.Color = col2;
        bodyInner.Position = pos;


        body = new Sprite(Assets.clockBody);
        body.Origin = ori;
        body.Scale = scale;
        body.Color = color;
        body.Position = pos;

        /*
        watchHandMin = new Sprite(Assets.watchHandMin);
        watchHandMin.Origin = ori;
        watchHandMin.Scale = scale;
        watchHandMin.Color = col2;
        watchHandMin.Position = pos;
        */
        watchHandSec = new Sprite(Assets.watchHandSec);
        watchHandSec.Origin = ori;
        watchHandSec.Scale = scale;
        watchHandSec.Color = col2;
        watchHandSec.Position = pos;


        bodyInner = new Sprite(Assets.clockBodyInner);
        bodyInner.Origin = ori;
        bodyInner.Scale = scale;
        bodyInner.Color = col2;
        bodyInner.Position = pos;

        ///_______________________________________________________________________//

        body2 = new Sprite(Assets.clockBody);
        body2.Origin = ori;
        body2.Scale = scale;
        body2.Color = color;
        body2.Position = pos;

        /*
        watchHandMin2 = new Sprite(Assets.watchHandMin);
        watchHandMin2.Origin = ori;
        watchHandMin2.Scale = scale;
        watchHandMin2.Color = col2;
        watchHandMin2.Position = pos;
        */
        watchHandSec2 = new Sprite(Assets.watchHandSec);
        watchHandSec2.Origin = ori;
        watchHandSec2.Scale = scale;
        watchHandSec2.Color = col2;
        watchHandSec2.Position = pos;

        bodyInner2 = new Sprite(Assets.clockBodyInner);
        bodyInner2.Origin = ori;
        bodyInner2.Scale = scale;
        bodyInner2.Color = col2;
        bodyInner2.Position = pos;
    }

    public void update(GameTime gameTime)
    {

        if (!InGame.isPaused && !InGame.isLevelFreezed)
        {
           // watchHandSec.Rotation += Help.toRadian((float)gameTime.ElapsedTime.TotalMilliseconds / 4) * 60;
            //watchHandMin.Rotation += Help.toRadian((float)gameTime.ElapsedTime.TotalMilliseconds / 4);

            watchHandSec.Rotation = 180.0f + (float)Math.Sin(gameTime.TotalTime.TotalSeconds) * 90.0f;

            body.Scale = scale;
            bodyInner.Scale = scale;

        }
        else
        {
            double time = Math.Pow(Math.Sin(gameTime.TotalTime.TotalSeconds), 2);
            Color helpCol = new Color((byte)255,(byte)255, (byte)255, (byte)(time*200));

            Vector2f scaleHelp = new Vector2f(scale.X + (float)time* 0.05f, scale.Y + (float)time * 0.05f);

     //       watchHandMin2.Rotation = watchHandMin.Rotation;
            watchHandSec2.Rotation = watchHandSec.Rotation;

            body2.Color = helpCol;
            bodyInner2.Color = helpCol;

       //     watchHandMin2.Color = helpCol;
            watchHandSec2.Color = helpCol;

            body2.Scale = scaleHelp;
            bodyInner2.Scale = scaleHelp;

         //   watchHandMin2.Scale = scaleHelp;
            watchHandSec2.Scale = scaleHelp;
        }
    }

    public void draw(List<RenderTexture> targets, GameTime gameTime)
    {
        targets[2].Draw(body);
        targets[2].Draw(bodyInner);
      //  targets[2].Draw(watchHandMin);
        targets[2].Draw(watchHandSec);

        if (InGame.isPaused || InGame.isLevelFreezed)
        {
            targets[2].Draw(body2);
            targets[2].Draw(bodyInner2);
         //   targets[2].Draw(watchHandMin2);
            targets[2].Draw(watchHandSec2);
        }
    }
}
