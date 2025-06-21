using UnityEngine;

[CreateAssetMenu(fileName = "TakeCard", menuName = "Scriptable Objects/TakeCard")]
public class TakeCard : SpellEffect
{
    public override void OnHit(PlayerScript caster, PlayerScript target, SpellEffect spell) { }

    public override void OnTurn(PlayerScript caster, PlayerScript target, SpellEffect spell) { }

    public override void OnCast(PlayerScript caster, PlayerScript target, SpellEffect spell)
    {
        target.TakeCard();
        this.duration = 0;
    }
}
