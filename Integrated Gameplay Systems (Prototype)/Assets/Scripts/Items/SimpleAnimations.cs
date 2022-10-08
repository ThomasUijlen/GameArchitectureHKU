using System;
using System.Threading.Tasks;
using UnityEngine;

public class SimpleAnimations
{
   public async void ItemPickupAnimation(GameObject _gameObject, float _scaleMultiplier, float _seconds, Action onDone = null)
   {
        await ScaleImageInSeconds(_gameObject, _scaleMultiplier, _seconds/3);
        await ScaleImageInSeconds(_gameObject, .1f, 2 * _seconds/3);
        onDone?.Invoke();
   }

    private async Task ScaleImageInSeconds(GameObject _gameObject, float _scaleMultiplier, float _seconds)
    {
        Vector3 oldScale = _gameObject.transform.localScale;
        Vector3 newScale = oldScale * _scaleMultiplier;
        float time = 0f;

        while (time <= _seconds)
        {
            // Change scale with smoothstep
            float newX = Mathf.SmoothStep(oldScale.x, newScale.x, time / _seconds);
            float newY = Mathf.SmoothStep(oldScale.y, newScale.y, time / _seconds);
            float newZ = Mathf.SmoothStep(oldScale.z, newScale.z, time / _seconds);
            _gameObject.transform.localScale = new Vector3(newX, newY, newZ);

            // Wait frame
            time += Time.deltaTime;
            await Task.Yield();
        }
    }
}
