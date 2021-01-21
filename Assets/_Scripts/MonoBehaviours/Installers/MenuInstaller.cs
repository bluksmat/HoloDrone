using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using HoloDrone;

// namespace HoloDrone {

    public class MenuInstaller : Installer<MenuInstaller>
    {

        public override void InstallBindings () {
            Container.Bind<R_MenuSlotBinder>().AsSingle();
        }
    }
// }