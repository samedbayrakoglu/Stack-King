using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StackBar : MonoBehaviour
{
    [SerializeField] Image fillBar;


    public void SetFillAmount(float amount)
    {
        fillBar.DOFillAmount(amount, 0.1f);
    }
}
