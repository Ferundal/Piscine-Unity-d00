using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Pipe pipeOne;
    [SerializeField] private Pipe pipeTwo;

    [SerializeField] private GameObject topLeftPoint;
    [SerializeField] private GameObject bottomRightPoint;
    [SerializeField] private float startVerticalVelocity = 2.0f;
    [SerializeField] private float birdGravity = 2.0f;
    [SerializeField] private float verticalBuff = 5.0f;
    private float verticalVelocity;
    private Vector3 birdPosition;
    public float TopBorder
    {
        get
        {
            return topLeftPoint.transform.position.y;
        }
    }
    public float BottomBorder
    {
        get
        {
            return bottomRightPoint.transform.position.y;
        }
    }
    public float RightBorder
    {
        get
        {
            return bottomRightPoint.transform.position.x;
        }
    }
    public float LeftBorder
    {
        get
        {
            return topLeftPoint.transform.position.x;
        }
    }

    public float BirdSpeed { get; private set; } = 2;
    [SerializeField] private float pipeAcceleration = 1;

    private int score = 0;
    private bool b_IsGameOver = false;
    public bool IsGameOver
    {
        get
        {
            return b_IsGameOver;
        }
        set
        {
            if (value == true)
            {
                Debug.Log("Score: " + score);
                Debug.Log("Time: " + Mathf.RoundToInt(Time.realtimeSinceStartup));
            }
            b_IsGameOver = value;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        verticalVelocity = startVerticalVelocity;
        birdPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (!IsGameOver)
        {
            if (isCollideWith(pipeOne) || isCollideWith(pipeTwo))
            {
                IsGameOver = true;
            }
            else
            {
                Debug.Log("Test");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    verticalVelocity += verticalBuff;
                }
                birdPosition.y += verticalVelocity * Time.deltaTime;
                transform.position = birdPosition;
                verticalVelocity -= birdGravity * Time.deltaTime;
            }
        }
    }
    private bool isCollideWith(Pipe pipe)
    {
        if ((this.LeftBorder < pipe.RightBorder && this.RightBorder > pipe.LeftBorder)
            && (this.TopBorder > pipe.TopBorder || this.BottomBorder < pipe.BottomBorder))
        {
            return true;
        }
        return false;
    }
    public void IncreaseScore(int points)
    {
        BirdSpeed += pipeAcceleration;
        score += points;
    }
}
