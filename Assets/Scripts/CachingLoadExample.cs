using UnityEngine;
using System.Collections;

public class CachingLoadExample : MonoBehaviour
{
	[SerializeField]
	bool enableCache = true;

	string bundleURL = "http://localhost:9000/assets/StreamingAssets/sprites";
	string assetName = "Test";
	int version = 0;

	void Start()
	{
		StartCoroutine(DownloadAndCache());
	}

	IEnumerator DownloadAndCache()
	{
		if (!enableCache) {
			Debug.Log("Clear Cache");
			Caching.CleanCache();
		}

		if (Caching.IsVersionCached(bundleURL, version)) {
			Debug.Log("Exist Cache");
		} else {
			Debug.Log("Not Exist Cache");
		}

		while(!Caching.ready)
			yield return null;

		using(WWW www = WWW.LoadFromCacheOrDownload(bundleURL, version)) {
			yield return www;

			if (www.error != null) {
				throw new UnityException("WWW download had an error" + www.error);
			}

			AssetBundle bundle = www.assetBundle;

			if (assetName == "") {
				Instantiate(bundle.mainAsset);
			} else {
				Instantiate(bundle.LoadAsset(assetName));
			}

			bundle.Unload(false);
		}
	}
}
