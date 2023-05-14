using System.Numerics;

namespace chip8;

public class Preferences
{
    public Vector3 DisplayForegroundColor = new Vector3(1, 1, 1);
    public Vector3 DisplayBackgroundColor = new Vector3(0, 0, 0);
    public int DisplayWidth = 32;
    public int DisplayHeight = 16;
}