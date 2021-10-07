using UnityEngine;

namespace ToolBox
{
    [System.Serializable]
    public class AudioElement
    {
        public string name;
        [Range(0, 1)]
        public float volume = 1;
    }

    [System.Serializable]
    public class AudioLoopable : AudioElement
    {
        public bool inLoopOut;
        public AudioClip clip;
        public AudioClip clipIn;
        public AudioClip clipOut;
    }

    [System.Serializable]
    public class AudioRandomable : AudioElement
    {
        public AudioClip[] clips;
    }
}