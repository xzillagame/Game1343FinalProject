using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuff : BuffBase
{
    public override void ApplyBuff(PlayerRaceLogic playerToApplyBuff)
    {
        playerToApplyBuff.BeginDefenseBuff();
    }
}
