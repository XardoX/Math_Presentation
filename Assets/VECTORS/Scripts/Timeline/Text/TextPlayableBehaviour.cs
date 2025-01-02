#if TEXT_TRACK_REQUIRES_TEXTMESH_PRO

using MathPresentation.LocalizationWrapper;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Playables;

namespace Timeline.Samples
{
    // Runtime representation of a TextClip.
    // The Serializable attribute is required to be animated by timeline, and used as a template.
    [Serializable]
    public class TextPlayableBehaviour : PlayableBehaviour
    {
        [Tooltip("The color of the text")]
        public Color color = Color.white;

        [Tooltip("The size of the font to use")]
        public int fontSize = 14;

        [Tooltip("The text to display")]

        [SerializeField]
        private LocalizedString localizedString;

        public string Text
        {
            get
            {
                if(Application.isPlaying)
                {
                    return localizedString.GetLocalizedString();
                }
                else
                {
                    return localizedString.TableEntryReference.Key ;
                    //return Localization.GetLocalizedStringInEditor("Dialog", localizedString.TableEntryReference.Key);
                }
            }
        }

        public string EditorText => localizedString.GetLocalizedString();
    }
}

#endif
