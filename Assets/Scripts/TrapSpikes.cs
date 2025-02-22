using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using TEDUJam;

namespace TEDUJam
{
    public class TrapSpikes : MonoBehaviour
    {
        public List<CharacterController> charactersInRange = new List<CharacterController>();
        public List<Spike> ListSpikes = new List<Spike>();

        Coroutine SpikeTriggerCoroutine;
        bool SpikesReloaded;

        private void Start()
        {
            charactersInRange.Clear();
            SpikesReloaded = true;
            ListSpikes.Clear();
            Spike[] arr = this.gameObject.GetComponentsInChildren<Spike>();
            foreach (Spike s in arr)
            {
                ListSpikes.Add(s);
            }
        }

        private void Update()
        {
            if (charactersInRange.Count > 0)
            {
                foreach (CharacterController control in charactersInRange)
                {
                    if (SpikeTriggerCoroutine == null && SpikesReloaded)
                    {
                        SpikeTriggerCoroutine = StartCoroutine(_SpikeTrigger());
                    }
                }
            }
        }

        private IEnumerator _SpikeTrigger()
        {
            SpikesReloaded = false;

            // Tüm spike'larý ayný anda hareket ettir
            foreach (Spike s in ListSpikes)
            {
                s.Shoot();
            }

            yield return new WaitForSeconds(1f); // Yeterli bekleme süresi

            foreach (Spike s in ListSpikes)
            {
                s.Retract();
            }

            yield return new WaitForSeconds(1f);
            SpikeTriggerCoroutine = null;
            SpikesReloaded = true;

        }

        public static bool IsTrap(GameObject obj)
        {
            if (obj.transform.root.gameObject.GetComponent<TrapSpikes>() != null)
            {
                return true;
            }
            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterController control =
                other.gameObject.transform.root.gameObject.GetComponent<CharacterController>();
            if (control != null && !charactersInRange.Contains(control))
            {
                charactersInRange.Add(control);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CharacterController control =
                other.gameObject.transform.root.gameObject.GetComponent<CharacterController>();
            if (control != null && charactersInRange.Contains(control))
            {
                charactersInRange.Remove(control);
            }
        }
    }
}
