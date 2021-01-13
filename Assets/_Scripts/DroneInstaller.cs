using System;
using System.Collections;
using UnityEngine;
using Zenject;
using HoloDrone.MicroAnimations;
using HoloDrone.TransformSuperior;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {
    public class DroneInstaller : MonoInstaller
    {
        [Inject]
        Settings _settings = null;

        public override void InstallBindings()
        {
            //IDEA: Make AppManager to switch States in Editor while in Prefab Edit Mode; adjustFanSpeed, MenuScale and etc.
            Container.Bind<LimitedLookAtCamera.AxisRotationLock>().AsSingle().IfNotBound();


            var appStateManager = Container.BindInterfacesAndSelfTo<AppStateManager>().FromInstance(new AppStateManager(statesInitialCapacity:3)).AsSingle();

            Container.BindInterfacesAndSelfTo<AppStateAdjust>().AsSingle();
            Container.BindInterfacesAndSelfTo<AppStateExplode>().AsSingle();
            Container.BindInterfacesAndSelfTo<AppStateInfo>().AsSingle();

            // TODO: Check if prefab for sure have same ammount of ButtonSlots as States we implement, othrwise throw readable Error
            GameObject spaceBox = Container.InstantiatePrefab(_settings.spaceBoxPrefab);

            //TODO: Fix error while menu chnge parent
            GameObject menu = Container.InstantiatePrefab(_settings.menuPrefab);
            if(_settings.menuAsChildOfBox) menu.transform.SetParent(spaceBox.transform,true);

            // DronePrefabInstaller drone = Container.InstantiatePrefabForComponent<DronePrefabInstaller>(_settings.dronePrefab,spaceBox.transform);

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