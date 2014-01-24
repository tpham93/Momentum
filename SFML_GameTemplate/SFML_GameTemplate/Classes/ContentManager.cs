using SFML;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ContentManager
{
    List<ObjectBase> list;

    public ContentManager()
    {
        this.list = new List<ObjectBase>();
    }

    public void load(ref Font font, String path)
    {
        font = new Font(path);
        list.Add(font);
    }

    public void load(ref Texture texture, String path)
    {
        texture = new Texture(path);
        list.Add(texture);
    }

    public void disposeAll()
    {
        foreach (ObjectBase b in list)
            b.Dispose();

        list = new List<ObjectBase>();
    }

}

