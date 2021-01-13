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

        public override void AddSelfToManager() => stateMananger.AddStateToSlot(this,index:0);

        public override void EnterState() {
            Debug.Log("Enter " + this.GetType().Name);
        }

        public override void ExitState() {
            Debug.Log("Exit " + this.GetType().Name);
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