using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSpell", menuName = "Scriptable Objects/ShieldSpell")]
public class ShieldSpell : SpellEffect
{
    public override void OnHit(PlayerScript caster, PlayerScript target, SpellEffect spell)
    {
        spell.BlockSpell();
        this.duration = 0;
    }

    public override void OnTurn(PlayerScript caster, PlayerScript target, SpellEffect spell)
    {
        this.duration = 0;
    }

    public override void OnCast(PlayerScript caster, PlayerScript target, SpellEffect spell) {}
}
