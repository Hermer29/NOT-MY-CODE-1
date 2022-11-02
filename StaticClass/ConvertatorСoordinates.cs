
using UnityEngine;

public static class ConvertatorСoordinates
{
    public static Vector2 Convert(int x, int y)
    {
        x = -x;
        y = -y;

        return new Vector2(x, y);
    }
}
