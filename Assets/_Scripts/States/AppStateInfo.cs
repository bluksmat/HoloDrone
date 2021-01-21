using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HoloDrone {
    // IDEA: Tool to adjust tooltips in prefab preview
    public class AppStateInfo : AppStateBase
    {
        [Inject]
        readonly Settings settings = null;

        // [Inject]
        // List<PartOfProduct> allParts = new List<PartOfProduct>();

        public override bool dissableWaves => true;

        [Inject]
        void AddSelfToManager(AppStateManager manager) => stateMananger.AddState(this);

        public override void EnterState() {
        }

        public override void ExitState() {
        }

        public void AddPart(PartOfProduct partOfProduct)
        {
            // allParts.Add(partOfProduct);
        }
        
        [Serializable]
        public enum DisplayFormat {
            OneOfGroup,
            Merged,
            NoGrouping,
        }

        [Serializable]
        public class TooltipReference{
            public string label;
            public GameObject reference;
        }

        [Serializable]
        public class Settings {
            
            public DisplayFormat grouping;
            public bool faceUser;

            [Space]
            public TooltipReference[] tooltips;
        }
    }
}