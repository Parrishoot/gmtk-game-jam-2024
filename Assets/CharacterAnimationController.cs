using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField]
    private AudioSource damageSource;

    [SerializeField]
    private AudioSource damageRunSource;

    [SerializeField]
    private AudioSource bandageAudioSource;

    [SerializeField]
    private AudioSource diveAudioSource;

    [SerializeField]
    private AudioSource riseAudioSource;

    public void Attack(Vector3 targetPos, Action OnAttack=null, Action OnComplete = null) {
        Vector3 startingPos = transform.position;

        OnAttack += damageSource.Play;

        damageRunSource.Play();

        DOTween.Sequence()
          .Append(transform.DOMove(targetPos, .1f).SetEase(Ease.InCubic).OnComplete(() => OnAttack?.Invoke()))
          .Append(transform.DOMove(startingPos, .1f).SetEase(Ease.OutCubic))
          .OnComplete(() => OnComplete?.Invoke())
          .Play();
    }

    public void Heal(Vector3 targetPos, Action OnComplete = null) {
        Vector3 startingPos = transform.position;
        Vector3 lerpedPos = Vector3.Lerp(startingPos, targetPos, .75f);
        DOTween.Sequence()
          .Append(transform.DOMove(lerpedPos, .1f).SetEase(Ease.InCubic).OnComplete(() => bandageAudioSource.Play()))
          .AppendInterval(.5f)
          .Append(transform.DOMove(startingPos, .1f).SetEase(Ease.OutCubic))
          .OnComplete(() => OnComplete?.Invoke())
          .Play();
    }

    public void Rise(Action OnComplete = null) {
        Vector3 startingPos = transform.position;
        riseAudioSource.Play();
        DOTween.Sequence()
          .Append(transform.DOMove(startingPos + Vector3.up * 30, .4f).SetEase(Ease.InSine))
          .Join(transform.DOScale(10f, .4f).SetEase(Ease.InSine))
          .OnComplete(() => OnComplete?.Invoke())
          .Play();
    }

    public void Dive(Vector3 targetPos, Action OnComplete = null) {
        
        diveAudioSource.Play();

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
