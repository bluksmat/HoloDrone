using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HoloDrone.MicroAnimations
{
    [AddComponentMenu("Micro Animations/Self Rotation")]
    public class SelfRotation : MonoBehaviour
    {

        private AppStateManager _stateManager;
        public Vector3 axisDirection;

        [InspectorName("RPM")]
        public float rpm;

        [Inject]
        private void BindManager(AppStateManager stateManager) {
            _stateManager = stateManager;
        }

        void Update()
        {
            // if(_stateManager?._currentStateHandler?.dissableWaves == true) return;
            //TODO: Make Base class for MicroAnimations which check allowMicroAnimations itself
            // if(_stateManager?._currentStateHandler?.allowMicroAnimations == true || _stateManager?._currentStateHandler == null) {
                transform.localRotation = transform.localRotation * Quaternion.AngleAxis(Time.deltaTime*rpm*6f,axisDirection.normalized);
            // }
        }
}
}