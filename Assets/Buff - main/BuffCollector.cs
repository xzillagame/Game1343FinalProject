using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRaceLogic))]
public class BuffCollector : MonoBehaviour
{

    PlayerRaceLogic player;

    private void Start()
    {
        player = GetComponent<PlayerRaceLogic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<BuffBase>(out BuffBase buff))
        {
            buff.ApplyBuff(player);
        }
    }
}
