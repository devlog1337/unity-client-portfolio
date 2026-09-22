using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Howly Dog 실제 프로젝트에서 사용 중인 Coroutine YieldInstruction 캐시 코드.
/// 개인 프로젝트 코드 중 공개 가능한 파일만 선별하여 공개합니다.
/// </summary>
internal static class YieldCache
{
    public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
    private static readonly Dictionary<Func<bool>, WaitUntil> waitUtils = new Dictionary<Func<bool>, WaitUntil>();
    private static readonly Dictionary<Func<bool>, WaitWhile> waitWhile = new Dictionary<Func<bool>, WaitWhile>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wait;

        if (!waitForSeconds.TryGetValue(seconds, out wait))
            waitForSeconds.Add(seconds, wait = new WaitForSeconds(seconds));

        return wait;
    }

    public static WaitUntil WaitUntil(Func<bool> condition)
    {
        WaitUntil wait;

        if (!waitUtils.TryGetValue(condition, out wait))
            waitUtils.Add(condition, wait = new WaitUntil(condition));

        return wait;
    }

    public static WaitWhile WaitWhile(Func<bool> condition)
    {
        WaitWhile wait;

        if (!waitWhile.TryGetValue(condition, out wait))
            waitWhile.Add(condition, wait = new WaitWhile(condition));

        return wait;
    }
}
