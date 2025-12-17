using UnityEngine;

public enum SfxType
{
    Jump,
    Death,
    DamageTaken,
    Checkpoint
}

public interface IAudioManager
{
    void PlaySfx(SfxType sfxType);
}
