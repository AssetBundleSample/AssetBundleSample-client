using UnityEngine;
using System.Collections;

public class LoadAssetBundle : MonoBehaviour
{
	string assetName = "Test";

	string AssetPath {
		get {
			return Application.streamingAssetsPath + "/sprites";
		}
	}

	IEnumerator Start()
	{
		var resultAssetBundle = AssetBundle.LoadFromFileAsync(AssetPath);

		yield return new WaitWhile(() => resultAssetBundle.isDone == false);

		var assetbundle = resultAssetBundle.assetBundle;
		var resultObject = assetbundle.LoadAssetAsync<GameObject>(assetName);

		yield return new WaitWhile(() => resultObject.isDone == false);

		GameObject.Instantiate(resultObject.asset);
		assetbundle.Unload(false);
	}
}
