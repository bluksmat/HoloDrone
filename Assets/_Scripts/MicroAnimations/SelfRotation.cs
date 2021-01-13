using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HoloDrone.MicroAnimations
{
    [AddComponentMenu("Micro Animations/Self Rotation")]
    public class SelfRotation : MonoBehaviour
    {

        [Inject]
        private AppStateManager _stateManager;
        public Vector3 axisDirection;

        [InspectorName("RPM")]
        public float rpm;

        [Inject]
        public void Init(float rpm) {
            this.rpm = rpm;
        }

        void Update()
        {
            //TODO: Make Base class for MicroAnimations which check allowMicroAnimations itself
            if(!_stateManager._currentStateHandler.allowMicroAnimations) return;

            transform.localRotation = transform.localRotation * Quaternion.AngleAxis(Time.deltaTime*rpm*6f,axisDirection.normalized);
        }
}
}