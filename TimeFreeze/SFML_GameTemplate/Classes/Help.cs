using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Help
{

    public static Color lerp(Color color1, Color color2, float t)
    {
        return new Color(   (byte)(color1.R - color1.R * t + color2.R * t), 
                            (byte)(color1.G - color1.G * t + color2.G * t), 
                            (byte)(color1.B - color1.B * t + color2.B * t), 
                            (byte)(color1.A - color1.A * t + color2.A * t));
    }

    public static Vector2f lerp(Vector2f first, Vector2f sec, float t)
    {
           return new Vector2f( (first.X - first.X * t + sec.X * t), 
                                (first.Y - first.Y * t + sec.Y * t));
    }

    public static Vector2f toVec2f(Vector2 vec)
    {
        return new Vector2f(vec.X, vec.Y);
    }

    public static float Dot(Vector2f v1, Vector2f v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y;
    }
    public static float toRadian(float degree)
    {
        return degree * ((float)Math.PI / 180.0f);
    }

    public static float toDegree(float radian)
    {
        return radian * (180.0f / (float)Math.PI);
    }

    public static Color multiply(Color color, float t)
    {
        return new Color((byte)(color.R * t), (byte)(color.G * t), (byte)(color.B * t), (byte)(color.A * t));
    }

    public static Color fade(Color c, float t)
    {
        return new Color(c.R, c.G , c.B, (byte)(c.A * t));
    }

    public static float length(Vector2f v)
    {
        return (float)Math.Sqrt(v.X * v.X + v.Y + v.Y);
    }

    public static Vector2f normalized(Vector2f v)
    {
        float leng = length(v);
        return new Vector2f(v.X / leng, v.Y / leng);
    }
}
