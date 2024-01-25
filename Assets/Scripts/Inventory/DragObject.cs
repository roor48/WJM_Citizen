using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace A
{
    public class DragObject : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public Transform parentTr;

        private Vector2 beginPoint;
        private Vector2 moveBegin;
        private Transform beginParent;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            if (parentTr == null)
            {
                parentTr = this.transform.parent;
            }
            canvasGroup = this.GetComponent<CanvasGroup>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            beginPoint = parentTr.position;
            beginParent = this.transform.parent;
            moveBegin = eventData.position;
            if (parentTr == this.transform)
            {
                this.GetComponent<RectTransform>().SetParent(this.transform.parent.parent);
            }
        }

        // 드래그 : 마우스 커서 위치로 이동
        public void OnDrag(PointerEventData eventData)
        {
            parentTr.position = beginPoint + eventData.position - moveBegin;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = true;
            }
            // 아이템이 슬롯 밖에 있을 시 원위치로 복귀
            if (this.GetComponent<RectTransform>().parent.name == "Inventory")
            {
                this.transform.SetParent(beginParent);
                this.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = false;
            }
        }
    }
}
