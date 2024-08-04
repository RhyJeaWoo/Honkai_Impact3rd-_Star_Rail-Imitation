using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class UiInfomationFade : MonoBehaviour
{

    public CanvasGroup uiCanvasGroup; // 페이드 효과를 적용할 UI의 Canvas Group
    public float fadeDuration = 0.5f; // 페이드 인/아웃 지속 시간

    private void Awake()
    {
        if (uiCanvasGroup == null)
        {
            uiCanvasGroup = GetComponent<CanvasGroup>();
        }
    }

    // UI 창을 열 때 페이드 인 효과 적용
    public void OpenUI()
    {
        uiCanvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            uiCanvasGroup.interactable = true;
            uiCanvasGroup.blocksRaycasts = true;
        });
    }

    // UI 창을 닫을 때 페이드 아웃 효과 적용
    public void CloseUI()
    {
        uiCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            uiCanvasGroup.interactable = false;
            uiCanvasGroup.blocksRaycasts = false;
        });
    }
}
