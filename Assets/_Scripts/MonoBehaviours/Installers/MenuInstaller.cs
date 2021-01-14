using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace HoloDrone {

    public class MenuInstaller : MonoInstaller<DronePrefabInstaller>
    {
        //TODO: Couldn't find reference to sceneContainer other way.

        public class Context {

            // [Inject] public List<PartOfProduct> buttons;

        }

        // [Inject]
        // public Context context;

        public override void InstallBindings () {
            // Container.BindInterfacesAndSelfTo<Context>();

            // Container.Bind<PartOfProduct>()
            // .FromComponentsInChildren();
        }
    }
}