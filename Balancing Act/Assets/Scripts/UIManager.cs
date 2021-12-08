using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ElementOverhead
{
    [SerializeField] private Image[] elementGauges;
    [SerializeField] private Image[] unbalancedGauges;
    [SerializeField] private Image healthGauge;
    [SerializeField] private Text EndingText;
    [SerializeField] private Image meleeGauge;
    [SerializeField] private Image magic1Gauge;
    [SerializeField] private Image magic2Gauge;
    [SerializeField] private Image magic3Gauge;

    private bool meleeFilled = true;
    private bool magic1Filled = true;
    private bool magic2Filled = true;
    private bool magic3Filled = true;

    private float meleeFillTime = 0f;
    private float magic1FillTime = 0f;
    private float magic2FillTime = 0f;
    private float magic3FillTime = 0f;

    private void Update()
    {
        if (!meleeFilled)
        {
            meleeGauge.fillAmount += (1 / meleeFillTime) * Time.deltaTime;

            if (meleeGauge.fillAmount >= 1f)
                meleeFilled = true;
        }

        if (!magic1Filled)
        {
            magic1Gauge.fillAmount += (1 / magic1FillTime) * Time.deltaTime;

            if (magic1Gauge.fillAmount >= 1f)
                magic1Filled = true;
        }

        if (!magic2Filled)
        {
            magic2Gauge.fillAmount += (1 / magic2FillTime) * Time.deltaTime;

            if (magic2Gauge.fillAmount >= 1f)
                magic2Filled = true;
        }

        if (!magic3Filled)
        {
            magic3Gauge.fillAmount += (1 / magic3FillTime) * Time.deltaTime;

            if (magic3Gauge.fillAmount >= 1f)
                magic3Filled = true;
        }
    }

    public void UpdateGauges(int value, Element element)
    {
        int changedElement = -1;

        switch (element)
        {
            case Element.Light:
                changedElement = 0;

                break;
            case Element.Dark:
                changedElement = 1;

                break;
            case Element.Gray:
                changedElement = 2;

                break;
        }

        if (changedElement != -1)
        {
            if (changedElement < 2)
                elementGauges[changedElement].fillAmount = value / 100f;
            else
            {
                elementGauges[changedElement].rectTransform.localScale = new Vector3((value / 100f), 1, 1);
            }
        }
    }

    public void UpdateUnbalanced(Element unbalanced)
    {
        switch(unbalanced)
        {
            case Element.Light:
                unbalancedGauges[0].gameObject.SetActive(true);

                break;
            case Element.Dark:
                unbalancedGauges[1].gameObject.SetActive(true);

                break;
            case Element.Gray:
                unbalancedGauges[0].gameObject.SetActive(false);
                unbalancedGauges[1].gameObject.SetActive(false);

                break;
        }
    }

    public void UpdateHealth(float current, float max)
    {
        healthGauge.fillAmount = current / max;
    }

    public void EmptyMelee(float totalTime)
    {
        meleeFilled = false;
        meleeGauge.fillAmount = 0;
        meleeFillTime = totalTime;
    }

    public void EmptyMagic1(float totalTime)
    {
        magic1Filled = false;
        magic1Gauge.fillAmount = 0;
        magic1FillTime = totalTime;
    }

    public void EmptyMagic2(float totalTime)
    {
        magic2Filled = false;
        magic2Gauge.fillAmount = 0;
        magic2FillTime = totalTime;
    }

    public void EmptyMagic3(float totalTime)
    {
        magic3Filled = false;
        magic3Gauge.fillAmount = 0;
        magic3FillTime = totalTime;
    }

    public void DisplayEndingText()
    {
        EndingText.gameObject.SetActive(true);
    }
}
