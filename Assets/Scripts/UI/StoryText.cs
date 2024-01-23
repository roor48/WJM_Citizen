using UnityEngine;

namespace A
{
    public class StoryText : MonoBehaviour
    {
        public float storySpeed;
        public float storySizeZ;

        private Vector2 startPos;
        private RectTransform rectTransform;
        private void Start()
        {
            rectTransform = this.GetComponent<RectTransform>();
            startPos = rectTransform.anchoredPosition;
        }

        private void Update()
        {
            if (rectTransform.anchoredPosition.y <= storySizeZ)
            {
                rectTransform.anchoredPosition += storySpeed * Time.deltaTime * Vector2.up;
            }
            else
            {
                rectTransform.anchoredPosition = startPos;
            }
        }
    }
}
