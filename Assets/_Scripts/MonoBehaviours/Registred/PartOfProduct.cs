using UnityEngine;
using System;
using Zenject;
using HoloDrone;
using HoloDrone.MicroAnimations;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections.Generic;

namespace HoloDrone {

    public class R_PartOfProduct: MonoRegister<PartOfProduct>{}

    [RequireComponent(typeof(Collider))]
    [AddComponentMenu("Holo Drone/Part Of Product")]
    public class PartOfProduct : RegistredMonoBehaviour<PartOfProduct,R_PartOfProduct>, IMixedRealityFocusHandler
    {
        [Header("Explode")]
        
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
        
        [Tooltip("Define whether we prefer using tranaform pivot or bounding box center")]
        public bool useBoundingBoxCenter = false;

        public Vector3 offsetMultiplayer = Vector3.one;

        public Action<PartOfProduct, FocusEventData> onFocusEnter;
        public Action<PartOfProduct, FocusEventData> onFocusExit;

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

        [Header("Tooltips")]
        public string label = "";
        
        public GameObject tooltipPrefab;

        [Serializable]
        public class PartTooltipSetup {
            [Tooltip("define position frow which tooltip line start")]
            public Transform tooltipAnchor = null;

            [Tooltip("define position for tooltip")]
            public Transform tooltipPivot = null;
        }

        [Tooltip("We use list in case of objects which visualy are slited in multiple object but are single mesh and etc.")]
        public List<PartTooltipSetup> productTooltipSetups = null;

        private Vector3 _localBoundCenter = Vector3.positiveInfinity;
        public Vector3 localBoundCenter {
            get {
                if(_localBoundCenter.x == Mathf.Infinity) {

                    _localBoundCenter = transform.InverseTransformPoint(GetComponent<MeshRenderer>().bounds.center);

                }
                return _localBoundCenter;
            }
        }

        /// <summary> In local space of merge target (used for object disassemble like in Explode State </summary>
        public Vector3 refOffsetPos {
            get {
                if(_refOffsetPos.x == Mathf.Infinity) {
                    if(useBoundingBoxCenter) {
                        _refOffsetPos = mergeTo.transform.InverseTransformPoint(transform.GetComponent<MeshRenderer>().bounds.center) - mergeTo.transform.InverseTransformPoint(mergeTo.GetComponent<MeshRenderer>().bounds.center);
                    }else{
                        _refOffsetPos = mergeTo.transform.InverseTransformPoint(transform.position);

                    }
                }
                return _refOffsetPos;
            }
        }

        public void OnFocusEnter(FocusEventData eventData)
        {
            if(onFocusEnter != null) onFocusEnter(this,eventData);
        }

        public void OnFocusExit(FocusEventData eventData)
        {
            if(onFocusEnter != null) onFocusEnter(this,eventData);
        }
    }
}