using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloDrone
{
    public class FaceCameraByXAxis : MonoBehaviour
    {
        public Transform relativeTo;

        private Quaternion _preaviousRot;

        private void Awake() {
            relativeTo = transform.parent ?? relativeTo; 
        }

        void Update () {
            Vector3 ViewDirecctionInTargetSpace = (Quaternion.Inverse(relativeTo.rotation) * Quaternion.LookRotation(Camera.main.transform.forward,Vector3.up)) * Vector3.forward;
            
            ViewDirecctionInTargetSpace.x = 0;
            ViewDirecctionInTargetSpace.Normalize();
            
            Quaternion result = Quaternion.AngleAxis(Mathf.Clamp(Mathf.Atan2(ViewDirecctionInTargetSpace.y,ViewDirecctionInTargetSpace.z)/Mathf.PI*-180f,-90,90),Vector3.right);

            result = Quaternion.Slerp(_preaviousRot,result,0.1f);

            transform.rotation = relativeTo.rotation * result; 
            
            _preaviousRot = result;

        }
    }
}