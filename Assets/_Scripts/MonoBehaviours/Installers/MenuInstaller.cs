using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using HoloDrone;

// namespace HoloDrone {

    public class MenuInstaller : MonoInstaller<MenuInstaller>
    {
        public override void InstallBindings () {
            Container.Bind<R_MenuSlotBinder>().AsSingle();
        }
    }
// }