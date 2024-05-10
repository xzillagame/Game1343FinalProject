using UnityEngine;

public class ShrinkBuff : BuffBase
{
    public override void ApplyBuff(PlayerRaceLogic playerToApplyBuff)
    {
        playerToApplyBuff.BeginShrinkBuff();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject, 0);
    }
}
