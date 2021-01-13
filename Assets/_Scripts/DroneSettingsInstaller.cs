using UnityEngine;
using System;
using Zenject;
using HoloDrone;
using HoloDrone.MicroAnimations;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {

    [CreateAssetMenu(fileName = "DroneSettingsInstaller", menuName = "Installers/DroneSettingsInstaller")]
    public class DroneSettingsInstaller : ScriptableObjectInstaller<DroneSettingsInstaller>
    {
        public DroneInstaller.Settings DroneInstaller;

        [Header("States Settings")]
        public AppStateAdjust.Settings AppStateAdjust;
        public AppStateExplode.Settings AppStateExplode;
        public AppStateInfo.Settings AppStateInfo;


        public override void InstallBindings()
        {
            Container.BindInstance(AppStateAdjust).AsSingle();
            Container.BindInstance(AppStateExplode).AsSingle();
            Container.BindInstance(AppStateInfo).AsSingle();

            Container.BindInstance(DroneInstaller).AsSingle();
        }
    }
}