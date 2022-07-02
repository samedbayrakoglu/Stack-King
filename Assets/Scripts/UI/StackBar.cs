using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class StackBar : MonoBehaviour
{
    [SerializeField] Image fillBar;

    [SerializeField] GameObject fillTextPrefab;



    public void SetFillAmount(float amount)
    {
        fillBar.DOFillAmount(amount, 0.1f);
    }

    public void DirectlySetFillBar(float amount)
    {
        fillBar.fillAmount = amount;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Upgrade(float rate)
    {
        transform.DOScale(transform.localScale * 1.2f, 0.15f).SetLoops(2, LoopType.Yoyo);

        SetFillAmount(rate);
    }

    public void FillText(string amountTxt)
    {
        GameObject fillTxtObj = Instantiate(fillTextPrefab, transform.position, Quaternion.identity);

        fillTxtObj.transform.SetParent(gameObject.transform);

        TextMeshProUGUI txt = fillTxtObj.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = amountTxt.ToString();

        txt.DOFade(0f, 1f).OnUpdate(() =>
        {
            fillTxtObj.transform.Translate(new Vector3(0, 1f, 0) * Time.deltaTime);
        }).OnComplete(() =>
        {
            Destroy(fillTxtObj);
        });
    }
}
