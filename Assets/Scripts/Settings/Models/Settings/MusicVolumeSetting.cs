using Ami.BroAudio;
using UnityEngine;
namespace MathPresentation.Settings.Models
{
    [CreateAssetMenu(fileName = "Music Volume", menuName = "Data/Settings/Music Volume")]
    public class MusicVolumeSetting : FloatSettingModel
    {
        protected override void OnValueChanged(float oldValue)
        {
            BroAudio.SetVolume(BroAudioType.Music, value);
        }
    }
}