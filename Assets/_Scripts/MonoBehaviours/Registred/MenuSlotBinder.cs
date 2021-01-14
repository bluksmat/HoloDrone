using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {

    public class R_MenuSlotBinder: MonoRegistry<PartOfProduct>{}

    public class MenuSlotBinder : RegistredMonoBehaviour<MenuSlotBinder,R_MenuSlotBinder> {

        [Inject]
        void BindMeToManager(AppStateManager appStateManager) => appStateManager.ActivateSlot(GetComponent<Interactable>(),transform.GetSiblingIndex());
    
    }
}
