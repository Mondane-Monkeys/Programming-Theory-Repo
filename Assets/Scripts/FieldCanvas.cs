using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldCanvas : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text dayText;
    public int days;
    private int m_money;
    public int money
    {
        set
        {
            m_money = value;
            SetMoneyText();
        }
        get
        {
            return m_money;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadValues();
        SetMoneyText();
        SetDayText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetMoneyText()
    {
        moneyText.text = "Money: " + money;
    }

    void SetDayText()
    {
        dayText.text = "Day: " + days;
    }

    void LoadValues()
    {
        int[] values = IOInterface.LoadValues();
        if (values != null)
        {
            days = values[0];
            money = values[1];
        }
        else
        {
            days = 0;
            money = 500; //default value
        }
    }
}
