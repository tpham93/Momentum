using SFML.Graphics;
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
}
