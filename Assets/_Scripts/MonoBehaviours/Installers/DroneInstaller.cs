using System;
using System.Collections;
using UnityEngine;
using Zenject;
using HoloDrone.MicroAnimations;
using HoloDrone.TransformSuperior;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {
    public class DroneInstaller : MonoInstaller<DroneInstaller>, IInitializable
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


            // SignalBusInstaller.Install(Container);

            // Container.DeclareSignal<UserJoinedSignal>();
            
            // // TODO: Check if prefab for sure have same ammount of ButtonSlots as States we implement, othrwise throw readable Error
            // GameObject spaceBox = Container.InstantiatePrefab(_settings.spaceBoxPrefab);

            // Container.BindInstance<BoundingBox>(spaceBox.GetComponent<BoundingBox>());
            // // Container.QueueForInject

            // //TODO: Fix error while menu chnge parent
            // if(_settings.menuAsChildOfBox) menu.transform.SetParent(spaceBox.transform,true);

            // GameObject drone = Container.InstantiatePrefab(_settings.dronePrefab,spaceBox.transform);

            // spaceBox.GetComponent<BoundingBox>().Target = drone.gameObject;



            // Container.Bind<R_MenuSlotBinder>().FromSubContainerResolve().ByNewPrefabInstaller<MenuInstaller>(_settings.menuPrefab).AsSingle().OnInstantiated((c,o) => {
            // });

            // "R_*" corespod to menu registrators

            Container.Bind<R_MenuSlotBinder>().AsSingle();
            Container.Bind<R_PartOfProduct>().AsSingle();

            // Container.Bind<R_MenuSlotBinder>().FromSubContainerResolve().ByNewPrefabInstaller<MenuInstaller>(_settings.menuPrefab).AsSingle();

            // Container.Bind<DronePrefabInstaller.Context>()
            // .FromSubContainerResolve()
            // .ByNewContextPrefab(_settings.dronePrefab)
            // .AsSingle()  


            // .OnInstantiated<DronePrefabInstaller>((ctx,obj)=>{}).NonLazy();

            // GameObject menu = Container.InstantiatePrefab(_settings.menuPrefab);

        }

        public void Initialize ()  {
            Debug.Log("wtf?");
        }

        [Serializable]
        public class Settings {
            public GameObject spaceBoxPrefab;
            [Space]
            public GameObject dronePrefab;

            [Header("Menu")]
            public GameObject menuPrefab;
            public bool menuAsChildOfBox;
            
            [Space]
            LimitedLookAtCamera.AxisRotationLock limitedLookAtCamera;
            
            [Header("Wave")]
            public bool addWaveEffect;
            public SimpleWave.Settings waveEffectSetting;

            [Space]
            public GameObject[] ModelVariantOveride;
        }

    //TODO: Review this Tooltip Pool
        // class TooltipPool : MonoPoolableMemoryPool<String,Transform,IMemoryPool,Tooltip>{}
    }
}