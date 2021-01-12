using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MicroAnimations
{   
    [AddComponentMenu("Micro Animations/Simpe Wave")]
    public class SimpeWave : MonoBehaviour
    {
        public Vector3 waveMoveRange;
        
        [Header("Rotatiom Range")]
        public float yaw;
        public float pitch;
        public float roll;

        [Space]
        [Tooltip("Duration of Wave Animation in seconds")]
        public float duration;

        // Update is called once per frame
        void Update()
        {
            float currentFrameOffestMove = Mathf.Sin(Time.time/duration);
            float currentFrameOffestRotation = Mathf.Sin(Time.time/duration)/180f;
            
            transform.localPosition = currentFrameOffestMove * waveMoveRange;

            transform.localRotation *= 
                Quaternion.AngleAxis(currentFrameOffestRotation*yaw,Vector3.up)*
                Quaternion.AngleAxis(currentFrameOffestRotation*pitch,Vector3.right)*
                Quaternion.AngleAxis(currentFrameOffestRotation*roll,Vector3.forward);
        }
    }
}
