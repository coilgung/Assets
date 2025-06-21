using UnityEngine;

[CreateAssetMenu(fileName = "SpellEffect", menuName = "Scriptable Objects/SpellEffect")]
public abstract class SpellEffect : ScriptableObject
{
    [SerializeField]
    protected int duration;

    public abstract void OnHit(PlayerScript caster, PlayerScript target, SpellEffect spell);

    public abstract void OnTurn(PlayerScript caster, PlayerScript target, SpellEffect spell);

    public abstract void OnCast(PlayerScript caster, PlayerScript target, SpellEffect spell);

    public void BlockSpell()
    {
        this.duration = 0;
    }
}
