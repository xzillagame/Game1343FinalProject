using UnityEngine;

public class ShrinkBuff : BuffBase
{
    public override void ApplyBuff(PlayerRaceLogic playerToApplyBuff)
    {
        playerToApplyBuff.BeginShrinkBuff();
    }
}
