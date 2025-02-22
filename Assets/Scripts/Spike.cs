using System.Collections;
using UnityEngine;

namespace TEDUJam
{
    public class Spike : MonoBehaviour
    {

        public void Shoot()
        {
            if (this.transform.localPosition.y < 0f) // Eþik deðeri projenize göre ayarlayýn
            {
                StartCoroutine(_Shoot());
            }
        }

        IEnumerator _Shoot()
        {
            float delay = Random.Range(0f, 0.25f);

            yield return new WaitForSeconds(delay);

            this.transform.localPosition += Vector3.up * 2.5f; // localPosition yerine position
            Debug.Log("New Position after Shoot: " + this.transform.localPosition);
        }

        public void Retract()
        {
            if (this.transform.localPosition.y > 0f)
            {
                StartCoroutine(_Retract()); 
            }

        }

        IEnumerator _Retract()
        {
            float delay = Random.Range(0f, 0.25f);

            yield return new WaitForSeconds(delay);

            this.transform.localPosition -= Vector3.up * 2.5f; // localPosition yerine position
            Debug.Log("New Position after Retract: " + this.transform.localPosition);
        }

    }
}
