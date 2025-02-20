using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnBlockAnimation : MonoBehaviour
{
    public UnityEvent onCompleteCallback;

    public LeanTweenType inType;
    public float duration;
    public float delay;
    public Vector2 vector;


    private void OnEnable()
    {
        transform.localScale = new Vector2(0, 0);
        LeanTween.scale(gameObject, vector, duration).setDelay(delay).setOnComplete(OnComplete).setEase(inType);
    }

    public void OnComplete()
    {
        if (onCompleteCallback != null)
        {
            onCompleteCallback.Invoke();
        }
    }

}
