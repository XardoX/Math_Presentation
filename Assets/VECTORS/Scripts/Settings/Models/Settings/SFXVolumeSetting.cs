using Ami.BroAudio;
using UnityEngine;
namespace MathPresentation.Settings.Models
{
    [CreateAssetMenu(fileName = "SFX Volume", menuName = "Data/Settings/SFX Volume")]
    public class SFXVolumeSetting : FloatSettingModel
    {
        protected override void OnValueChanged(float oldValue)
        {
            BroAudio.SetVolume(BroAudioType.SFX, value);
        }
    }
}