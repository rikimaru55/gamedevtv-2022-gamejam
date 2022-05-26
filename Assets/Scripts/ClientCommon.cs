using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class ClientCommon : MonoBehaviour
{
    protected void spriteRenderersFadeOut(SpriteRenderer[] spriteRenderers, Action onSpriteRendererFadeOutEnd)
    {
        int spriteRenderersFinished = 0;
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOFade(0.0f, 0.30f).OnComplete(() => {
                onSpriteRendererFinished();
            });
        }

        void onSpriteRendererFinished()
        {
            spriteRenderersFinished++;
            if(spriteRenderersFinished >= spriteRenderers.Length)
            {
                onSpriteRendererFadeOutEnd();
            }
        }
    }

    public void ResetSpriteRenderers(SpriteRenderer[] spriteRenderers)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOFade(1.0f, 0.01f);
        }
    }
}
