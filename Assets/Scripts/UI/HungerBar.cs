using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Image ImgFoodBar;
    public Text TxtFood;
    public int Min;
    public int Max;
    private int mCurrentValue;
    private float mCurrentPercentage;

    public void SetFood(int food)
    {
        if (food != mCurrentValue)
        {
            if (Max - Min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercentage = 0;
            }
            else
            {
                mCurrentValue = food;

                mCurrentPercentage = (float) mCurrentValue / (float) (Max - Min);
            }

            TxtFood.text = string.Format("{0} %", Mathf.RoundToInt(mCurrentPercentage * 100));

            ImgFoodBar.fillAmount = mCurrentPercentage;
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
        SetFood(100);
    }
}
