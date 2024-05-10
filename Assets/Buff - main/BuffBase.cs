using UnityEngine;

public class BuffBase : MonoBehaviour
{
    public virtual void ApplyBuff(PlayerRaceLogic playerToApplyBuff) 
    {
        Debug.Log("Base Buff Class, ApplyBuff function called. ApplyBuff function needs to be overriden");
    }

}
