using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public UIFishing UIFishing;

    // Start is called before the first frame update
    public Animator playerAnim;
    public bool isFishing;
    public bool poleBack;
    public bool throwBobber;
    public Transform fishingPoint;
    public GameObject bobber;

    public float targetTime = 0.0f;
    public float savedTargetTime;
    public float extraBobberDistance;

    public GameObject fishGame;

    public float timeTillCatch = 0.0f;
    public bool winnerAnim;

    public int countFish;

    void Start()
    {
        isFishing = false;
        fishGame.SetActive(false);
        throwBobber = false;
        targetTime = 0.0f;
        savedTargetTime = 0.0f;
        extraBobberDistance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isFishing == false && winnerAnim == false)
        {
            poleBack = true;
        }
        if (isFishing == true)
        {
            timeTillCatch += Time.deltaTime;
            if (timeTillCatch >= 3)
            {
                fishGame.SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isFishing == false && winnerAnim == false)
        {
            poleBack = false;
            isFishing = true;
            throwBobber = true;
            if (targetTime >= 3)
            {
                extraBobberDistance += 3;
            }
            else
            {
                extraBobberDistance += targetTime;
            }
        }

        Vector3 temp = new Vector3(extraBobberDistance, 0, 0);
        fishingPoint.transform.position += temp;

        if (poleBack == true)
        {
            playerAnim.Play("PlayerSwingBack");
            savedTargetTime = targetTime;
            targetTime += Time.deltaTime;
        }

        if (isFishing == true)
        {
            if (throwBobber == true)
            {
                Instantiate(bobber, fishingPoint.position, fishingPoint.rotation, transform);
                fishingPoint.transform.position -= temp;
                throwBobber = false;
                targetTime = 0.0f;
                savedTargetTime = 0.0f;
                extraBobberDistance = 0.0f;
            }
            playerAnim.Play("PlayerFishing");
        }

        if (Input.GetKeyDown(KeyCode.P) && timeTillCatch <= 3)
        {
            playerAnim.Play("PlayerStill");
            poleBack = false;
            throwBobber = false;
            isFishing = false;
            timeTillCatch = 0;
        }

    }

    public void fishGameWon()
    {
        countFish++;
        playerAnim.Play("PlayerWonFish");
        
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;

        if (countFish >= 2)
        {
            UIFishing.ShowSuccessMenu();
        }
    }
    public void fishGameLossed()
    {
        playerAnim.Play("PlayerStill");
        fishGame.SetActive(false);
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;
    }


}