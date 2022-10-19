using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float inertion = 1;
    [SerializeField] private float velocityToEnterHole = 0.1f;
    [SerializeField] private float distanceToEnterHole = 0.1f;
    [SerializeField] private Club club;
    [SerializeField] private GameObject hole;
    [SerializeField] private GameObject topBorder;
    [SerializeField] private GameObject bottomBorder;
    private Vector3 velocityDirection;
    private float velocity;
    public bool IsMoving
    {
        get
        {
            if (velocity == 0)
            {
                return false;
            }
            return true;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        velocityDirection = Vector3.zero;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (velocity != 0)
        {
            transform.Translate(velocityDirection * velocity * Time.deltaTime);
            velocity -=  inertion * Time.deltaTime;
            if (velocity <= 0)
            {
                velocity = 0;
                club.MoveToBall();
            } else
            {
                float diff = transform.position.y - hole.transform.position.y;
                if (((diff > 0 && diff < distanceToEnterHole) || (diff < 0 && diff > -distanceToEnterHole))
                    && velocity < velocityToEnterHole)
                {
                    club.IsGameActive = false;
                    Destroy(this.gameObject);
                }
            }
        }
        if (transform.position.y > topBorder.transform.position.y || transform.position.y < bottomBorder.transform.position.y)
        {
            velocityDirection.y = -velocityDirection.y;
        }
    }

    public void GetVelocity(Vector3 velocityDirection, float velocity)
    {
        this.velocityDirection = velocityDirection;
        this.velocity = velocity;
    }
}
