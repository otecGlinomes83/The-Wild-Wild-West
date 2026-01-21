using UnityEngine;

public static class AudioPlayerFactory
{
    public static AudioPlayer Create(Transform parent = null)
    {
        GameObject gameObject = new GameObject("AudioPlayer");

        if (parent != null)
            gameObject.transform.SetParent(parent, false);

        AudioPlayer player = gameObject.AddComponent<AudioPlayer>();
        player.Setup();

        return player;
    }
}