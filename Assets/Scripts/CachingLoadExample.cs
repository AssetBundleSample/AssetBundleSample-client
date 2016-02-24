using UnityEngine;
using System.Collections;

public class CachingLoadExample : MonoBehaviour
{
	string bundleURL = "http://localhost:9000/assets/StreamingAssets/sprites";
	string assetName = "Test";
	int version = 0;

	void Start()
	{
		StartCoroutine(DownloadAndCache());
	}

	IEnumerator DownloadAndCache()
	{
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
