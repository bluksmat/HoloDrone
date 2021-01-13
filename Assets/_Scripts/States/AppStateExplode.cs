using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HoloDrone {

    // IDEA: Tool for adjusting Explode object merging
    public class AppStateExplode : AppStateBase,IInitializable
    {
        [Inject]
        readonly Settings settings = null;

        [Inject]
        List<PartOfProduct> allParts;

        public override void AddSelfToManager() => stateMananger.AddStateToSlot(this,1);

        public override void EnterState() {
            Debug.Log("Enter " + typeof(AppStateExplode).Name);
            Debug.Log(allParts.Count);
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

            public int initialPartsCapacity;

            [Header("Manuals")]
            public CombineModels[] merges;
            public CombineModels[] unmerges;
        }
    }
}