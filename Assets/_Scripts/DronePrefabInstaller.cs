using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace HoloDrone {

    public class DronePrefabInstaller : MonoInstaller<DronePrefabInstaller>
    {
        //TODO: Couldn't find reference to sceneContainer other way.

        public class Context {
            [Inject] public DronePrefabInstaller installer;

        }

        // [Inject]
        // public Context context;

        public List<PartOfProduct> partOfProduct;

        public override void InstallBindings () {
            Debug.Log("DronePrefabInstaller.InstallBindings");
//TODO Inject parts of product

            Container.Bind<DronePrefabInstaller.Context>().FromInstance(new DronePrefabInstaller.Context()).AsSingle();

            Container.BindInterfacesAndSelfTo<DronePrefabInstaller>()
            .FromInstance(this)
            .AsSingle();

            Container.Bind<PartOfProduct>()
            .FromComponentsInChildren().AsTransient();
        }
        
        void Update () {
        }

        [Inject]
        public void part (PartOfProduct p) {
        }
    }
}