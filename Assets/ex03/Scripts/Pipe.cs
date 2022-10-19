using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Bird bird;
    [SerializeField] private GameObject pipeSpawnPosition;
    [SerializeField] private GameObject pipeReusePosition;
    [SerializeField] private GameObject topLeftPoint;
    [SerializeField] private GameObject bottomRightPoint;

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
    private bool isPipePassed = false;
    private Vector3 spawnPosition;
    private Vector3 newPosition;
    private void Awake()
    {
        newPosition = transform.position;
        spawnPosition = transform.position;
        spawnPosition.x = pipeSpawnPosition.transform.position.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bird.IsGameOver)
        {
            if (transform.position.x < pipeReusePosition.transform.position.x)
            {
                ReusePipe();
            }
            else if (transform.position.x < bird.transform.position.x && !isPipePassed)
            {
                isPipePassed = true;
                bird.IncreaseScore(1);
            }
            newPosition.x -= bird.BirdSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    private void ReusePipe() {
        newPosition = spawnPosition;
        isPipePassed = false;
    }
}
