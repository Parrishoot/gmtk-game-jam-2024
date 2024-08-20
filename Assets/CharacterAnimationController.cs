using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public void Attack(Vector3 targetPos, Action OnAttack=null, Action OnComplete = null) {
        Vector3 startingPos = transform.position;
        DOTween.Sequence()
          .Append(transform.DOMove(targetPos, .1f).SetEase(Ease.InCubic).OnComplete(() => OnAttack?.Invoke()))
          .Append(transform.DOMove(startingPos, .1f).SetEase(Ease.OutCubic))
          .OnComplete(() => OnComplete?.Invoke())
          .Play();
    }

    public void Heal(Action OnComplete = null) {
        Vector3 startingPos = transform.position;
        DOTween.Sequence()
          .Append(transform.DOMove(startingPos + Vector3.up * .5f, .2f).SetEase(Ease.OutSine))
          .Append(transform.DOMove(startingPos, .1f).SetEase(Ease.InSine))
          .OnComplete(() => OnComplete?.Invoke())
          .Play();
    }

    public void Rise(Action OnComplete = null) {
        Vector3 startingPos = transform.position;
        DOTween.Sequence()
          .Append(transform.DOMove(startingPos + Vector3.up * 30, .4f).SetEase(Ease.InSine))
          .Join(transform.DOScale(10f, .4f).SetEase(Ease.InSine))
          .OnComplete(() => OnComplete?.Invoke())
          .Play();
    }

    public void Dive(Vector3 targetPos, Action OnComplete = null) {
        
        Vector3 dist = Vector3.Lerp(transform.position, targetPos, .7f);
        Vector3 halfPos = dist + (Vector3.up * 1);


        DOTween.Sequence()
          .Append(transform.DOMove(halfPos, .2f).SetEase(Ease.OutSine))
          .Append(transform.DOMove(targetPos, .3f).SetEase(Ease.InSine))
          .Join(transform.DOScale(Vector3.zero, .3f).SetEase(Ease.InSine))
          .OnComplete(() => OnComplete?.Invoke())
          .Play();
    }

    public void Damage(Action OnComplete = null) {
        transform.DOShakePosition(.3f, .2f, 100).OnComplete(() => OnComplete?.Invoke());
    }
}
