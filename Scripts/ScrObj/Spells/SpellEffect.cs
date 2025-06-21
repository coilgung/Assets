using UnityEngine;

[CreateAssetMenu(fileName = "SpellEffect", menuName = "Scriptable Objects/SpellEffect")]
public abstract class SpellEffect : ScriptableObject
{
    [SerializeField]
    protected int duration;

    public abstract void OnHit(PlayerScript player);

    public abstract void OnTurn(PlayerScript player);

    public abstract void OnCast(PlayerScript player);
}
