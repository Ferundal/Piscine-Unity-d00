using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaloonController baloonController;
    [SerializeField] private float breathAmount = 10;
    [SerializeField] private float breathCost = 2;
    [SerializeField] private float timeToCatchBreath = 2;
    [SerializeField] private TMP_Text breathText;
    private float lastBlowUp;

    private bool b_IsGameOver = false;
    public bool IsGameOver {
        get
        {
            return b_IsGameOver;
        }
        set
        {
            b_IsGameOver = value;
            if (value == true)
            {
                Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.timeSinceLevelLoad) + "s");
            }
        }
    }
    private void Awake()
    {
        lastBlowUp = -timeToCatchBreath;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Blow();
            }
            if (breathAmount < 10)
            {
                breathAmount += 1 / timeToCatchBreath * Time.deltaTime;
            }
        }
        breathText.text = breathAmount.ToString();
    }

    private void Blow()
    {
        if (breathAmount > breathCost)
        {
            baloonController.BlowUp();
            float breathDealay = Time.timeSinceLevelLoad - lastBlowUp;
            if (breathDealay > timeToCatchBreath)
            {
                breathDealay = timeToCatchBreath;
            }
            breathAmount -= breathCost * (1 + timeToCatchBreath - breathDealay);
        }
    }
}
