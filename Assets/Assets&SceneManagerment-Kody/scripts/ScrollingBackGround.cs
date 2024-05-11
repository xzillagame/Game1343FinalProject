using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackGround : MonoBehaviour
{
    [SerializeField] RawImage image;

    [SerializeField] PlayerRaceLogic player;

    [SerializeField] private float MaxRaceSpeed = 2.5f;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(player != null)
        {
            float percantageOfPlayerSpeed = player.CurrentSpeed / player.MaxSpeed;
            float lepredValue = Mathf.Lerp(0f, MaxRaceSpeed, percantageOfPlayerSpeed);
            image.uvRect = new Rect(image.uvRect.position + new Vector2(0, lepredValue) * Time.deltaTime, image.uvRect.size);
        }
        else
        {
            image.uvRect = new Rect(image.uvRect.position + new Vector2(0, speed) * Time.deltaTime, image.uvRect.size);
        }
    }
}
