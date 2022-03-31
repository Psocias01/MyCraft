using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image ImgHealthBar;
    public Text TxtHealth;
    public int Min;
    public int Max;
    private int mCurrentValue;
    private float mCurrentPercentage;

    public void SetHealth(int health)
    {
        if (health != mCurrentValue)
        {
            if (Max - Min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercentage = 0;
            }
            else
            {
                mCurrentValue = health;

                mCurrentPercentage = (float) mCurrentValue / (float) (Max - Min);
            }

            TxtHealth.text = string.Format("{0} %", Mathf.RoundToInt(mCurrentPercentage * 100));

            ImgHealthBar.fillAmount = mCurrentPercentage;
        }
    }

    public float CurrentPercent
    {
        get { return mCurrentPercentage; }
    }

    public int CurrentValue
    {
        get { return mCurrentValue; }
    }
        
    // Start is called before the first frame update
    void Start()
    {
        SetHealth(45);
    }
}
