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

        // LazyInject<DronePrefabInstaller.Context> _dronePrefabContext;
        // LazyInject<DronePrefabInstaller.Context> _dronePrefabContext;

        public override bool allowMicroAnimations => true;

        public override void AddSelfToManager() => stateMananger.AddStateToSlot(this,index:0);

        public override void EnterState() {
            // boundingBox.gameObject.SetActive(true);
            // Debug.Log(_dronePrefabContext==null);
        }

        public override void ExitState() {
            // boundingBox.gameObject.SetActive(false);
        }

        public override void Initialize()
        {
            base.Initialize();
            // boundingBox.gameObject.SetActive(false);
        }

        public void FinalizeBinding(DiContainer container)
        {
            throw new NotImplementedException();
        }

        [Serializable]
        public class Settings {
            public float paddingBoundingBox;
        }
    }
}