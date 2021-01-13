using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HoloDrone.TransformSuperior
{
    [AddComponentMenu("Transform Superior/Limited Look At Camera")]
    public class LimitedLookAtCamera : MonoBehaviour
    {
        [Tooltip("Realative to parent Transform if not specyfied")]
        public Transform relativeTo;

        private Quaternion _preaviousRot;

        [Inject]
        AxisRotationLock locks;
        [Serializable]
        public class AxisRotationLock{
            public bool x;
            public bool y;
            public bool z;
            [Range(0,360)]
            public float range;
        }

        void Update () {
            Quaternion relativeQ = (relativeTo ? relativeTo.rotation : (transform.parent ? transform.parent.rotation : Quaternion.identity));

            Vector3 ViewDirecctionInTargetSpace = Quaternion.Inverse(relativeQ * Quaternion.LookRotation(Camera.main.transform.forward,Vector3.up)) * Vector3.forward;
            
            Quaternion result = Quaternion.identity;

            if(locks.x) SingleAxis(Vector3.right,ViewDirecctionInTargetSpace,ref result);
            if(locks.y) SingleAxis(Vector3.up,ViewDirecctionInTargetSpace,ref result);
            if(locks.z) SingleAxis(Vector3.forward,ViewDirecctionInTargetSpace,ref result);

            // ViewDirecctionInTargetSpace.x = 0;
            // ViewDirecctionInTargetSpace.Normalize();
            
            // Quaternion result = Quaternion.AngleAxis(Mathf.Clamp(Mathf.Atan2(ViewDirecctionInTargetSpace.y,ViewDirecctionInTargetSpace.z)/Mathf.PI*-180f,-90,90),Vector3.right);

            result = Quaternion.Slerp(_preaviousRot,result,0.1f);

            transform.rotation = relativeQ * result; 
            
            _preaviousRot = result;

        }

        void SingleAxis(Vector3 axis, Vector3 direction, ref Quaternion result) {

            direction = (direction - Vector3.Scale(axis,direction)).normalized;
            result *= Quaternion.AngleAxis(Mathf.Clamp(Mathf.Atan2(direction.x == 0f ? direction.y : direction.x,direction.z == 0f ? direction.y : direction.z)/Mathf.PI*-180f,-locks.range/2,locks.range/2),axis);
        
        }
    }
}