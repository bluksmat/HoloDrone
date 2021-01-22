using System;
using System.Collections;
using UnityEngine;
using Zenject;
using HoloDrone.MicroAnimations;
using HoloDrone.TransformSuperior;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {
    public class DroneInstaller : MonoInstaller<DroneInstaller>
    {
        [Inject]
        Settings _settings = null;

        public override void InstallBindings()
        {
            //IDEA: Make AppManager to switch States in Editor while in Prefab Edit Mode; adjustFanSpeed, MenuScale and etc.
            Container.Bind<LimitedLookAtCamera.AxisRotationLock>().AsSingle().IfNotBound();

            Container.BindInterfacesAndSelfTo<AppStateManager>().AsSingle().IfNotBound();

            Container.BindInterfacesAndSelfTo<AppStateAdjust>().AsSingle();
            Container.BindInterfacesAndSelfTo<AppStateExplode>().AsSingle();
            Container.BindInterfacesAndSelfTo<AppStateInfo>().AsSingle();

            Container.Bind<R_MenuSlotBinder>().AsSingle();
            Container.Bind<R_PartOfProduct>().AsSingle();

        }

        [Serializable]
        public class Settings {}

    }
}