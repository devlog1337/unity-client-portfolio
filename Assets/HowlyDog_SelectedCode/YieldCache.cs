using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Howly Dog에서 사용한 Coroutine GC 절감 아이디어를 바탕으로,
/// 공개 포트폴리오용으로 불필요한 delegate 캐싱을 제거해 정리한 버전입니다.
/// </summary>
internal static class YieldCache
{
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsCache
        = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (WaitForSecondsCache.TryGetValue(seconds, out var wait))
            return wait;

        wait = new WaitForSeconds(seconds);
        WaitForSecondsCache.Add(seconds, wait);
        return wait;
    }
}