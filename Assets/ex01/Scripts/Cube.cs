using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float moveDownSpeed = 2.0f;
    private Vector3 newPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        newPosition = this.transform.position;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        newPosition.y -= moveDownSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
