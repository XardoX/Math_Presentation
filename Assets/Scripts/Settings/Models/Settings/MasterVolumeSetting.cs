using Ami.BroAudio;
using UnityEngine;
namespace MathPresentation.Settings.Models
{
    [CreateAssetMenu(fileName = "Master Volume", menuName = "Data/Settings/Master Volume")]
    public class MasterVolumeSetting : FloatSettingModel
    {
        protected override void OnValueChanged(float oldValue)
        {
            BroAudio.SetVolume(BroAudioType.All, value);
        }
    }
}