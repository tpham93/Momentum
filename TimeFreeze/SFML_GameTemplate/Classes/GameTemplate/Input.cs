using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Input
{
    static Window window;
    static List<Keyboard.Key> usedKeys;

    static bool[] oldKeys;
    static bool[] currentKeys;

    static bool[] oldMouse;
    static bool[] currentMouse;

    public static Vector2i oldMousePos;
    public static Vector2i currentMousePos;

    public static void init(Window window, List<Keyboard.Key> usedKeys)
    {
        Input.window = window;
        Input.usedKeys = usedKeys;
        
        oldKeys = new bool[(int)Keyboard.Key.KeyCount];
        currentKeys = new bool[(int)Keyboard.Key.KeyCount];

        oldMouse = new bool[(int)Mouse.Button.ButtonCount];
        currentMouse = new bool[(int)Mouse.Button.ButtonCount];
    }

    public static void update()
    {
        foreach(Keyboard.Key key in usedKeys)
            oldKeys[(int)key] = currentKeys[(int)key];

        //update every Key that is needed:
        foreach (Keyboard.Key key in usedKeys)
            if (Keyboard.IsKeyPressed(key))
                currentKeys[(int)key] = true;
            else
                currentKeys[(int)key] = false;

        oldMousePos = currentMousePos;
        currentMousePos = Mouse.GetPosition(window);


        for (int i = 0; i < oldMouse.Length; i++)
            oldMouse[i] = currentMouse[i];

        //update every mouseKey:
        for (int i = 0; i < oldMouse.Length; i++)
            if (Mouse.IsButtonPressed((Mouse.Button)i))
                currentMouse[i] = true;
            else
                currentMouse[i] = false;   
    }

    public static bool isClicked(Keyboard.Key key)
    {
        return currentKeys[(int)key] && !oldKeys[(int)key];
    }

    public static bool isPressed(Keyboard.Key key)
    {
        return Keyboard.IsKeyPressed(key);
    }

    public static bool isReleased(Keyboard.Key key)
    {
        return oldKeys[(int)key] && !Keyboard.IsKeyPressed(key);
    }



    public static bool leftClicked()
    {
        return !oldMouse[(int)Mouse.Button.Left] && currentMouse[(int)Mouse.Button.Left];
    }

    public static bool leftPressed()
    {
        return currentMouse[(int)Mouse.Button.Left];
    }

    public static bool leftReleased()
    {
        return oldMouse[(int)Mouse.Button.Left] && !currentMouse[(int)Mouse.Button.Left];
    }


}

