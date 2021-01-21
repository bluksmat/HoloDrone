using UnityEngine;
using System;
using Zenject;
using HoloDrone;
using HoloDrone.MicroAnimations;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {

    public class R_PartOfProduct: MonoRegister<PartOfProduct>{}

    [AddComponentMenu("Holo Drone/Part Of Product")]
    public class PartOfProduct : RegistredMonoBehaviour<PartOfProduct,R_PartOfProduct>
    {
        public string label = "";
        
        [Tooltip("Explosion would be relative to this target")]
        [SerializeField]
        private PartOfProduct _mergeTo;
        public PartOfProduct mergeTo {
            get {
                if(_mergeTo == null) {
                    _mergeTo = this;
                }
                return _mergeTo;
            }
        }
        [Tooltip("Object would not innclude it's own offfset")]
        public bool useBoundingBoxCenter = false;

        public Vector3 ownOffsetMultiplayer = Vector3.one;

        private Vector3 _refLocalPos = Vector3.positiveInfinity;
        public Vector3 refLocalPos {
            get {
                if(_refLocalPos.x == Mathf.Infinity) {
                    _refLocalPos = transform.localPosition;
                }
                return _refLocalPos;
            }
        }
        private Vector3 _refOffsetPos = Vector3.positiveInfinity;

        //In local space of merge targer
        public Vector3 refOffsetPos {
            get {
                if(_refOffsetPos.x == Mathf.Infinity) {
                    if(useBoundingBoxCenter) {
                        //Offset betwen bounding boxes
                        _refOffsetPos = mergeTo.transform.InverseTransformPoint(transform.GetComponent<MeshRenderer>().bounds.center) - mergeTo.transform.InverseTransformPoint(mergeTo.GetComponent<MeshRenderer>().bounds.center);
                    }else{
                        _refOffsetPos = mergeTo.transform.InverseTransformPoint(transform.position);

                    }
                }
                return _refOffsetPos;
            }
        }
    }
}