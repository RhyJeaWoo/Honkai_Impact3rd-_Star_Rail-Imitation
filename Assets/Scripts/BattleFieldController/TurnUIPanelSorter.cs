using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnUIPanelSorter : MonoBehaviour
{

    public RectTransform panelRectTransform; // 패널의 RectTransform
    public float itemHeight = 80f; // 아이템의 높이
    public float spacing = 10f; // 아이템 간의 간격
    public float animationDuration = 0.5f; // 애니메이션 지속 시간

    private Vector2 _panelCenter;

    private List<Entity> _entities;

    private void Start()
    {
        // DOTween의 최대 Tween 수를 설정합니다.
        DOTween.SetTweensCapacity(1250, 50);

        // 패널의 중앙 좌표 계산
        _panelCenter = new Vector2(panelRectTransform.rect.width / 2, -panelRectTransform.rect.height / 2);
    }


    public void SortPanels(List<Entity> entities)
    {
        if (panelRectTransform == null)
        {
            Debug.LogError("RectTransform is not assigned.");
            return;
        }

        // 패널의 자식 RectTransform 가져오기
        var childRects = panelRectTransform.GetComponentsInChildren<RectTransform>(true)
            .Where(rt => rt != panelRectTransform)
            .ToList();

        // 각 자식 RectTransform에 대해 Entity를 찾기
        var entityList = new List<Entity>();
        foreach (var rect in childRects)
        {
            var entity = entities.FirstOrDefault(e => e.turn_Ui_Image.GetComponent<RectTransform>() == rect);
            if (entity != null)
            {
                entityList.Add(entity);
            }
        }

        if (entityList.Count == 0)
        {
            Debug.LogWarning("No entities found for sorting.");
            return;
        }

        // currentTurnSpeed에 따라 정렬
        var sortedEntities = entityList.OrderBy(e => e.currentTurnSpeed).ToList();

        //Debug.Log($"Sorting {sortedEntities.Count} entities.");

        // 패널의 상단 기준으로 위치 계산
        float startY = panelRectTransform.rect.height / 2 - itemHeight / 2;

        // 정렬된 UI 요소에 대한 위치 조정
        for (int i = 0; i < sortedEntities.Count; i++)
        {
            var rectTransform = sortedEntities[i].turn_Ui_Image.GetComponent<RectTransform>();
            float targetY = startY - i * (itemHeight + spacing); // Y 위치 계산 (패널의 상단을 기준으로 정렬)

           // Debug.Log($"Animating {sortedEntities[i].gameObject.name} to Y: {targetY}");

            rectTransform.DOAnchorPosY(targetY, animationDuration)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    if (Mathf.Abs(targetY) > panelRectTransform.rect.height / 2)
                    {
                       // Debug.LogWarning("Item is going out of the panel bounds!");
                    }
                });
        }
    }

  
}




