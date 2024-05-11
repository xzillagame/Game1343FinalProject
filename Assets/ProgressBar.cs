using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.PlasticSCM.Editor.WebApi; // Required when Using UI elements.

public class Progressbar : MonoBehaviour
{

    [SerializeField] GameObject playerGO;
    [SerializeField] GameObject finishGO;
    [SerializeField] Slider progressSlider;
    Image Slider;
    float maxDistance;
    void Start()
    {

        Slider = GetComponent<Image>();
        maxDistance = finishGO.transform.position.z;
    }

    public void ChangeValue()
    {
        progressSlider.value = Slider.fillAmount;

        Slider.fillAmount = playerGO.transform.position.z / maxDistance;


      }
        
        
   }

