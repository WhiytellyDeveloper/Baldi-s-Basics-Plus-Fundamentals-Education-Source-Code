using UnityEngine;

namespace bbpfer.Extessions
{
    public static class Animmations
    {
        public static void SquishingAnim(this SpriteRenderer spr, float minScale, float maxScale, float animationSpeed, float groundOffset)
        {
            if (spr == null)
                return;

            float time = Mathf.PingPong(Time.time * animationSpeed, 1f);
            float scaleValue = Mathf.Lerp(minScale, maxScale, time);

            Vector3 newScale = spr.transform.localScale;
            newScale.y = scaleValue;

            Vector3 newPosition = spr.transform.position;
            newPosition.y -= (1f - scaleValue) * groundOffset;

            spr.transform.localScale = newScale;
            spr.transform.position = newPosition;
        }



    }
}
