using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace A
{
    public class DragObject : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Transform parentTr;

        private Vector2 beginPoint;
        private Vector2 moveBegin;
        public Transform beginParent;
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
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = false;
            }

            beginPoint = parentTr.position;
            beginParent = this.transform.parent;
            moveBegin = eventData.position;
            if (parentTr == this.transform)
            {
                this.GetComponent<RectTransform>().SetParent(this.transform.parent.parent);
            }
        }

        // �巡�� : ���콺 Ŀ�� ��ġ�� �̵�
        public void OnDrag(PointerEventData eventData)
        {
            parentTr.position = beginPoint + eventData.position - moveBegin;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = true;
            }
            // �������� ���� �ۿ� ���� �� ����ġ�� ����
            if (this.GetComponent<RectTransform>().parent.name == "Inventory")
            {
                this.transform.SetParent(beginParent);
                this.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }
    }
}
