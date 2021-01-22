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
            //TODO: More smooth dissable like in SimpleWave
            if(_stateManager?._currentStateHandler?.dissableSelfRotations == true) return;
            
            transform.localRotation = transform.localRotation * Quaternion.AngleAxis(Time.deltaTime*rpm*6f,axisDirection.normalized);
            // }
        }
}
}