using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HoloDrone.MicroAnimations
{   
    [AddComponentMenu("Micro Animations/Simple Wave")]
    public class SimpleWave : MonoBehaviour
    {
        [Inject]
        private AppStateManager _stateManager;

        // [Inject]
        public Settings settings;

        float _instenstity = 0;

        [Serializable]
        public class Settings
        {
            public Vector3 waveMoveRange;

            [Header("Rotatiom Range")]
            public float yaw;
            public float pitch;
            public float roll;

            [Space]
            [Tooltip("Duration of Wave Animation in seconds")]
            public float duration;
        }
        // Update is called once per frame
        void Update()
        {
            //TODO: Optimize to remove calvulation over Range
            if(_stateManager?._currentStateHandler?.dissableWaves == true) {
                _instenstity -= Time.fixedDeltaTime;
            }else {
                _instenstity += Time.fixedDeltaTime;
            }

            _instenstity = Mathf.Clamp(_instenstity,0f,1f);
            
            float currentFrameOffestMove = Mathf.Sin(Time.time/settings.duration)*_instenstity;
            float currentFrameOffestRotation = Mathf.Sin(Time.time/settings.duration/3)*_instenstity;
            
            transform.localPosition = currentFrameOffestMove * settings.waveMoveRange;

            transform.localRotation = 
                Quaternion.AngleAxis(currentFrameOffestRotation*settings.yaw,Vector3.up)*
                Quaternion.AngleAxis(currentFrameOffestRotation*settings.pitch,Vector3.right)*
                Quaternion.AngleAxis(currentFrameOffestRotation*settings.roll,Vector3.forward);
        }
    }
}
