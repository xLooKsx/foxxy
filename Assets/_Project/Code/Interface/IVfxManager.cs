using UnityEngine;

public enum VfxType
{
    Death
}

public interface IVfxManager
{
    void PlayVfx(VfxType vfxType, Vector3 position);
}
