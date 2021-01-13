using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StateSwitchBinding : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("bindState");
    }
}
