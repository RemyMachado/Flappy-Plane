using UnityEngine;

namespace Utils.Behaviors
{
    public class DestroyOutOfBound : MonoBehaviour
    {
        public float minY = float.MinValue;
        public ParticleSystem[] destroyParticlesPrefabs; // particles effects to apply in order
        public AudioClip destroySound;

        void Update()
        {
            if (transform.position.y < minY)
            {
                GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(destroySound, 0.5f);
                for (uint i = 0; i < destroyParticlesPrefabs.Length; ++i)
                {
                    Instantiate(destroyParticlesPrefabs[i], transform.position, transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }
}