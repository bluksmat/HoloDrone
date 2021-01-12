using UnityEngine;
using Zenject;
using HoloDrone;

namespace HoloDrone {
    [CreateAssetMenu(fileName = "DroneSettingsInstaller", menuName = "Installers/DroneSettingsInstaller")]
    public class DroneSettingsInstaller : ScriptableObjectInstaller<DroneSettingsInstaller>
    {
        AppStateAdjust.Settings AppStateAdjust;
        AppStateExplode.Settings AppStateExplode;
        AppStateInfo.Settings AppStateInfo;
        public override void InstallBindings()
        {
            Container.BindInstance(AppStateAdjust).IfNotBound();
            Container.BindInstance(AppStateExplode).IfNotBound();
            Container.BindInstance(AppStateInfo).IfNotBound();
        }
    }
}