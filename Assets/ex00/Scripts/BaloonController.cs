using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviour
{
    [SerializeField] private float maxBaloonSize = 10;
    [SerializeField] private float minBaloonSize = 1;
    [SerializeField] private float blowUpSpeed = 4;
    [SerializeField] private float blowDownSpeed = 2;
    [SerializeField] private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > maxBaloonSize || transform.localScale.x < minBaloonSize)
        {
            gameManager.IsGameOver = true;
            Destroy(this.gameObject);
        }
        else
        {
            transform.localScale -= blowDownSpeed * Time.deltaTime * Vector3.one;
        }
    }

    public void BlowUp ()
    {
        transform.localScale += Vector3.one * blowUpSpeed;
    }
}
