using UnityEngine;

[CreateAssetMenu(fileName = "SkipTurn", menuName = "Scriptable Objects/SkipTurn")]
public class SkipTurn : SpellEffect
{
    public override void OnHit(PlayerScript caster, PlayerScript target, SpellEffect spell) {}

    public override void OnTurn(PlayerScript caster, PlayerScript target, SpellEffect spell)
    {
        target.EndTurn();
        this.duration = 0;
    }

    public override void OnCast(PlayerScript caster, PlayerScript target, SpellEffect spell) {}
}
