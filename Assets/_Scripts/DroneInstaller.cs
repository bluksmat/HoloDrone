using System;
using System.Collections;
using UnityEngine;
using Zenject;
using HoloDrone.MicroAnimations;
using HoloDrone.TransformSuperior;

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

            // TODO: Check if prefab for sure same ammount of ButtonSlots as States we implement, othrwise throw readable Error
            Container.InstantiatePrefab(_settings.menuPrefab);

            Container.InstantiatePrefab(_settings.dronePrefab);
        }

        [Serializable]
        public class Settings {
            public GameObject dronePrefab;

            [Header("Menu")]
            public GameObject menuPrefab;
            public bool menuFollowBox;
            public bool menuScaleWithBox;
            
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