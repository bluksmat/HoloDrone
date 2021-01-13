using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {

    public class MenuSlotBinder : MonoBehaviour
    {

        [Inject]
        void BindMeToManager(AppStateManager appStateManager) {
            appStateManager.ActivateSlot(GetComponent<Interactable>(),transform.GetSiblingIndex());
        }

    }
}
