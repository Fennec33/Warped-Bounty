using UnityEngine;

namespace WarpedBounty
{
    public class Timer : MonoBehaviour
    {
        private float _time;

        public float GetTime()
        {
            return _time;
        }

        public void ResetTime()
        {
            _time = 0f;
        }
    
        void Update()
        {
            _time += Time.deltaTime;
        }
    }
}
