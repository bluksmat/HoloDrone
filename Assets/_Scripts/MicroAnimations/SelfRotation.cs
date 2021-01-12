using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MicroAnimations
{
    [AddComponentMenu("Micro Animations/Self Rotation")]
    public class SelfRotation : MonoBehaviour
    {

        public Vector3 axisDirection;
        [InspectorName("RPM")]
        public float rpm;

        void Update()
        {
            transform.localRotation = transform.localRotation * Quaternion.AngleAxis(Time.deltaTime*rpm*6f,axisDirection.normalized);
        }
}
}