using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HoloDrone {

    // IDEA: Tool for adjusting Explode object merging
    public class AppStateExplode : AppStateBase, IFixedTickable
    {
        [Inject]
        readonly Settings settings = null;

        [Inject]
        readonly AppStateManager manager;

        R_PartOfProduct _partsOfProduct;

        public override bool dissableWaves => true;

        float _animationProgress;
        float _animationDelta;

        [Inject]
        void GetPartRegistry(R_PartOfProduct partsOfProduct) {
            this._partsOfProduct = partsOfProduct;
        }

        [Inject]
        void AddSelfToManager(AppStateManager manager) => stateMananger.AddState(this);



        public override void EnterState() {
            _animationDelta = settings.animationSpeed;

        }

        public override void ExitState() {
            _animationDelta = -settings.animationSpeed;

        }

        public void FixedTick()
        {
            // if(manager._currentStateHandler != this) return;
            _animationProgress = Mathf.Clamp(_animationProgress + Time.fixedDeltaTime*_animationDelta,0f,1f);

            foreach(var part in _partsOfProduct.components) {
                var refLP = part.refLocalPos;
                var refOP = part.refOffsetPos;

                part.transform.position = 
                    part.transform.parent.TransformPoint(refLP)
                    -part.mergeTo.transform.parent.TransformPoint(part.mergeTo.refLocalPos)
                    +part.mergeTo.transform.TransformPoint(Vector3.Scale(refOP,part.ownOffsetMultiplayer*settings.rangeMultiplayer*settings.animationCurve.Evaluate(_animationProgress)));
            }
        }
        
        [Serializable]
        public class Settings {
            public float rangeMultiplayer;
            public AnimationCurve animationCurve;
            public float animationSpeed;

            public int initialPartsCapacity;

        }
    }
}