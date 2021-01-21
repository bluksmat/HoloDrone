using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Microsoft.MixedReality.Toolkit.UI;
namespace HoloDrone {

    // IDEA: Tool for Preview and adjust BoundBox Padding
    public class AppStateAdjust : AppStateBase
    {
        [Inject]
        readonly Settings _settings = null;

        BoundingBox _boundingBox;

        [Inject]
        void AddSelfToManager(AppStateManager manager) {
            manager.AddState(this);
        }

        public override void EnterState() {
            _boundingBox.enabled = true;
        }

        public override void ExitState() {
            _boundingBox.enabled = false;
        }

        public void FinalizeBinding(DiContainer container)
        {
            throw new NotImplementedException();
        }

        [Inject]
        public void BoundBoxSetup(BoundingBox boundingBox) {
        
            this._boundingBox = boundingBox;

            boundingBox.enabled = false;
        }

        public void Initialize() {}

        [Serializable]
        public class Settings {
            public float paddingBoundingBox;
        }
    }
}