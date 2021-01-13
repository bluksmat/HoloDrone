using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace HoloDrone {
    public class DronePrefabInstaller : MonoInstaller
    {
        public override void InstallBindings () {
            Container.Bind<PartOfProduct>().FromComponentsInChildren();

            Debug.Log("lel");
        }
    }
}