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

    public void DisplayEndingText()
    {
        EndingText.gameObject.SetActive(true);
    }
}
