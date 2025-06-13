using UnityEngine;

[CreateAssetMenu(fileName = "StarterDeckScript", menuName = "Scriptable Objects/StarterDeckScript")]
public class StarterDeck : ScriptableObject
{
    public ColorScript[] colorScript;
    public int[] maxCards;
}
