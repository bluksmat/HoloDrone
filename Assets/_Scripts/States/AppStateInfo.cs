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
        
        public override void AddSelfToManager() => stateMananger.AddStateToSlot(this,2);

        public override void EnterState() {
            Debug.Log("Enter " + this.GetType().Name);
        }

        public override void ExitState() {
            Debug.Log("Exit " + this.GetType().Name);
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