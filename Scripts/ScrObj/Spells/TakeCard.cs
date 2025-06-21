using UnityEngine;

[CreateAssetMenu(fileName = "TakeCard", menuName = "Scriptable Objects/TakeCard")]
public class TakeCard : SpellEffect
{
    public override void OnHit(PlayerScript player) { }

    public override void OnTurn(PlayerScript player) { }

    public override void OnCast(PlayerScript player)
    {
        player.TakeCard();
        this.duration--;
    }
}
