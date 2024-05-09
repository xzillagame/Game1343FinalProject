using UnityEngine;

public class ProjectileMissle : MonoBehaviour
{
    [SerializeField] private float chaseSpeed;
    private float offSet = 1;
    private Transform player;

    private void Update()
    {
        if (transform.position.y - offSet > player.position.y)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg);
            transform.position += (transform.position.x <= player.position.x ? chaseSpeed : -chaseSpeed) * Vector3.right * Time.deltaTime;
        }
    }
    public void SetPlayerTransform(Transform player)
    { 
        this.player = player;
    }
}
