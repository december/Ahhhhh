﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class GameManager : Singleton<GameManager> {

    // 双方初始血量
    public int initialHP = 10;

    public Player WangWang;
    public Player XianYu;

    [SerializeField]
    private GameObject SoundBarLeft;
    [SerializeField]
    private GameObject SoundBarRight;

    [SerializeField]
    private Wind wind;

    public float recordTime = 3f;
    public float waitTime = 15f;

	IEnumerator WaitSeconds()
	{
		yield return new WaitForSeconds (5f);
	}

    public void StartGame()
    {
		Debug.Log ("Started.");

		UIManager.instance.ResetHP (initialHP);
		WangWang.HP = initialHP;
		XianYu.HP = initialHP;
		int r = Random.Range (0, 2);
		if (r == 0)
			fsm.ChangeState (States.Left);
		else
			fsm.ChangeState (States.Right);
		
        

		UIManager.instance.playingUI.SetActive(true);
		UIManager.instance.startUI.SetActive(false);
		Debug.Log (fsm.State);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public void CheckRoundEnd()
    {
		Debug.Log ("I'm check.");
		Debug.Log (fsm.State);
        if (WangWang.HP <= 0 || XianYu.HP <= 0)
        {
            fsm.ChangeState(States.End);
        }
        else
        {
            if (fsm.State == States.Left)
            {
                fsm.ChangeState(States.Right);
            }
            else if (fsm.State == States.Right)
            {
                fsm.ChangeState(States.Left);
            }
        }
    }

    private void ChangeWindDirection()
    {
        float angle = Random.Range(0f, 360f);
        wind.SetWindDirection(angle);
    }

    public enum States
    {
        Init,
        Left,
        Right,
        End
    }
    public StateMachine<States> fsm;

    void Start()
    {
		
        fsm = StateMachine<States>.Initialize(this);
        fsm.ChangeState(States.Init);
    }

    void Init_Exit()
    {
        UIManager.instance.startUI.SetActive(false);
        UIManager.instance.playingUI.SetActive(true);
    }

    void Left_Enter()
    {
		if (WangWang.HP <= 0 || XianYu.HP <= 0)
			fsm.ChangeState(States.End);
        ChangeWindDirection();
        UIManager.instance.recordButtonLeft.SetActive(true);
        /*yield return new WaitForSeconds(prepareTime);
        SoundBarLeft.SetActive(true);
        yield return new WaitForSeconds(speakTime);
        float result = 0;
        for (int i = 0; i < 5; ++i)
        {
            result += SoundDetect.instance.currentVolume;
            yield return null;
        }
        result = result / 5;
        SoundBarLeft.SetActive(false);

        Throw.instance.ThrowStuff(result, Player.Left);

        yield return new WaitForSeconds(waitTime);
        if (playerLeftHP <= 0 || playerRightHP <= 0)
        {
            fsm.ChangeState(States.End);
        }
        else
        {
            fsm.ChangeState(States.Right);
        }*/
    }

    void Left_Exit()
    {
        UIManager.instance.recordButtonLeft.SetActive(false);
    }

    void Right_Enter()
    {
		if (WangWang.HP <= 0 || XianYu.HP <= 0)
			fsm.ChangeState(States.End);
        ChangeWindDirection();
        UIManager.instance.recordButtonRight.SetActive(true);
        /*yield return new WaitForSeconds(prepareTime);
        SoundBarRight.SetActive(true);
        yield return new WaitForSeconds(speakTime);
        float result = 0;
        for (int i = 0; i < 5; ++i)
        {
            result += SoundDetect.instance.currentVolume;
            yield return null;
        }
        result = result / 5;
        SoundBarRight.SetActive(false);

        Throw.instance.ThrowStuff(result, Player.Right);

        yield return new WaitForSeconds(waitTime);
        if (playerLeftHP <= 0 || playerRightHP <= 0)
        {
            fsm.ChangeState(States.End);
        }
        else
        {
            fsm.ChangeState(States.Left);
        }*/
    }

    void Right_Exit()
    {
        UIManager.instance.recordButtonRight.SetActive(false);
    }

    void End_Enter()
    {
        UIManager.instance.playingUI.SetActive(false);
		//StartCoroutine (WaitSeconds());
		UIManager.instance.startUI.SetActive(true);
		//fsm = StateMachine<States>.Initialize(this);
		//fsm.ChangeState (States.Init);
    }
}
