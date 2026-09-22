using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace PortfolioSamples.TextureLifecycle
{
    public sealed class TextureLease : IDisposable
    {
        private readonly Action<string> _release;
        private bool _disposed;

        public string Key { get; }
        public Texture2D Texture { get; }

        internal TextureLease(string key, Texture2D texture, Action<string> release)
        {
            Key = key;
            Texture = texture;
            _release = release;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _release?.Invoke(Key);
        }
    }

    public sealed class RemoteTextureStore : MonoBehaviour
    {
        private sealed class Entry
        {
            public Texture2D Texture;
            public int ReferenceCount;
        }

        private readonly Dictionary<string, Entry> _entries = new();

        public IEnumerator Acquire(string url, Action<TextureLease> onSuccess, Action<string> onError)
        {
            if (_entries.TryGetValue(url, out var cached))
            {
                cached.ReferenceCount++;
                onSuccess?.Invoke(new TextureLease(url, cached.Texture, Release));
                yield break;
            }

            using var request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                onError?.Invoke(request.error);
                yield break;
            }

            var texture = DownloadHandlerTexture.GetContent(request);
            texture.wrapMode = TextureWrapMode.Clamp;

            _entries[url] = new Entry
            {
                Texture = texture,
                ReferenceCount = 1
            };

            onSuccess?.Invoke(new TextureLease(url, texture, Release));
        }

        private void Release(string key)
        {
            if (!_entries.TryGetValue(key, out var entry))
                return;

            entry.ReferenceCount--;

            if (entry.ReferenceCount > 0)
                return;

            _entries.Remove(key);

            if (entry.Texture != null)
                Destroy(entry.Texture);
        }

        private void OnDestroy()
        {
            foreach (var entry in _entries.Values)
            {
                if (entry.Texture != null)
                    Destroy(entry.Texture);
            }

            _entries.Clear();
        }
    }
}
