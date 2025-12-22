using System;
using UnityEngine;

public class VfxManager : MonoBehaviour, IVfxManager
{

    [SerializeField] 
    private FxConfig[] fxConfigs;
    public void PlayVfx(VfxType vfxType, Vector3 position)
    {
        GameObject currentFx = GetFx(vfxType);
        currentFx.transform.position = position;
        currentFx.SetActive(true);
    }

    private GameObject GetFx(VfxType vfxType)
    {
        foreach (var item in fxConfigs)
        {
            if (vfxType.Equals(item.vfxType))
            {
                return item.fx;
            }
        }

        return null;
    }
}

    [Serializable]
    public struct FxConfig
    {
        public VfxType vfxType;
        public GameObject fx;
    }
