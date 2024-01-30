using UnityEngine;

namespace A
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        private void LateUpdate()
        {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(this.transform.position, desiredPos, smoothSpeed);
            this.transform.position = smoothedPos;
            this.transform.LookAt(target);
        }
    }
}
