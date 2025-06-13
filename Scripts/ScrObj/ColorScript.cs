using UnityEngine;

public enum CardColor
{
    RED = 0,
    YELLOW = 1,
    BLUE =2
}

[CreateAssetMenu(fileName = "Color", menuName = "Scriptable Objects/Color")]
public class ColorScript : ScriptableObject
{
    public Sprite sprite;
    public CardColor color;

}
