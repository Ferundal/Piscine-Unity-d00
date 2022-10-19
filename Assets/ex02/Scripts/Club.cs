using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] GameObject hole;
    [SerializeField] float distanceToBall = 1.0f;
    [SerializeField] float minVelocity = 0.0f;
    [SerializeField] float maxVelocity = 4.0f;
    [SerializeField] float swingPowerSpeedModificator = 4.0f;
    private float swingVelocity;
    private Vector3 preSwingPosition;

    private bool isSwinging = false;
    private float swingStartTime;
    private bool b_IsGameActive;
    private Vector3 directionToBall;
    private int points = -20;
    public bool IsGameActive
    {
        get
        {
            return b_IsGameActive;
        }
        set
        {
            if (value == true)
            {
                MoveToBall();
            }
            b_IsGameActive = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        IsGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameActive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (!isSwinging)
                {
                    swingStartTime = Time.timeSinceLevelLoad;
                    preSwingPosition = transform.position;
                    isSwinging = true;
                }
                swingVelocity = Mathf.Clamp(Time.timeSinceLevelLoad - swingStartTime, 0, 1);
                transform.position = preSwingPosition - directionToBall * swingVelocity;
            }
            else if (isSwinging == true)
            {
                ball.GetVelocity(directionToBall, Mathf.Clamp(swingVelocity * swingPowerSpeedModificator, minVelocity, maxVelocity));
                transform.position = preSwingPosition;
                isSwinging = false;
            }
        }
    }

    public void MoveToBall()
    {
        directionToBall = (hole.transform.position - ball.transform.position).normalized;
        this.transform.position = ball.transform.position - directionToBall * distanceToBall;
        points += 5;
        Debug.Log("Score: " + points);
        if (points == 0)
        {
            Debug.Log("You lose!");
        }
    }
}
