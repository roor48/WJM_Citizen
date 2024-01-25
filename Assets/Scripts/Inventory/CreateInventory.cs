using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace A
{
    public class CreateInventory : MonoBehaviour
    {
        public int slotCntWidth = 4;
        public int slotCntHeight = 4;
        public int slotWidthSize = 64;
        public int slotHeightSize = 64;
        public GameObject prefabSlot;
        public RectTransform parentObj;
        public Sprite backSprite;
        public int SlotTopBarSize = 20;

        private GameObject inventory;
        public List<GameObject> slots = new();
        public GameObject prefabItem;

        private void Start()
        {
            SetInventory();

            SetItem( new List<Item>() { new Item() } );
        }

        private void SetInventory()
        {
            inventory = new GameObject();
            Image image = inventory.AddComponent<Image>();
            if (backSprite != null )
            {
                image.sprite = backSprite;
            }
            RectTransform inventoryRT = inventory.GetComponent<RectTransform>();
            inventoryRT.SetParent(parentObj);
            inventoryRT.anchoredPosition = Vector3.zero;
            inventoryRT.sizeDelta = new Vector2(slotWidthSize * slotCntWidth, slotHeightSize * slotCntHeight + SlotTopBarSize);
            inventoryRT.name = "Inventory";


            // 인벤토리 움직일 헤더 생성
            GameObject backTop = new GameObject();
            Image backTopImg = backTop.AddComponent<Image>();
            backTopImg.color = Color.blue;
            RectTransform backTopRT = backTop.GetComponent<RectTransform>();
            backTopRT.SetParent(inventoryRT.transform);
            // Left-Top 으로 정렬
            backTopRT.pivot = Vector2.up;
            backTopRT.anchorMin = Vector2.up;
            backTopRT.anchorMax = Vector2.up;

            backTopRT.anchoredPosition = Vector3.zero;
            backTopRT.sizeDelta = new Vector2(slotWidthSize * slotCntWidth, SlotTopBarSize);
            backTopRT.name = "BackTop";
            backTop.AddComponent<DragObject>();


            for (int i = 0; i < slotCntHeight; i++)
            {
                for (int j = 0; j < slotCntWidth;j ++)
                {
                    GameObject slot = Instantiate(prefabSlot);
                    RectTransform slotRT = slot.GetComponent<RectTransform>();
                    slotRT.SetParent(inventoryRT);
                    slotRT.pivot = Vector2.up;
                    slotRT.anchorMin = Vector2.up;
                    slotRT.anchorMax = Vector2.up;
                    slotRT.anchoredPosition = Vector3.zero;
                    slotRT.anchoredPosition += new Vector2(slotWidthSize * i, -(slotHeightSize * j) - SlotTopBarSize);
                    //slotRT.anchoredPosition += new Vector2(slotWidthSize * j, -(slotHeightSize * i) - SlotTopBarSize);
                    slotRT.sizeDelta = new Vector2(slotWidthSize, slotHeightSize);
                    slotRT.name = i + ", " + j;
                    slots.Add(slot);
                }
            }

            //inventory.SetActive(false);
        }

        public void SetItem(List<Item> item)
        {
            for (int i = 0; i < item.Count; i++)
            {
                GameObject it = Instantiate(prefabItem);
                RectTransform itRT = it.GetComponent<RectTransform>();
                itRT.SetParent(slots[i].GetComponent<RectTransform>());
                itRT.pivot = new Vector2(0.5f, 0.5f);
                itRT.anchorMin = Vector2.zero;
                itRT.anchorMax = Vector2.one;
                itRT.offsetMin = new Vector2(10, 10);
                // offsetMax의 경우 반대
                itRT.offsetMax = new Vector2(-10, -10);
                it.AddComponent<DragObject>().parentTr = it.transform;
            }
        }
    }
}
