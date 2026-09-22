using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PortfolioSamples.Networking
{
    public sealed class RestApiClient : MonoBehaviour
    {
        [SerializeField] private string baseUrl;

        public IEnumerator Get<T>(string path, Action<ApiResult<T>> completed)
        {
            using var request = UnityWebRequest.Get(BuildUrl(path));
            yield return Send(request, completed);
        }

        public IEnumerator Post<TRequest, TResponse>(
            string path,
            TRequest body,
            string requestId,
            Action<ApiResult<TResponse>> completed)
        {
            var json = JsonUtility.ToJson(body);
            var bytes = Encoding.UTF8.GetBytes(json);

            using var request = new UnityWebRequest(BuildUrl(path), UnityWebRequest.kHttpVerbPOST)
            {
                uploadHandler = new UploadHandlerRaw(bytes),
                downloadHandler = new DownloadHandlerBuffer()
            };

            request.SetRequestHeader("Content-Type", "application/json");

            if (!string.IsNullOrWhiteSpace(requestId))
                request.SetRequestHeader("X-Request-Id", requestId);

            yield return Send(request, completed);
        }

        private IEnumerator Send<T>(UnityWebRequest request, Action<ApiResult<T>> completed)
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                completed?.Invoke(ApiResult<T>.Failure(request.responseCode, request.error));
                yield break;
            }

            try
            {
                var data = JsonUtility.FromJson<T>(request.downloadHandler.text);
                completed?.Invoke(ApiResult<T>.Success(request.responseCode, data));
            }
            catch (Exception exception)
            {
                completed?.Invoke(ApiResult<T>.Failure(
                    request.responseCode,
                    $"JSON_PARSE_ERROR: {exception.Message}"));
            }
        }

        private string BuildUrl(string path)
        {
            return $"{baseUrl.TrimEnd('/')}/{path.TrimStart('/')}";
        }
    }
}
