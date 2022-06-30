using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonBounce : MonoBehaviour
{
    private void Awake()
    {
        transform.DOScale(transform.localScale * 1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
