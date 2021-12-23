using UnityEngine;

public abstract class CollisionEvents : MonoBehaviour
{
    [System.Serializable]
    public class Sounds
    {
        public AudioClip clip;
        [Range(0, 1)]
        public float volume = 1;
        [Range(-3, 3)]
        public float pitch = 1;
        // [Sirenix.OdinInspector.ReadOnly]
        public AudioSource source;
    }

    public bool useTag = true;
    // [Sirenix.OdinInspector.ShowIf("useTag")]
    [Tag]
    public string targetTag;
    // [Sirenix.OdinInspector.HideIf("useTag")]
    public LayerMask tagetLayer;

    [Header("Sounds")]
    public Sounds sounds = new Sounds();

    protected virtual void Start()
    {
        if (!sounds.clip)
            return;
        GameObject audioSource = new GameObject();
        audioSource.AddComponent<AudioSource>();
        AudioSource s = audioSource.GetComponent<AudioSource>();
        s.playOnAwake = false;
        s.volume = sounds.volume;
        s.pitch = sounds.pitch;
        s.clip = sounds.clip;
        sounds.source = s;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (useTag)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                PlaySound();
                CollisionEnter(other);
            }
        }
        else
        {
            if (((1 << other.gameObject.layer) & tagetLayer) != 0)
            {
                PlaySound();
                CollisionEnter(other);
            }
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (useTag)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                PlaySound();
                CollisionStay(other);
            }
        }
        else
        {
            if (((1 << other.gameObject.layer) & tagetLayer) != 0)
            {
                PlaySound();
                CollisionStay(other);
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (useTag)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                PlaySound();
                CollisionExit(other);
            }
        }
        else
        {
            if (((1 << other.gameObject.layer) & tagetLayer) != 0)
            {
                PlaySound();
                CollisionExit(other);
            }
        }
    }

    void PlaySound()
    {
        if (sounds.source)
            sounds.source.Play();
    }

    public abstract void CollisionEnter(Collision triggeredObject);
    public abstract void CollisionExit(Collision triggeredObject);
    public abstract void CollisionStay(Collision triggeredObject);
}
