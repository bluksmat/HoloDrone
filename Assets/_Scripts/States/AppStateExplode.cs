using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HoloDrone {

    // IDEA: Tool for adjusting Explode object merging
    public class AppStateExplode : AppStateBase
    {
        [Inject]
        readonly Settings settings = null;

        public override void AddSelfToManager() => stateMananger.AddStateToSlot(this,1);

        public override void EnterState() {
            Debug.Log("Enter " + typeof(AppStateExplode).Name);
        }

        public override void ExitState() {
            Debug.Log("Exit " + typeof(AppStateExplode).Name);
        }
        
        [Serializable]
        public class CombineModels {
            public GameObject Who;
            public GameObject To;
        }
        [Serializable]
        public class Settings {
            public float defaultCombineRange;

            [Header("Manuals")]
            public CombineModels[] merges;
            public CombineModels[] unmerges;
        }
    }
}