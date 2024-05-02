using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Timers;



public class TagTimerBehavior : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _player1Timertext;
    [SerializeField]
    private TextMeshProUGUI _player2Timertext;

    [SerializeField]
    private PlayerTagBehavior _player1;

    [SerializeField]
    private PlayerTagBehavior _player2;

    private float _player1timer = Mathf.Clamp(60, 0, 60);
    private float _player2timer = Mathf.Clamp(60, 0, 60);


    float time = 60;



    private void Update()
    {
        if (_player1.IsTagged)
            _player1timer -= Time.deltaTime;
        else
            _player2timer -= Time.deltaTime;


        //update text
        _player1Timertext.text = ((int)_player1timer).ToString();
        _player2Timertext.text = ((int)_player2timer).ToString();

        // when one of the timers reaches zwero,
        //display some text thT says who won.

        if (_player1timer <= 0)
        {
            _player2Timertext.text = "Player 2 Wins!";
            _player1Timertext.text = "Player 1 Loses!";
        }
        else if (_player2timer <= 0)
        {
            _player1Timertext.text = "Player 1 Wins!";
            _player2Timertext.text = "Player 2 Loses!";
        }

    }
}
