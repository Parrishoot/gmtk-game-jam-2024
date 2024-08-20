using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TitleImageController: MonoBehaviour
{
    [SerializeField]
    private float breatheSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Sequence()
            .Join(transform.DOScale(1.1f, breatheSpeed).SetEase(Ease.InOutSine))
            .Append(transform.DOScale(1, breatheSpeed).SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Yoyo)
            .Play();
    }
}
