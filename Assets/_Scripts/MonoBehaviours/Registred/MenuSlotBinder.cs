using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {

    public class R_MenuSlotBinder: MonoRegister<MenuSlotBinder>{}

    [RequireComponent(typeof(Interactable))]
    public class MenuSlotBinder : RegistredMonoBehaviour<MenuSlotBinder,R_MenuSlotBinder> {

    }
}
