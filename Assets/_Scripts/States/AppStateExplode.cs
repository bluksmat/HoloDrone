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

        // [Inject]
        // List<PartOfProduct> allParts = new List<PartOfProduct>();

        public override void AddSelfToManager() => stateMananger.AddStateToSlot(this,1);

        public override void EnterState() {
        }

        public override void ExitState() {
        }

        public void AddPart(PartOfProduct partOfProduct)
        {
            // allParts.Add(partOfProduct);
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