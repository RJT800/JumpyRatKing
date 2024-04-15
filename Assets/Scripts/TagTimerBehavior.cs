using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Timers;



public class TagTimerBehavior : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;


    private void Awake()
    {
        //get text?
        float time = 160;


        _text.text = "TIme remaing: "+ time.ToString();
    }

    private void Start()
    {
        
    }

    public void SwitchTimer()
    {
        
    }


}
